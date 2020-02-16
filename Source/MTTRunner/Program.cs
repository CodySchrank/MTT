using System;
using MTT;

namespace MTTRunner
{
    public class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Assuming testing...");

                args = new string[2] {"", ""};
                
                //works for vs code
                args[0] = "../../example/Resources";
                args[1] = "../../example/models";
            }

            Program.StartService(args);
        }

        public static void StartService(string[] args) {
            var convertService = new ConvertService((logString, logArgs) => Console.WriteLine(logString, logArgs))
            {
                WorkingDirectory = args[0],
                ConvertDirectory = args[1]
            };
            
            convertService.Execute();
        }


    }
}
