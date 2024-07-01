namespace MediCompendium.Services;

static class Constants
{
    // API
    public const string NdcRoute = "https://api.fda.gov/drug/ndc.json?";
    public const string Limit = "limit=5";
    
    // Database
    public const string DatabaseFilename = "Medicompendium.db3";
    public const SQLite.SQLiteOpenFlags Flags =
        // open the database in read/write mode
        SQLite.SQLiteOpenFlags.ReadWrite |
        // create the database if it doesn't exist
        SQLite.SQLiteOpenFlags.Create |
        // enable multi-threaded database access
        SQLite.SQLiteOpenFlags.SharedCache;
    public static string DatabasePath =>
        Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);
}
    
