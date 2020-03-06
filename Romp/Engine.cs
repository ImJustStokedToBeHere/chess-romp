using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Romp
{
    /// <summary>
    /// contains engine state management for the thinking threads
    /// </summary>
    static class Engine
    {
        /// <summary>
        /// used to cancel all threads via the UCi 'stop' or 'quit' command
        /// </summary>
        public static CancellationTokenSource MainThreadCanceller = new CancellationTokenSource();

        /// <summary>
        /// used when the 'register' command is called with the 'later' token via the UCI interface
        /// </summary>
        public static bool RegisterLater { get; set; } = false;

        /// <summary>
        /// Flag signifiying the 'debug' command was passed via UCI interface
        /// </summary>
        public static bool UciDebug { get; set; } = false;

        public static Stack<Game> Games { get; private set; } = new Stack<Game>();

        /// <summary>
        /// the Game the engine is working on
        /// </summary>
        public static Game CurrentGame => Games.Peek();

        /// <summary>
        /// checks internal state of thinking threads to see if we're ready to perform more actions
        /// </summary>
        /// <returns>whether or not the engine is ready to accept commands from std in</returns>
        public static bool IsReady()
        {
            if (CurrentGame is null)
                return false;

            // TODOTODO: figure out how to manage the state so the GUI doesn't flood us with commands
            return true;
        }
        
        public static Game NewGame(string fen)
        {
            return null;
        }

        /// <summary>
        /// contains the static information used for console display
        /// </summary>
        public static class Info
        {            
            public static string FileVersion
            {
                get
                {
                    var assembly = Assembly.GetExecutingAssembly();
                    var fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
                    return fileVersionInfo.FileVersion;
                }
            }

            public static readonly string Author = "Jake & Joe Vidunas";
        }
    }
}
