using SQLite;

namespace MediCompendium.Models.DbTables;

[Table("profile")]
public class UserProfile {
    [PrimaryKey, AutoIncrement, Column("id")]
    public int Id { get; set; }
    
    [MaxLength(250), Unique, Column("username")]
    public string Username { get; set; }
    
    [Column("gender")]
    public string Gender { get; set; }
    
    [Column("first_name")]
    public string FirstName { get; set; }
    
    [Column("last_name")]
    public string LastName { get; set; }
    
    [Column("age")]
    public int Age { get; set; } 
}