using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediCompendium.Models;
using MediCompendium.Services;

namespace MediCompendium.Pages;


[QueryProperty("MedicationNdc", "MedicationNdc")]
public partial class PrescriptionDetails : ContentPage {
    private string _medicationNdc;
    private Medication _currentMedication;
    public string MedicationNdc {
        get => _medicationNdc;
        set {
            _medicationNdc = value;
            FetchMedicationData(); 
        }
    }

    public PrescriptionDetails() {
        InitializeComponent();
    }

    private async void FetchMedicationData() {
        _currentMedication = await Helper.FetchMedication(_medicationNdc);
        PopulatePage();
    }
    
    private void PopulatePage() {
        if (!(_currentMedication is MedicationPrescription prescription)) return;
        
        DeaSchedule.Text = prescription.DeaSchedule ?? "DEA Schedule: N/A";
        MedicationLabeler.Text = $"Labeler: {prescription.LabelerName}";
        MedicationName.Text = $"Medication: {prescription.BrandName}";
        
        if(prescription.ActiveIngredients != null)
            MedicationActiveIngredients.Text = prescription.ActiveIngredientsToString();
        if(prescription.Description != null) 
            MedicationDescription.Text = prescription.Description[0].Replace(". ",".\n\n");
        if(prescription.WarningsAndCautions != null) 
            MedicationWarnings.Text = prescription.WarningsAndCautions[0].Replace(". ", ".\n\n");
        if(prescription.Warnings != null) 
            MedicationWarnings.Text = prescription.Warnings[0].Replace(". ", ".\n\n");
        if(prescription.IndicationsAndUsage != null) 
            MedicationUsage.Text = prescription.IndicationsAndUsage[0].Replace(". ", ".\n\n");
        if(prescription.DosageAndAdministration != null) 
            MedicationDosage.Text = prescription.DosageAndAdministration[0].Replace(". ", ".\n\n");
        if (prescription.HowSupplied != null)
            MedicationPackaging.Text = string.Join("\n", prescription.HowSupplied).Replace(". ", ".\n\n");
    }
    
    protected override bool OnBackButtonPressed() {
        MedicationDescription.IsVisible = false;
        MedicationWarnings.IsVisible = false;
        MedicationDosage.IsVisible = false;
        MedicationPackaging.IsVisible = false;
        MedicationActiveIngredients.IsVisible = false;
        MedicationUsage.IsVisible = false;
        Shell.Current.GoToAsync("//MedicationList");
        return true;
    }

    private async void ToggleVisiblity(Label element, Grid grid, Button button) {
        element.IsVisible = !element.IsVisible;
        if (element.IsVisible) {
            button.Text = "Hide Details";
            return;
        }
        
        button.Text = "Show Details";
        await Task.Delay(100);
        await MainScrollView.ScrollToAsync(grid, ScrollToPosition.MakeVisible, false);
    }

    private void ToggleDescriptionVisibility(Object sender, EventArgs e) {
        ToggleVisiblity(MedicationDescription, DescriptionGrid, MedicationDescriptionButton);
    }
    
    private void ToggleWarningVisibility(Object sender, EventArgs e) {
        ToggleVisiblity(MedicationWarnings, WarningGrid, MedicationWarningButton);
    }
    
    private void ToggleUsageVisibility(Object sender, EventArgs e) {
        ToggleVisiblity(MedicationUsage, UsageGrid, MedicationUsageButton);
    }
    
    private void ToggleDosageVisibility(Object sender, EventArgs e) {
        ToggleVisiblity(MedicationDosage, DosageGrid, MedicationDosageButton);
    }
    
    private void ToggleActiveIngredientsVisibility(Object sender, EventArgs e) {
        ToggleVisiblity(MedicationActiveIngredients, ActiveIngredientsGrid, MedicationActiveIngredientsButton);
    }
    
    private void TogglePackagingVisibility(Object sender, EventArgs e) {
        ToggleVisiblity(MedicationPackaging, PackagingGrid, MedicationPackagingButton);
    }
}