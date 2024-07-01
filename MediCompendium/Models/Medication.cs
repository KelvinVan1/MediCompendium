namespace MediCompendium.Models;

public class Medication {
    public string? BrandName { get; set; }
    public string? GenericName { get; set; }
    public string? LabelerName { get; set; }
    
    public Medication(){}
    
    public Medication(string brandName, string genericName, string labelerName) {
        BrandName = brandName;
        GenericName = genericName;
        LabelerName = labelerName;
    }
}