using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediCompendium.Models;

namespace MediCompendium.Views;

public partial class MedicationItemReport : ContentView {
    public static readonly BindableProperty CurrentMedicationProperty = BindableProperty.Create(
        nameof(CurrentMedication),
        typeof(Medication),
        typeof(MedicationItem),
        propertyChanged: OnCurrentMedicationChanged);

    public Medication CurrentMedication {
        get => (Medication)GetValue(CurrentMedicationProperty);
        set => SetValue(CurrentMedicationProperty, value);
    }

    public MedicationItemReport() {
        InitializeComponent();
        BindingContextChanged += OnBindingContextChanged;
    }
    
    private void OnBindingContextChanged(object sender, EventArgs e) {
        if (BindingContext is Medication medication)
            ActiveIngredientLabel.Text = medication.ActiveIngredientsToString(2);
    }
    
    private static void OnCurrentMedicationChanged(BindableObject bindable, object oldValue, object newValue) {
        var control = (MedicationItemReport)bindable;
        control.BindingContext = newValue;
    }
}