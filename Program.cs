using System.ComponentModel.Design.Serialization;
using System.Reflection.Metadata;
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
    List<string> characterFirstAppearance = [];
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
                characterFirstAppearance.Add(characterDetails[4]);
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
            Console.WriteLine("Enter character name: ");
            string? name = Console.ReadLine();
            if (!string.IsNullOrEmpty(name))
            {
                List<string> LowerCaseNames = characterNames.ConvertAll(n => n.ToLower());
                if (LowerCaseNames.Contains(name.ToLower()))
                {
                    logger.Info($"Duplicate name {name}");
                }
                else
                {
                    UInt64 ID = characterIds.Max() + 1;
                    Console.WriteLine("Enter description: ");
                    string? description = Console.ReadLine();
                    Console.WriteLine($"{ID},{name},{description}");
                }
            }
            else
            {
                logger.Error("You must enter a name");
            }
        }

        else if (choice == "2")
        {
            for (int i = 0; i < characterIds.Count; i++)
            {
                Console.WriteLine($"ID: {characterIds[i]}");
                Console.WriteLine($"Name: {characterNames[i]}");
                Console.WriteLine($"Description: {characterDescriptions[i]}");
                Console.WriteLine($"Species: {characterSpecies[i]}");
                Console.WriteLine($"First Appearance: {characterFirstAppearance[i]}");
                Console.WriteLine($"Year Created: {characterYearCreated[i]}");
                Console.WriteLine();
            }
        }
    } while (choice == "1" || choice == "2");    
}

logger.Info("program ended");
