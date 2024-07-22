using MediCompendium.Models.ApiRecords;
namespace MediCompendium.Models;

public class Medication {
    public string? BrandName { get; set; }
    public string? GenericName { get; set; }
    public string? LabelerName { get; set; }
    public List<ActiveIngredients>? ActiveIngredients { get; set; }
    
    public Medication(){}
    
    public Medication(string brandName, string genericName, string labelerName, List<ActiveIngredients> activeIngredients) {
        BrandName = brandName;
        GenericName = genericName;
        LabelerName = labelerName;
        ActiveIngredients = activeIngredients;
    }

    public string ActiveIngredientsToString(int count) {
        var result = "";
        
        if (ActiveIngredients == null || ActiveIngredients.Count == 0) return "Not Provided";
        if (ActiveIngredients.Count < count) count = ActiveIngredients.Count;
        
        for (var i = 0; i < count; i++) {
            var ingredient = ActiveIngredients[i].name.ToLower() + " " + ActiveIngredients[i].strength + ", ";
            result += ingredient;
        }

        return result.Remove(result.Length-2, 2);
    }
}