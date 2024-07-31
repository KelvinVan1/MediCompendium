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
        if (_currentMedication is MedicationPrescription prescription) {
            DeaSchedule.Text = prescription.DeaSchedule ?? "DEA Schedule: N/A";
            MedicationLabeler.Text = $"Labeler: {prescription.LabelerName}";
            MedicationName.Text = $"Medication: {prescription.BrandName}";
            if(prescription.ActiveIngredients != null)
                MedicationActiveIngredients.Text = prescription.ActiveIngredientsToString(prescription.ActiveIngredients.Count).Replace(". ", ".\n");
            if(prescription.Description != null) 
                MedicationDescription.Text = prescription.Description[0].Replace(". ",".\n\n");
            if(prescription.Warnings != null) 
                MedicationWarnings.Text = prescription.Warnings[0].Replace(". ", ".\n\n");
            if(prescription.IndicationsAndUsage != null) 
                MedicationUsage.Text = prescription.IndicationsAndUsage[0].Replace(". ", ".\n\n");
            if(prescription.DosageAndAdministration != null) 
                MedicationDosage.Text = prescription.DosageAndAdministration[0].Replace(". ", ".\n\n");
            if (prescription.HowSupplied != null)
                MedicationPackaging.Text = string.Join("\n", prescription.HowSupplied).Replace(". ", ".\n\n");
        }
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

    private async void ToggleVisiblity(Label element, Grid grid) {
        element.IsVisible = !element.IsVisible;
        if(element.IsVisible) return;
        
        await Task.Delay(100);
        await MainScrollView.ScrollToAsync(grid, ScrollToPosition.MakeVisible, false);
    }

    private void ToggleDescriptionVisibility(Object sender, EventArgs e) {
        ToggleVisiblity(MedicationDescription, DescriptionGrid);
    }
    
    private void ToggleWarningVisibility(Object sender, EventArgs e) {
        ToggleVisiblity(MedicationWarnings, WarningGrid);
    }
    
    private void ToggleUsageVisibility(Object sender, EventArgs e) {
        ToggleVisiblity(MedicationUsage, UsageGrid);
    }
    
    private void ToggleDosageVisibility(Object sender, EventArgs e) {
        ToggleVisiblity(MedicationDosage, DosageGrid);
    }
    
    private void ToggleActiveIngredientsVisibility(Object sender, EventArgs e) {
        ToggleVisiblity(MedicationActiveIngredients, ActiveIngredientsGrid);
    }
    
    private void TogglePackagingVisibility(Object sender, EventArgs e) {
        ToggleVisiblity(MedicationPackaging, PackagingGrid);
    }
}