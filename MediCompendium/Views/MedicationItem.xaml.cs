using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediCompendium.Models;
using MediCompendium.Pages;

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

    public MedicationItem() {
        InitializeComponent();
        BindingContextChanged += OnBindingContextChanged;
    }
    
    private void OnBindingContextChanged(object sender, EventArgs e) {
        if (BindingContext is Medication medication)
            ActiveIngredientLabel.Text = medication.ActiveIngredientsToString(3);
    }
    
    private void OnFavoriteTapped(object sender, EventArgs e) {
        var currentImage = FavoriteHeart.Source.ToString();
        FavoriteHeart.Source = currentImage.Contains("heart.png") ? "heart_filled.png" : "heart.png";
    }
    private async void OnMedicationTapped(object sender, EventArgs e) {
        if (CurrentMedication.ProductType == "HUMAN PRESCRIPTION DRUG")
            await Shell.Current.GoToAsync($"//PrescriptionDetails?MedicationDetails={CurrentMedication.ProductNdc}");
        else
            await Shell.Current.GoToAsync($"//OtcDetails?MedicationDetails={CurrentMedication.ProductNdc}");
    }
    
}