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
            Character character = new();
            Console.WriteLine("Enter character name: ");
            character.Name = Console.ReadLine() ?? string.Empty;
            if (!string.IsNullOrEmpty(character.Name))
            {
                List<string> LowerCaseNames = characters.ConvertAll(character => character.Name.ToLower());
                if (LowerCaseNames.Contains(character.Name.ToLower()))
                {
                    logger.Info($"Duplicate name {character.Name}");
                }
                else
                {
                    character.Id = characters.Max(character => character.Id) + 1;
                    Console.WriteLine("Enter description: ");
                    character.Description = Console.ReadLine() ?? string.Empty;
                    Console.WriteLine("Enter species: ");
                    character.Species = Console.ReadLine() ?? string.Empty;
                    Console.WriteLine("Enter first appearance: ");
                    character.FirstAppearance = Console.ReadLine() ?? string.Empty;
                    Console.WriteLine("Enter year created: ");
                    character.YearCreated = int.Parse(Console.ReadLine() ?? "0");
                    StreamWriter sw = new(file, true);
                    sw.WriteLine($"{character.Id},{character.Name},{character.Description},{character.Species},{character.FirstAppearance},{character.YearCreated}");
                    sw.Close();
                    logger.Info($"Character id {character.Id} added");
                }
            }
            else
            {
                logger.Error("You must enter a name");
            }
        }

        else if (choice == "2")
        {
            foreach (Character character in characters)
            {
                Console.WriteLine(character.Display());
            }
        }
    } while (choice == "1" || choice == "2");    
}

logger.Info("program ended");
