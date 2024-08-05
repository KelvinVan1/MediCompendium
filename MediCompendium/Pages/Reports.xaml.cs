using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediCompendium.Models;
using MediCompendium.Models.DbTables;
using MediCompendium.Services;
using MediCompendium.Views;

namespace MediCompendium.Pages;

public partial class Reports : ContentPage {
    private DbCommands _db;

    private List<FavoritedItem>? FavoriteMedications { get; set; }
    private List<Medication>? MedicationList { get; set; }
    
    public Reports() {
        InitializeComponent();
        _db = new DbCommands();
    }

    protected override async void OnAppearing() {
        base.OnAppearing();
        
        ReportContent.Children.Clear();
        if(FavoriteMedications != null) FavoriteMedications.Clear();
        if(MedicationList != null) MedicationList.Clear();
        
        FavoriteMedications = await _db.GetFavoriteItems(ProfileSelection.SelectedProfile.Id);
        MedicationList = Helper.GenerateMedications(await Helper.FetchFavoriteMedications(FavoriteMedications));
    }

    private void GenerateReport(object sender, EventArgs e) {
        ReportContent.Children.Clear();
        if (SavedMedications.IsChecked) DisplaySavedMedications();
        if (SavedOtcMedications.IsChecked) DisplayOtcMedications();
        if (SavedPrescriptionMedications.IsChecked) DisplayPrescriptionMedications();
        if (MedicationCount.IsChecked) DisplayMedicationCount();
    }

    private void DisplaySavedMedications() {
        GenerateReportHeading("Saved Medication List");
        
        foreach (var medication in MedicationList) {
            var reportItem = new MedicationItemReport() {
                CurrentMedication = medication
            };
            ReportContent.Children.Add(reportItem);
        }
    }

    private void DisplayOtcMedications() {
        GenerateReportHeading("Saved Over the Counter Medication List");
        
        foreach (var medication in MedicationList) {
            if (medication.ProductType == "HUMAN OTC DRUG") {
                var reportItem = new MedicationItemReport() {
                    CurrentMedication = medication
                };
                ReportContent.Children.Add(reportItem);
            };
        }
    }

    private void DisplayPrescriptionMedications() {
        GenerateReportHeading("Saved Prescription Medication List");
        
        foreach (var medication in MedicationList) {
            if (medication.ProductType == "HUMAN PRESCRIPTION DRUG") {
                var reportItem = new MedicationItemReport() {
                    CurrentMedication = medication
                };
                ReportContent.Children.Add(reportItem);
            };
        }
    }

    private void DisplayMedicationCount() {
        GenerateReportHeading("Saved Medication counts (prescription and OTC): ");
        
        var otcCount = 0;
        var prescriptionCount = 0;
        
        foreach (var medication in MedicationList) {
            if (medication.ProductType == "HUMAN PRESCRIPTION DRUG") prescriptionCount++;
            else otcCount++;
        }

        var otcLabel = new Label();
        otcLabel.Text = $"Saved Over the Counter Medications: {otcCount}";
        var prescriptionLabel = new Label();
        prescriptionLabel.Text = $"Saved Prescription Medications: {prescriptionCount}";
        
        ReportContent.Children.Add(otcLabel);
        ReportContent.Children.Add(prescriptionLabel);
    }

    private void GenerateReportHeading(string message) {
        ReportContent.Children.Add(new Label() {
            Text = message, 
            Style=(Style)Application.Current.Resources["Headline"]
        });
        ReportContent.Children.Add(new Label() {
            Text = $"Report Generated on: {DateTime.Now}"
        });
        ReportContent.Children.Add(new BoxView {
            HeightRequest = 3,
        });
    }
}