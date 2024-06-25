using MediCompendium.Services;

namespace MediCompendium;

public partial class MainPage : ContentPage {
    public MainPage() {
        InitializeComponent();
    }

    private async void OnContinueClicked(object sender, EventArgs e) {
        await Shell.Current.GoToAsync("//MedicationList");
    }
}