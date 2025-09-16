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
    
}

logger.Info("program ended");
