using MediCompendium.Models;
using MediCompendium.Models.ApiRecords;
using MediCompendium.Services;
using Xunit.Abstractions;

namespace MediCompendiumUnitTest;

public class GenerateMedicationTests {
    
    // Test to ensure that a List is returned when running GenerateMedication
    [Fact]
    public void Adding_Medication_Returns_List() {
        var prescription = new NdcData() {product_type = "HUMAN PRESCRIPTION DRUG" };
        var parameter = new List<NdcData>();
        parameter.Add(prescription);

        var medication = Helper.GenerateMedications(parameter);
        Assert.IsType<List<Medication>>(medication);
    }
    
    // Test to ensure that when adding a medication that the returned list's count increases
    [Fact]
    public void Adding_Medication_Updates_List() {
        var prescription = new NdcData() {product_type = "HUMAN PRESCRIPTION DRUG" };
        var parameter = new List<NdcData>();
        parameter.Add(prescription);
        var medication = Helper.GenerateMedications(parameter);
        Assert.Single(medication);
        
        var otc = new NdcData() {product_type = "HUMAN OTC DRUG" };
        parameter.Add(otc);
        medication = Helper.GenerateMedications(parameter);
        Assert.Equal(2, medication.Count);
    }
    
    // Test to ensure that the correct medication type is being generated
    [Fact]
    public void Adding_Medication_Returns_Proper_Type() {
        var prescription = new NdcData() {product_type = "HUMAN PRESCRIPTION DRUG" };
        var medication = Helper.GenerateMedications(new List<NdcData>() {prescription});

        Assert.IsType<MedicationPrescription>(medication[0]);
        
        var otc = new NdcData() { brand_name = "OTC Drug", product_type = "HUMAN OTC DRUG" };
        medication = Helper.GenerateMedications(new List<NdcData>() {otc});

        Assert.IsType<MedicationOTC>(medication[0]);
    }
    
    // Test to ensure that the correct medication data is being generated
    [Fact]
    public void Adding_Medication_Contains_Correct_Data() {
        var prescription = new NdcData() { brand_name = "Prescription Drug", product_type = "HUMAN PRESCRIPTION DRUG" };
        var medication = Helper.GenerateMedications(new List<NdcData>() {prescription});

        Assert.Equal("Prescription Drug", medication[0].BrandName);
        
        var otc = new NdcData() { brand_name = "OTC Drug", product_type = "HUMAN OTC DRUG" };
        medication = Helper.GenerateMedications(new List<NdcData>() {otc});

        Assert.Equal("OTC Drug", medication[0].BrandName);
    }
}