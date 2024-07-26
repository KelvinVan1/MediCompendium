using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediCompendium.Models;

namespace MediCompendium.Pages;

[QueryProperty("MedicationNdc", "MedicationNdc")]
public partial class OTCDetails : ContentPage {
    private string _medicationNdc; 
    public string MedicationNdc {
        get => _medicationNdc;
        set {
            _medicationNdc = value;
        }
    }
    public OTCDetails() {
        InitializeComponent();
    }
    
    protected override bool OnBackButtonPressed() {
        Shell.Current.GoToAsync("//MedicationList");
        return true;
    }
}