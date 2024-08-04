using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediCompendium.Models.DbTables;
using MediCompendium.Services;

namespace MediCompendium.Pages;

public partial class ProfileCreation : ContentPage {
    private DbCommands db;
    public ProfileCreation() {
        InitializeComponent();
        db = new DbCommands();
    }

    private async void OnSaveClicked(object sender, EventArgs e) {
        if (string.IsNullOrEmpty(Username.Text)) 
            await DisplayAlert(
                "Missing Username", 
                "You must enter a Username", 
                "Ok");
        else if (string.IsNullOrEmpty(FirstName.Text))
            await DisplayAlert(
                "Missing First Name", 
                "You must enter a first name", 
                "Ok");
        else if (string.IsNullOrEmpty(LastName.Text))
            await DisplayAlert(
                "Missing Last Name", 
                "You must enter a last name", 
                "Ok");
        else if (Gender.SelectedIndex == -1)
            await DisplayAlert(
                "Missing Gender", 
                "You must select a gender", 
                "Ok");
        else if (string.IsNullOrEmpty(Age.Text))
            await DisplayAlert(
                "Missing Age", 
                "You must enter an age", 
                "Ok");
        else if (!int.TryParse(Age.Text, out _))
            await DisplayAlert(
                "Invalid Age", 
                "Please enter an age in numeric form (22)", 
                "Ok");
        else {
            var profile = new UserProfile() {
                Username = Username.Text,
                FirstName = FirstName.Text,
                LastName = LastName.Text,
                Gender = Gender.SelectedItem.ToString(),
                Age = int.Parse(Age.Text),
            };
        
            await db.AddProfile(profile);
            await Shell.Current.GoToAsync("//ProfileSelection");
        }
    }

    private void OnCancelClicked(object sender, EventArgs e) {
        Shell.Current.GoToAsync("//ProfileSelection");
    }
}