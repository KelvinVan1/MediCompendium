using MediCompendium.Models;
using MediCompendium.Models.ApiRecords;
using MediCompendium.Models.DbTables;

namespace MediCompendium.Services;

public class Helper {
    public static List<Medication> GenerateMedications(List<NdcData> ndcInformation) {
        var result = new List<Medication>();
        
        foreach (var ndc in ndcInformation) {
            if(ndc.product_type == "HUMAN PRESCRIPTION DRUG")
                result.Add(new MedicationPrescription() {
                    ProductNdc = ndc.product_ndc,
                    ProductType = ndc.product_type,
                    BrandName = ndc.brand_name,
                    LabelerName = ndc.labeler_name,
                    ActiveIngredients = ndc.active_ingredients,
                    DeaSchedule = ndc.dea_schedule,
                });
            else
                result.Add(new MedicationOTC() {
                    ProductNdc = ndc.product_ndc,
                    ProductType = ndc.product_type,
                    BrandName = ndc.brand_name,
                    LabelerName = ndc.labeler_name,
                    GenericName = ndc.generic_name,
                    ActiveIngredients = ndc.active_ingredients,
                });
        }
        
        return result;
    }

    public static async Task<Medication> FetchMedication(string productNdc) {
        var label = await ApiCommands.FetchMedicationLabel(productNdc);
        var ndc = await ApiCommands.FetchMedicationNdc(productNdc);
        if(ndc.product_type == "HUMAN PRESCRIPTION DRUG")
            return new MedicationPrescription() {
                ProductNdc = ndc.product_ndc,
                ProductType = ndc.product_type,
                BrandName = ndc.brand_name,
                LabelerName = ndc.labeler_name,
                Description = label.Description,
                Warnings = label.warnings,
                IndicationsAndUsage = label.indications_and_usage,
                ActiveIngredients = ndc.active_ingredients,
                DosageAndAdministration = label.dosage_and_administration,
                WarningsAndCautions = label.warnings_and_cautions,
                HowSupplied = label.HowSupplied,
                DeaSchedule = ndc.dea_schedule,
            };
        else
            return new MedicationOTC() {
                ProductNdc = ndc.product_ndc,
                ProductType = ndc.product_type,
                BrandName = ndc.brand_name,
                LabelerName = ndc.labeler_name,
                GenericName = ndc.generic_name,
                ActiveIngredients = ndc.active_ingredients,
                DosageForm = ndc.dosage_form.ToLower(),
                IndicationsAndUsage = label.indications_and_usage,
                Warnings = label.warnings,
                KeepOutOfReach = label.keep_out_of_reach_of_children,
                DosageAndAdministration = label.dosage_and_administration,
                Questions = label.questions,
                Purpose = label.purpose,
            };
    }
    
    public static async Task<List<NdcData>> FetchFavoriteMedications(List<FavoritedItem> favorites) {
        var result = new List<NdcData>();
        
        foreach (var medication in favorites) {
            result.Add(await ApiCommands.FetchMedicationNdc(medication.ProductNdc));    
        }
        
        return result;
    }
}