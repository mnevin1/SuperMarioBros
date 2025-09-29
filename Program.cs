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

    List<Character> characters = [];

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
                Character character = new();
                string[] characterDetails = line.Split(',');
                character.Id = UInt64.Parse(characterDetails[0]);
                character.Name = characterDetails[1] ?? string.Empty;
                character.Description = characterDetails[2] ?? string.Empty;
                character.Species = characterDetails[3] ?? string.Empty;
                character.FirstAppearance = characterDetails[4] ?? string.Empty;
                character.YearCreated = int.Parse(characterDetails[5]);
                characters.Add(character);
            }
        }
        sr.Close();
    }
    catch (Exception ex)
    {
        logger.Error(ex.Message);
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
                    Console.WriteLine("Enter species: ");
                    string? species = Console.ReadLine();
                    Console.WriteLine("Enter first appearance: ");
                    string? firstAppearance = Console.ReadLine();
                    Console.WriteLine("Enter year created: ");
                    int yearCreated = int.Parse(Console.ReadLine() ?? "0");
                    StreamWriter sw = new(file, true);
                    sw.WriteLine($"{ID},{name},{description},{species},{firstAppearance},{yearCreated}");
                    sw.Close();
                    logger.Info($"Character id {ID} added");
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
