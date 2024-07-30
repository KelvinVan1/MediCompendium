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
            MedicationLabeler.Text = prescription.LabelerName;
            MedicationName.Text = prescription.BrandName;
            if(prescription.Description != null) MedicationDescription.Text = prescription.Description[0].Replace(". ",".\n\n");
            MedicationUsage.Text = prescription.Warnings[0].Replace(". ", ".\n\n");
        }
    }
    
    protected override bool OnBackButtonPressed() {
        Shell.Current.GoToAsync("//MedicationList");
        return true;
    }

    private async void ToggleDescriptionVisibility(Object sender, EventArgs e) {
        MedicationDescription.IsVisible = !MedicationDescription.IsVisible;
        
        if (MedicationDescription.IsVisible) return;
        
        await Task.Delay(100);
        await MainScrollView.ScrollToAsync(MedicationDescription, ScrollToPosition.MakeVisible, false);
    }
}