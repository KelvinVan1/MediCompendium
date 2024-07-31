using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediCompendium.Models;
using MediCompendium.Services;

namespace MediCompendium.Pages;


[QueryProperty("MedicationNdc", "MedicationNdc")]
public partial class OTCDetails : ContentPage {
    private string _medicationNdc;
    private Medication _currentMedication;
    public string MedicationNdc {
        get => _medicationNdc;
        set {
            _medicationNdc = value;
            FetchMedicationData(); 
        }
    }

    public OTCDetails() {
        InitializeComponent();
    }

    private async void FetchMedicationData() {
        _currentMedication = await Helper.FetchMedication(_medicationNdc);
        PopulatePage();
    }
    
    private void PopulatePage() {
        if (!(_currentMedication is MedicationOTC otc)) return;

        MedicationLabeler.Text = $"Labeler: {otc.LabelerName}";
        MedicationName.Text = $"Medication: {otc.BrandName}";
        
        if(otc.ActiveIngredients != null)
            MedicationActiveIngredients.Text = otc.ActiveIngredientsToString(otc.ActiveIngredients.Count).Replace(". ", ".\n");
        if(otc.Purpose != null) 
            MedicationDescription.Text = otc.Purpose[0].Replace(". ",".\n\n");
        if(otc.Warnings != null)
            MedicationWarnings.Text = otc.Warnings[0].Replace(". ", ".\n\n");
        if(otc.KeepOutOfReach != null && MedicationWarnings.Text != $"Warnings {otc.KeepOutOfReach[0]}")
            MedicationWarnings.Text += otc.KeepOutOfReach[0];
        if(otc.IndicationsAndUsage != null) 
            MedicationUsage.Text = otc.IndicationsAndUsage[0].Replace(". ", ".\n\n");
        if(otc.DosageAndAdministration != null) 
            MedicationDosage.Text = otc.DosageAndAdministration[0].Replace(". ", ".\n\n");
        if(otc.DosageForm != null)
            MedicationPackaging.Text = string.Join("\n", otc.DosageForm).Replace(". ", ".\n\n");
        if(otc.Questions != null)
            MedicationQuestion.Text = string.Join("\n", otc.Questions).Replace(". ", ".\n\n");
        if (otc.GenericName != null)
            MedicationGeneric.Text = otc.GenericName.Replace(", ", "\n");
    }
    
    protected override bool OnBackButtonPressed() {
        Shell.Current.GoToAsync("//MedicationList");
        return true;
    }
}