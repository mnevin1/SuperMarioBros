using System.ComponentModel.Design.Serialization;
using NLog;
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

    try
    {
        StreamReader sr = new(file);
        // first line contains column headers
        sr.ReadLine();
        while (!sr.EndOfStream)
        {
            string? line = sr.ReadLine();
            if (line is not null)
            {
                string[] characterDetails = line.Split(',');
                characterIds.Add(UInt64.Parse(characterDetails[0]));
                characterNames.Add(characterDetails[1]);
                characterDescriptions.Add(characterDetails[2]);
                characterSpecies.Add(characterDetails[3]);
                characterFirstAppearance.Add(int.Parse(characterDetails[4]));
                characterYearCreated.Add(int.Parse(characterDetails[5]));
            }
        }
        sr.Close();
    }
    catch (Exception ex)
    {
        logger.Error(ex, "Error reading file: {File}", file);
    }

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
