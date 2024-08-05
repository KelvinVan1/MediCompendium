using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediCompendium.Models;
using MediCompendium.Models.DbTables;
using MediCompendium.Pages;
using MediCompendium.Services;

namespace MediCompendium.Views;

public partial class MedicationItem : ContentView {
    public static readonly BindableProperty CurrentMedicationProperty = BindableProperty.Create(
        nameof(CurrentMedication),
        typeof(Medication),
        typeof(MedicationItem));

    public Medication CurrentMedication {
        get => (Medication)GetValue(CurrentMedicationProperty);
        set => SetValue(CurrentMedicationProperty, value);
    }
    
    private DbCommands _db { get; set; }

    public MedicationItem() {
        InitializeComponent();
        _db = new DbCommands();
        BindingContextChanged += OnBindingContextChanged;
    }
    
    private void OnBindingContextChanged(object sender, EventArgs e) {
        if (BindingContext is Medication medication)
            ActiveIngredientLabel.Text = medication.ActiveIngredientsToString(3);
    }
    
    private async void OnFavoriteTapped(object sender, EventArgs e) {
        var currentImage = FavoriteHeart.Source.ToString();
        
        if (CurrentMedication.ProductNdc == null) return;
        if (string.IsNullOrEmpty(ProfileSelection.SelectedProfile.Id.ToString())) return;
        
        var favorite = new FavoritedItem() {
            ProductNdc = CurrentMedication.ProductNdc,
            ProfileId = ProfileSelection.SelectedProfile.Id.ToString()
        };
        if (currentImage.Contains("heart.png")) {
            FavoriteHeart.Source = "heart_filled.png";
            await _db.AddFavorite(favorite);
        }
        else {
            FavoriteHeart.Source = "heart.png";
            await _db.RemoveFavorite(favorite);
        }
    }
    private async void OnMedicationTapped(object sender, EventArgs e) {
        if (CurrentMedication.ProductType == "HUMAN PRESCRIPTION DRUG")
            await Shell.Current.GoToAsync($"//PrescriptionDetails?MedicationNdc={CurrentMedication.ProductNdc}");
        else
            await Shell.Current.GoToAsync($"//OtcDetails?MedicationNdc={CurrentMedication.ProductNdc}");
    }
    
}