using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediCompendium.Models;
using MediCompendium.Services;

namespace MediCompendium.Pages;

public partial class FavoritedMedicationList : ContentPage {
    private DbCommands _db { get; set; }
    public ObservableCollection<Medication> Medications { get; set; }
    
    private List<Medication> _MedicationViews;
    
    public FavoritedMedicationList() {
        InitializeComponent();
        _db = new DbCommands();
        Medications = new ObservableCollection<Medication>();
        BindingContext = this;
    }

    protected override void OnAppearing() {
        base.OnAppearing();
        GenerateDisplay();
    }

    private async void GenerateDisplay() {
        Medications.Clear();
        var favoriteMedications = await _db.GetFavoriteItems(ProfileSelection.SelectedProfile.Id);
        _MedicationViews = Helper.GenerateMedications(await Helper.FetchFavoriteMedications(favoriteMedications));
        foreach (var medication in _MedicationViews) {
            var search = await _db.SearchFavoriteItem(ProfileSelection.SelectedProfile.Id, medication.ProductNdc);
            if (search.Count > 0) medication.Favorited = true;
            
            Medications.Add(medication);
        }
    }
}