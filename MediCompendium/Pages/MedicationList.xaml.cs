using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MediCompendium.Models;
using MediCompendium.Services;

namespace MediCompendium.Pages;

public partial class MedicationList : ContentPage {
    private DbCommands _db { get; set; }
    public ObservableCollection<Medication> Medications { get; set; }
    public ICommand PerformSearch { get; set; }
    
    private List<Medication> _MedicationViews;
    private int _skip = 0;
    private bool _searching = false;
    private string _searchQuery = "";
    
    public MedicationList() {
        InitializeComponent();
        _db = new DbCommands();
        Medications = new ObservableCollection<Medication>();
        PerformSearch = new Command<string>(SearchMedication);
        BindingContext = this;
    }

    protected override void OnAppearing() {
        base.OnAppearing();
        Shell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;
        GenerateDisplay();
    }

    private async void GenerateDisplay() {
        Medications.Clear();
        _MedicationViews = Helper.GenerateMedications(await ApiCommands.FetchMedications(_skip));
        foreach (var medication in _MedicationViews) {
            var search = await _db.SearchFavoriteItem(ProfileSelection.SelectedProfile.Id, medication.ProductNdc);
            if (search.Count > 0) medication.Favorited = true;
            
            Medications.Add(medication);
        }
    }

    private void OnNextClicked(Object sender, EventArgs e) {
        _skip += 5;
        
        if (_searching) SearchMedication(_searchQuery);
        else GenerateDisplay();
    }
    
    private void OnPrevClicked(Object sender, EventArgs e) {
        if (_skip <= 0) return;
        
        _skip -= 5;

        if (_searching) SearchMedication(_searchQuery);
        else GenerateDisplay();
    }

    private void OnResetClicked(Object sender, EventArgs e) {
        _skip = 0;
        _searching = false;
        GenerateDisplay();
    }

    private async void SearchMedication(string query) {
        if(!_searching) _skip = 0;
        
        _searchQuery = query;
        _searching = true;
        
        Medications.Clear();
        _MedicationViews = Helper.GenerateMedications(await ApiCommands.SearchMedication(_searchQuery, _skip));
        foreach (var medication in _MedicationViews) {
            var search = await _db.SearchFavoriteItem(ProfileSelection.SelectedProfile.Id, medication.ProductNdc);
            if (search.Count > 0) medication.Favorited = true;
            
            Medications.Add(medication);
        }
    }
}