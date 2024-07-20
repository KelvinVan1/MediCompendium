using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediCompendium.Models;

namespace MediCompendium.Views;

public partial class MedicationItem : ContentView {

    public static readonly BindableProperty MedicationProperty =
        BindableProperty.Create(nameof(Medication), typeof(Medication), typeof(MedicationItem));

    public Medication Medication {
        get { return (Medication)GetValue(MedicationProperty); }
        set { SetValue(MedicationProperty, value); }
    }
    
    public MedicationItem() {
        InitializeComponent();
    }

    private void OnFavoriteTapped(object sender, EventArgs e) {
        var currentImage = FavoriteHeart.Source.ToString();
        FavoriteHeart.Source = currentImage.Contains("heart.png") ? "heart_filled.png" : "heart.png";
    }
    protected override void OnPropertyChanged(string propName = null) {
        base.OnPropertyChanged(propName);
        if (propName == MedicationProperty.PropertyName)
            BindingContext = Medication;
    }
}