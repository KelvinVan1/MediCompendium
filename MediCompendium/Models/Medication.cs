using MediCompendium.Models.ApiRecords;
namespace MediCompendium.Models;

public class Medication {
    public string? ProductNdc { get; set; }
    public string? ProductType { get; set; }
    public string? BrandName { get; set; }
    public string? LabelerName { get; set; }
    public List<string>? Warnings { get; set; }
    public List<string>? IndicationsAndUsage { get; set; }
    public List<string>? DosageAndAdministration { get; set; }
    public List<ActiveIngredients>? ActiveIngredients { get; set; }
    
    public Medication(){}
    
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