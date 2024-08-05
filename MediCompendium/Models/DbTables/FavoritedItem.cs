using SQLite;

namespace MediCompendium.Models.DbTables;

[Table("favorite_items")]
public class FavoritedItem {
    [PrimaryKey, AutoIncrement, SQLite.Column("_id")]
    public int Id { get; set; }
    
    [Column("profile_id")]
    public string ProfileId { get; set; }

    [Column("product_ndc")] 
    public string ProductNdc { get; set; }
}