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
                MedicationPackaging.Text = string.Join("\n", prescription.HowSupplied);
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

    private async void ToggleDescriptionVisibility(Object sender, EventArgs e) {
        MedicationDescription.IsVisible = !MedicationDescription.IsVisible;
        
        if (MedicationDescription.IsVisible) return;
        
        await Task.Delay(100);
        await MainScrollView.ScrollToAsync(DescriptionGrid, ScrollToPosition.MakeVisible, false);
    }
    
    private async void ToggleWarningVisibility(Object sender, EventArgs e) {
        MedicationWarnings.IsVisible = !MedicationWarnings.IsVisible;
        
        if (MedicationWarnings.IsVisible) return;
        
        await Task.Delay(100);
        await MainScrollView.ScrollToAsync(WarningGrid, ScrollToPosition.MakeVisible, false);
    }
    
    private async void ToggleUsageVisibility(Object sender, EventArgs e) {
        MedicationUsage.IsVisible = !MedicationUsage.IsVisible;
        
        if (MedicationUsage.IsVisible) return;
        
        await Task.Delay(100);
        await MainScrollView.ScrollToAsync(UsageGrid, ScrollToPosition.MakeVisible, false);
    }
    
    private async void ToggleDosageVisibility(Object sender, EventArgs e) {
        MedicationDosage.IsVisible = !MedicationDosage.IsVisible;
        
        if (MedicationDosage.IsVisible) return;
        
        await Task.Delay(100);
        await MainScrollView.ScrollToAsync(DosageGrid, ScrollToPosition.MakeVisible, false);
    }
    
    private async void ToggleActiveIngredientsVisibility(Object sender, EventArgs e) {
        MedicationActiveIngredients.IsVisible = !MedicationActiveIngredients.IsVisible;
        
        if (MedicationActiveIngredients.IsVisible) return;
        
        await Task.Delay(100);
        await MainScrollView.ScrollToAsync(ActiveIngredientsGrid, ScrollToPosition.MakeVisible, false);
    }
    
    private async void TogglePackagingVisibility(Object sender, EventArgs e) {
        MedicationPackaging.IsVisible = !MedicationPackaging.IsVisible;
        
        if (MedicationPackaging.IsVisible) return;
        
        await Task.Delay(100);
        await MainScrollView.ScrollToAsync(PackagingGrid, ScrollToPosition.MakeVisible, false);
    }
}