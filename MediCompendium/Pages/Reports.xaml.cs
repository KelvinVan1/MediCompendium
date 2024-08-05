using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediCompendium.Models;
using MediCompendium.Models.DbTables;
using MediCompendium.Services;

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
        var grid = new Grid() {ColumnSpacing = 5};
        
        CreateColumns(grid, 3);
        CreateMedicationColumns(grid);
        
        CreateRows(grid, MedicationList.Count);

        for (var i = 0; i < MedicationList.Count; i++) {
            grid.Add(new Label() {Text = $"{MedicationList[i].BrandName}"}, 0, i + 1);
            grid.Add(new Label() {Text = $"{MedicationList[i].LabelerName}"}, 1, i + 1);
            grid.Add(new Label() {Text = $"{MedicationList[i].ActiveIngredientsToString()}"}, 2, i + 1);
        }
        
        ReportContent.Add(grid);
    }

    private void DisplayOtcMedications() {
        GenerateReportHeading("Saved Over the Counter Medication List");
        var grid = new Grid() {ColumnSpacing = 5};
        
        CreateColumns(grid, 3);
        CreateMedicationColumns(grid);
        
        CreateRows(grid, MedicationList.Count);

        for (var i = 0; i < MedicationList.Count; i++) {
            if (MedicationList[i].ProductType == "HUMAN OTC DRUG") {
                grid.Add(new Label() {Text = $"{MedicationList[i].BrandName}"}, 0, i + 1);
                grid.Add(new Label() {Text = $"{MedicationList[i].LabelerName}"}, 1, i + 1);
                grid.Add(new Label() {Text = $"{MedicationList[i].ActiveIngredientsToString()}"}, 2, i + 1);
            };
        }
        
        ReportContent.Add(grid);
    }

    private void DisplayPrescriptionMedications() {
        GenerateReportHeading("Saved Prescription Medication List");
        var grid = new Grid() {ColumnSpacing = 5};
        
        CreateColumns(grid, 3);
        CreateMedicationColumns(grid);
        
        CreateRows(grid, MedicationList.Count);

        for (var i = 0; i < MedicationList.Count; i++) {
            if (MedicationList[i].ProductType == "HUMAN PRESCRIPTION DRUG") {
                grid.Add(new Label() {Text = $"{MedicationList[i].BrandName}"}, 0, i + 1);
                grid.Add(new Label() {Text = $"{MedicationList[i].LabelerName}"}, 1, i + 1);
                grid.Add(new Label() {Text = $"{MedicationList[i].ActiveIngredientsToString()}"}, 2, i + 1);
            };
        }
        
        ReportContent.Add(grid);
    }

    private void DisplayMedicationCount() {
        GenerateReportHeading("Saved Medication Counts (Prescription and OTC): ");
        
        var otcCount = 0;
        var prescriptionCount = 0;
        
        foreach (var medication in MedicationList) {
            if (medication.ProductType == "HUMAN PRESCRIPTION DRUG") prescriptionCount++;
            else otcCount++;
        }

        var grid = new Grid() {ColumnSpacing = 5};
        
        CreateColumns(grid, 2);
        grid.Add(new Label() {Text = "Over the Counter"}, 0, 0);
        grid.Add(new Label() {Text = "Prescription"}, 1, 0);
        
        CreateRows(grid, 1);
        grid.Add(new Label() {Text = $"{otcCount}"}, 0, 1);
        grid.Add(new Label() {Text = $"{prescriptionCount}"}, 1, 1);
        ReportContent.Add(grid);
    }

    private void CreateColumns(Grid grid, int count) {
        for(var i = 0; i < count; i++) 
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
    }
    
    private void CreateMedicationColumns(Grid grid) {
        grid.Add(new Label() {Text = "Name", Style=(Style)Application.Current.Resources["ReportHeader"]}, 0, 0);
        grid.Add(new Label() {Text = "Labeler", Style=(Style)Application.Current.Resources["ReportHeader"]}, 1, 0);
        grid.Add(new Label() {Text = "Active Ingredient", Style=(Style)Application.Current.Resources["ReportHeader"]}, 2, 0);
    }
    
    private void CreateRows(Grid grid, int count) {
        for(var i = 0; i < count; i++) 
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
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