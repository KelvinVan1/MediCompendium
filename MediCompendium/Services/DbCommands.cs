using MediCompendium.Models.DbTables;
using SQLite;

namespace MediCompendium.Services;

public class DbCommands {
    private SQLiteAsyncConnection? _database;
    
    public DbCommands() { }

    // Profile creation and management
    private async Task InitProfile()
    {
        if (_database is not null) return;

        _database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        await _database.CreateTableAsync<UserProfile>();
    }

    public async Task<int> AddProfile(UserProfile profile) {
       await InitProfile();
       
       if (_database == null) return 0;
       
       return await _database.InsertAsync(profile);
    }

    public async Task<List<UserProfile>> GetProfiles() {
        await InitProfile();

        if (_database == null) return new List<UserProfile>();

        return await _database.Table<UserProfile>().ToListAsync();
    }
}