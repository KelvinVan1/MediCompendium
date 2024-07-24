using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediCompendium.Models;

namespace MediCompendium.Pages;

[QueryProperty("MedicationDetails", "MedicationDetails")]
public partial class OTCDetails : ContentPage {
    private MedicationPrescription _medicationDetails; 
    public MedicationPrescription MedicationDetails {
        get => _medicationDetails;
        set => _medicationDetails = value;
    }
    public OTCDetails() {
        InitializeComponent();
    }
    
    protected override bool OnBackButtonPressed() {
        Shell.Current.GoToAsync("//MedicationList");
        return true;
    }
}