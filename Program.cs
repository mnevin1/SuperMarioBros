﻿using NLog;
string path = Directory.GetCurrentDirectory() + "//nlog.config";

var logger = LogManager.Setup().LoadConfigurationFromFile(path).GetCurrentClassLogger();

logger.Info("Program started");

string file = "mario.csv";
if (!File.Exists(file))
{
    logger.Error("File does not exist: {file}", file);
}
else
{

    List<UInt64> characterIds = [];
    List<string> characterNames = [];
    List<string> characterDescriptions = [];
    List<string> characterSpecies = [];
    List<int> characterFirstAppearance = [];
    List<int> characterYearCreated = [];


        string? choice;
    do
    {
        // display choices to user
        Console.WriteLine("1) Add Character");
        Console.WriteLine("2) Display All Characters");
        Console.WriteLine("Enter to quit");

        // input selection
        choice = Console.ReadLine();
        logger.Info("User choice: {Choice}", choice);

        if (choice == "1")
        {
            // Add Character
        }
        else if (choice == "2")
        {
            // Display All Characters
        }
    } while (choice == "1" || choice == "2");    
}

logger.Info("program ended");
