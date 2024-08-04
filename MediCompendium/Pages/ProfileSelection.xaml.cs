using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediCompendium.Models;
using MediCompendium.Models.DbTables;
using MediCompendium.Services;

namespace MediCompendium.Pages;

public partial class ProfileSelection : ContentPage {
    public ObservableCollection<UserProfile> UserProfiles { get; set; }
    private DbCommands? _db;
    
    public ProfileSelection() {
        InitializeComponent();
        UserProfiles = new ObservableCollection<UserProfile>();
        _db = new DbCommands();
        BindingContext = this;
    }

    protected override void OnAppearing() {
        base.OnAppearing();
        GenerateProfiles();
    }

    private async void GenerateProfiles() {
        var profiles = await _db.GetProfiles();
        UserProfiles.Clear();
        foreach (var profile in profiles) {
            UserProfiles.Add(profile);
        }
    }
    
    private void OnCreateProfileClicked(object sender, EventArgs e) {
        Shell.Current.GoToAsync("//ProfileCreation");
    }
}