using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Romp
{
    class UciInputException : Exception
    {
        public UciInputException(string? msg)
            : base(msg)
        { }

        public UciInputException()
            : base()
        { }

        public UciInputException(string? msg, Exception innerException)
            : base(msg, innerException)
        { }

        public override string Message => $"Uci Input Exception: {base.Message}";
    }

    static class UCI
    {
        public class RegisteredUser
        {
            public string Name;
            public string Code;
        }

        private delegate void CmdFunc(in string[] args);

        public static readonly ConcurrentDictionary<int, string> TaskIdLookup = new ConcurrentDictionary<int, string>();
        public static readonly ConcurrentBag<Task> TaskList = new ConcurrentBag<Task>();

        private static readonly char[] SPLIT_CMD_CHARS = new char[2] { ' ', '\t' };
        private static readonly ConcurrentBag<RegisteredUser> _registeredUsers = new ConcurrentBag<RegisteredUser>();        
        private static int _taskUpdIdx = 0;

        public static void StartMainLoop(string[] args)
        {
#if !DEBUG            
            _taskList.Add(Task.Run(() => { MainLoop(args); }, Engine.MainThreadCanceller.Token);
#else
            var task = new Task(() => { MainLoop(args); }, Engine.MainThreadCanceller.Token);
            TaskIdLookup.AddOrUpdate(task.Id, "MainLoop", (int id, string desc) => {

                var idx = desc.IndexOf('-');
                if (idx == -1)
                {
                    return $"{desc}-{Interlocked.Increment(ref _taskUpdIdx)}";
                }
                else
                {
                    return $"{desc.Substring(0, idx - 1)}-{Interlocked.Increment(ref _taskUpdIdx)}";
                }
            });

            TaskList.Add(task);
            task.Start();
#endif        
        }

        private static void MainLoop(string[] args)
        {
            // turn the cmd line string into 
            var sb = new StringBuilder();
            for (int i = 0; i < args.Length; i++)
                sb.Append($"{args[i]} ");

            string cmdLine = sb.ToString();
            Debug.WriteLine($"cmdline args to string: {cmdLine}");

            bool keep_going = true;

            do
            {
                // if we receive startup parameters then, run it as one-and-done and don't read from std::in
                // the Console.Readline() should block until it receives some input
                if (args.Length == 0 && String.IsNullOrEmpty(cmdLine = Console.ReadLine()))
                    cmdLine = "quit";

                // quit all threads
                if (cmdLine == "quit" || cmdLine == "stop" && !Engine.MainThreadCanceller.IsCancellationRequested)
                    Engine.MainThreadCanceller.Cancel();

                var tokens = cmdLine.Split(SPLIT_CMD_CHARS);

                if (tokens[0] == "uci")
                {
                    ConsoleWriter.WriteLine(GetInfoStr());
                }
                else if (tokens[0] == "debug")
                {
                    tokens[0] = "e";
                    if (tokens.Length != 2)
                    {
                        Engine.UciDebug = Utils.SwitchStrToBool(tokens[1]);
                    }
                    else
                    {
                        throw new UciInputException("invalid input token for \'debug\' command");
                    }
                }
                else if (tokens[0] == "isready")
                {
                    // need to check on currently running tasks to respond properly
                    if (Engine.IsReady())
                        ConsoleWriter.WriteLine("readyok");

                }
                else if (tokens[0] == "setoption")
                {
                    SetOption(tokens[1..]);
                }
                else if (tokens[0] == "register")
                {
                    Register(tokens[1..]);
                }
                else if (tokens[0] == "ucinewgame")
                {

                }
                else if (tokens[0] == "position")
                {
                    // set the pieces on the board in FEN  
                    Position(tokens[1..]);
                }
                else if (tokens[0] == "go")
                {
                    // start searching or pondering if the position command was sent previously
                    Go(tokens[1..]);
                }
                else if (tokens[0] == "stop")
                {
                    // stop calculating as soon as possible                    
                }
                else if (tokens[0] == "ponderhit")
                {

                }
                else if (tokens[0] == "quit")
                {

                }

            } while (args.Length == 0 && !Engine.MainThreadCanceller.IsCancellationRequested); // only loop if no cmdline args are passed
        }

        /// <summary>
        /// performs the UCI, GUI => Engine 'setoption' command
        /// </summary>
        /// <param name="tokens"></param>
        private static void SetOption(string[] tokens)
        {

        }

        /// <summary>
        /// performs the UCI, GUI => Engine 'register' commmand.
        /// </summary>
        /// <param name="tokens">all the tokens folling the &quot;register&quot; token</param>
        private static void Register(string[] tokens)
        {
            if (tokens.Length == 1)
            {
                // there's only 1 token so it should be 'later', error if not
                if (tokens[0] != "later")
                    throw new UciInputException($"invalid input token for register cmd: {tokens[0]}");

                // set the RegisterLater flag, we may not actually need this, but maybe we should output registration error as a reminder that 
                // the GUI did actually want to register at some point
                Engine.RegisterLater = true;

                // there isn't anything we really need to do as registration is not required
                return;
            }
            else
            {
                var usr = new RegisteredUser();
                for (int i = 0; i < tokens.Length; i++)
                {
                    if (tokens[i] == "name" && i + 1 < tokens.Length)
                    {
                        usr.Name = tokens[++i];
                    }
                    else if (tokens[i] == "code" && i + 1 < tokens.Length)
                    {
                        usr.Code = tokens[++i];
                    }
                }

                _registeredUsers.Add(usr);
            }
        }

        /// <summary>
        /// sets the board position using FEN and moves
        /// </summary>
        /// <param name="tokens">all of the tokens following the  &quot;position&quot; token</param>
        private static void Position(string[] tokens)
        {
            int idx = 0;
            string fen = String.Empty;
            if (tokens[0] == "startpos")
            {
                fen = Constants.FEN_START_STRING;
            }
            else 
            {
                if (tokens[0] == "fen")
                    idx++; // eat the FEN token because the functionality is the same between 'position fen' and 'postion {fen string}'

                // eat up the all parts of the FEN string
                var sb = new StringBuilder();                
                do
                {
                    sb.Append($"{tokens[idx++]} ");
                } while (tokens[idx] != "moves");

                fen = sb.ToString();
            }

            var game = Engine.NewGame(fen);

            var moveGen = new MoveGenerator(game);

            // use idx+1 to advance past the 'moves' token 
            for (int i = idx+1; i < tokens.Length; i++)
            {
                // not sure if it's faster to loop or index and search the move list
                // but we're going to loop for now because that is what stockfish does
                // and thats the best conventional engine right now

                foreach (var move in moveGen.LegalMoves)
                {
                    if (move.GetNotation(game) == tokens[idx])
                        game.Apply(move);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tokens">all of the tokens following the  &quot;go&quot; token</param>
        private static void Go(string[] tokens)
        {
            // there are a zillion options that go has to support
        }

        /// <summary>
        /// gets startup output for UCI engine
        /// </summary>
        /// <returns>&quot;id&quot; and &quot;option&quot; command output</returns>
        private static string GetInfoStr()
        {
            var strBld = new StringBuilder();

            strBld.AppendLine($"id name Romp {Engine.Info.FileVersion}");
            strBld.AppendLine($"id author {Engine.Info.Author}");            
            strBld.AppendLine();
            strBld.AppendLine(Option.GetInfoStr());
            strBld.AppendLine();

            return strBld.ToString();
        }


        // use this to preserve the timing of the output written to console from different threads
        private static class ConsoleWriter
        {
            private static readonly BlockingCollection<string> _queue = new BlockingCollection<string>();

            static ConsoleWriter()
            {
                var thread = new Thread(() => { while (true) Console.WriteLine(_queue.Take()); });
                thread.IsBackground = true;
                thread.Start();
            }

            public static void WriteLine(string s)
            {
                _queue.Add(s);
            }
        }

    }
}


