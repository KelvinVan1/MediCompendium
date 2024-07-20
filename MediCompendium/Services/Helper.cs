using MediCompendium.Models;
using MediCompendium.Models.ApiRecords;

namespace MediCompendium.Services;

public class Helper {
    public static List<Medication> GenerateMedications(List<NdcData> ndcInformation) {
        var result = new List<Medication>();
        
        foreach (var ndc in ndcInformation) {
            result.Add(new Medication() {
                BrandName = ndc.brand_name,
                LabelerName = ndc.labeler_name,
                GenericName = ndc.generic_name,
                ActiveIngredients = ndc.active_ingredients,
            });
        }

        return result;
    }
}