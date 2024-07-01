using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediCompendium.Models;
using MediCompendium.Services;

namespace MediCompendium.Pages;

public partial class MedicationList : ContentPage {
    public ObservableCollection<Medication> Medications { get; set; }
    private List<Medication> _MedicationViews;
    private int _skip = 0;
    public MedicationList() {
        InitializeComponent();
        Medications = new ObservableCollection<Medication>();
        GenerateDisplay();
        BindingContext = this;
    }

    private async void GenerateDisplay() {
        _MedicationViews = Helper.GenerateMedications(await ApiCommands.FetchMedications(_skip));
        foreach (var medication in _MedicationViews) {
            Medications.Add(medication);
        }
    }

    private void OnNextClicked(Object sender, EventArgs e) {
        Medications.Clear();
        _skip += 5;
        GenerateDisplay();
    }
}