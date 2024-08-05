using MediCompendium.Models.DbTables;
using SQLite;

namespace MediCompendium.Services;

public class DbCommands {
    private SQLiteAsyncConnection? _database;
    
    public DbCommands() { }

    // Profile creation and management
    private async Task InitUserProfile() {
        try {
            if (_database is not null) return;

            _database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            await _database.CreateTableAsync<UserProfile>();
        }
        catch (Exception err) {
            Console.WriteLine($"An error has occured. {err}");
        }
    }

    private async Task InitFavoriteMedication() {
        try {
            if (_database is not null) return;

            _database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            await _database.CreateTableAsync<FavoritedItem>();
        }
        catch (Exception err) {
            Console.WriteLine($"An error has occured. {err}");
        }
    }
    
    public async Task<int> AddProfile(UserProfile profile) {
        try {
            await InitUserProfile();
       
            if (_database == null) return 0;
       
            if (profile.Id == 0) 
                return await _database.InsertAsync(profile);
            else
                return await _database.UpdateAsync(profile);
        }
        catch (Exception err) {
            Console.WriteLine($"An error has occured. {err}");
            return 0;
        }
    }

    public async Task<int> DeleteProfile(int id) {
        try {
            await InitUserProfile();

            if (_database == null) return 0;

            var query = "DELETE FROM profile WHERE id = ?";
            return await _database.ExecuteAsync(query, id);
        }
        catch (Exception err) {
            Console.WriteLine($"An error has occured. {err}");
            return 0;
        }
    }
    
    public async Task<List<UserProfile>> GetProfiles() {
        try {
            await InitUserProfile();

            if (_database == null) return new List<UserProfile>();

            return await _database.Table<UserProfile>().ToListAsync();
        }
        catch (Exception err) {
            Console.WriteLine($"An error has occured. {err}");
            return new List<UserProfile>();
        }
    }
    
    public async Task<int> AddFavorite(FavoritedItem item) {
        try {
            await InitFavoriteMedication();

            if (_database == null) return 0;

            return await _database.InsertAsync(item);
        }
        catch (Exception err) {
            Console.WriteLine($"An error has occured. {err}");
            return 0;
        }
    }
    
    public async Task<int> RemoveFavorite(FavoritedItem item) {
        try {
            await InitFavoriteMedication();

            if (_database == null) return 0;

            const string query = "DELETE FROM favorite_items WHERE (product_ndc = ? AND profile_id = ?);";
            return await _database.ExecuteAsync(query, item.ProductNdc, item.ProfileId);
        }
        catch (Exception err) {
            Console.WriteLine($"An error has occured. {err}");
            return 0;
        }
    }

    public async Task<List<FavoritedItem>> SearchFavoriteItem(int userId, string productNdc) {
        try {
            await InitFavoriteMedication();

            if (_database == null) return new List<FavoritedItem>();

            const string query = "SELECT * FROM favorite_items WHERE (product_ndc = ? AND profile_id = ?)";
            return await _database.QueryAsync<FavoritedItem>(query, productNdc, userId);
        }
        catch (Exception err) {
            Console.WriteLine($"An error has occured. {err}");
            return new List<FavoritedItem>();
        }
    }
    
    public async Task<List<FavoritedItem>> GetFavoriteItems(int userId) {
        try {
            await InitFavoriteMedication();

            if (_database == null) return new List<FavoritedItem>();

            const string query = "SELECT * FROM favorite_items WHERE profile_id = ?";
            return await _database.QueryAsync<FavoritedItem>(query, userId);
        }
        catch (Exception err) {
            Console.WriteLine($"An error has occured. {err}");
            return new List<FavoritedItem>();
        }
    }
}