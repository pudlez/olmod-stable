using System;
using System.Diagnostics;
using System.IO;
using LibGit2Sharp;
using static System.Net.WebRequestMethods;

class Program {
    static void Main(string[] args) {
        string gitRepo="https://github.com/overload-development-community/olmod-stable-binaries.git";

        string exeDirectory = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule?.FileName ?? string.Empty) ?? string.Empty;
        string repoPath = Path.Combine(exeDirectory, "olmod-stable-binaries");
        string executablePath = Path.Combine(repoPath, "bin", "olmod.exe");

        // Check if being asked to generate a config file
        if (args.Contains("--config")) {
            CreateConfigFile(exeDirectory);
            Console.WriteLine("config.txt has been created.");
            return; // exit
        }

        // Check if config.txt exists
        string configFilePath = Path.Combine(exeDirectory, "config.txt");

        // set default to autoclose the window
        bool autoCloseWindow = true;

        if (System.IO.File.Exists(configFilePath))
        { // Fully qualify the File class
          // Read lines from config.txt and pass arguments to the executable
            string[] lines = System.IO.File.ReadAllLines(configFilePath); // Fully qualify the File class
            foreach (string line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line) && !line.TrimStart().StartsWith("#"))
                {
                    if (line.StartsWith("auto-close-window=false"))
                    {
                        if (line.StartsWith("auto-close-window=false"))
                        {
                            autoCloseWindow = false;
                        }
                    }
                    else
                    {
                        string[] argsToAdd = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        args = args.Concat(argsToAdd).ToArray();
                    }
                }
            }
        }

        // Check if the repo exists locally
        if (!Repository.IsValid(repoPath)) {
            // doesn't exist so lets clone
            Console.WriteLine("Grabbing the latest olmod version...");
            Repository.Clone(gitRepo, repoPath);
        } else {
            // exists so lets pull
            using (var repo = new Repository(repoPath)) {
                Console.WriteLine("Making sure we have the latest olmod version...");
                Commands.Pull(repo, new Signature("Anonymous", "anonymous@otl.gg", DateTimeOffset.Now), new PullOptions());
            }
        }

        // launch olmod
        Console.WriteLine("Launching the latest olmod version...");
        Process.Start(executablePath, string.Join(" ", args));

        // if autoCloseWindow is set to false, wait for user input before closing
        if (!autoCloseWindow) {
            Console.WriteLine("Press any key to close...");
            Console.ReadKey();
        }
    }

    // ********************************
    // * Create a generic config file *
    // ********************************
    static void CreateConfigFile(string directory)
    {
        string configFilePath = Path.Combine(directory, "config.txt");
        if (!System.IO.File.Exists(configFilePath))
        { // Fully qualify the File class
            using (StreamWriter writer = System.IO.File.CreateText(configFilePath))
            { // Fully qualify the File class
                writer.WriteLine("#  Config file for olmod-stable");
                writer.WriteLine("#  Lines that begin with # are ignored...");
                writer.WriteLine("#");
                writer.WriteLine("#  Uncomment the following line to keep the console window open");
                writer.WriteLine("#auto-close-window=false");
                writer.WriteLine("#");
                writer.WriteLine("#  Each argument should be on a separate line");
                writer.WriteLine("#-gamedir \"C:\\Program Files (x86)\\Steam\\steamapps\\common\\Overload\"");
                writer.WriteLine("#-frametime");
            }
        }
    }

}
