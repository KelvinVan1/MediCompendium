using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediCompendium.Models;
using MediCompendium.Models.DbTables;

namespace MediCompendium.Pages;

public partial class ProfileSelection : ContentPage {
    public ObservableCollection<UserProfile> UserProfiles { get; set; }
    
    public ProfileSelection() {
        InitializeComponent();
    }

    private void OnCreateProfileClicked(object sender, EventArgs e) {
        Shell.Current.GoToAsync("//ProfileCreation");
    }
}