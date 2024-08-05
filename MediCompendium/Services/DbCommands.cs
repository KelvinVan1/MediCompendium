using MediCompendium.Models.DbTables;
using SQLite;

namespace MediCompendium.Services;

public class DbCommands {
    private SQLiteAsyncConnection? _database;
    
    public DbCommands() { }

    // Profile creation and management
    private async Task InitUserProfile()
    {
        if (_database is not null) return;

        _database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        await _database.CreateTableAsync<UserProfile>();
    }

    private async Task InitFavoriteMedication()
    {
        if (_database is not null) return;

        _database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        await _database.CreateTableAsync<FavoritedItem>();
    }
    
    public async Task<int> AddProfile(UserProfile profile) {
       await InitUserProfile();
       
       if (_database == null) return 0;
       
       if (profile.Id == 0) 
           return await _database.InsertAsync(profile);
       else
           return await _database.UpdateAsync(profile);
    }

    public async Task<int> DeleteProfile(int id) {
        await InitUserProfile();

        if (_database == null) return 0;

        var query = "DELETE FROM profile WHERE id = ?";
        return await _database.ExecuteAsync(query, id);
    }
    
    public async Task<List<UserProfile>> GetProfiles() {
        await InitUserProfile();

        if (_database == null) return new List<UserProfile>();

        return await _database.Table<UserProfile>().ToListAsync();
    }
    
    public async Task<int> AddFavorite(FavoritedItem item) {
        await InitFavoriteMedication();

        if (_database == null) return 0;

        return await _database.InsertAsync(item);
    }
    
    public async Task<int> RemoveFavorite(FavoritedItem item) {
        await InitFavoriteMedication();

        if (_database == null) return 0;

        const string query = "DELETE FROM favorite_items WHERE (product_ndc = ? AND profile_id = ?);";
        return await _database.ExecuteAsync(query, item.ProductNdc, item.ProfileId);
    }

    public async Task<List<FavoritedItem>> SearchFavoriteItem(int userId, string productNdc) {
        await InitFavoriteMedication();

        if (_database == null) return new List<FavoritedItem>();

        const string query = "SELECT * FROM favorite_items WHERE (product_ndc = ? AND profile_id = ?)"; 
        return await _database.QueryAsync<FavoritedItem>(query, productNdc, userId);
    }
    
    public async Task<List<FavoritedItem>> GetFavoriteItems(int userId) {
        await InitFavoriteMedication();

        if (_database == null) return new List<FavoritedItem>();

        const string query = "SELECT * FROM favorite_items WHERE profile_id = ?";
        return await _database.QueryAsync<FavoritedItem>(query, userId);
    }
}