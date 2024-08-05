using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediCompendium.controls;
using MediCompendium.Models.DbTables;
using MediCompendium.Services;

namespace MediCompendium.Pages;

public partial class EditProfile : ContentPage {
    private DbCommands _db;
    
    public EditProfile() {
        InitializeComponent();
        _db = new DbCommands();
    }

    protected override void OnAppearing() {
        base.OnAppearing();
        var profile = ProfileSelection.SelectedProfile;
        Username.Text = profile.Username;
        FirstName.Text = profile.FirstName;
        LastName.Text = profile.LastName;
        Gender.SelectedItem = profile.Gender;
        Age.Text = profile.Age.ToString();
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
                Id = ProfileSelection.SelectedProfile.Id,
                Username = Username.Text,
                FirstName = FirstName.Text,
                LastName = LastName.Text,
                Gender = Gender.SelectedItem.ToString(),
                Age = int.Parse(Age.Text),
            };
        
            await _db.AddProfile(profile);
            ProfileSelection.SelectedProfile = profile;
            var flyoutHeader = (FlyoutHeader)Shell.Current.FlyoutHeader;
            flyoutHeader.CurrentUserText = $"Currently logged in as: {ProfileSelection.SelectedProfile.Username}";
            await Shell.Current.GoToAsync("//MedicationList");
        }
    }

    private void OnCancelClicked(object sender, EventArgs e) {
        Shell.Current.GoToAsync("//MedicationList");
    }

    private async void OnDeleteClicked(object sender, EventArgs e) {
        var delete = await DisplayAlert(
            "Delete Profile",
            "Are you sure you want to delete your profile? This action cannot be reversed.",
            "Yes", "No");
        
        if (!delete) return;
        
        await _db.DeleteProfile(ProfileSelection.SelectedProfile.Id);
        await Shell.Current.GoToAsync("//ProfileSelection");
    }
}