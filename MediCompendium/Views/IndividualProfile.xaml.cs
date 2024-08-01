using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediCompendium.Models.DbTables;

namespace MediCompendium.Views;

public partial class IndividualProfile : ContentView {
    public static readonly BindableProperty CurrentProfileProperty = BindableProperty.Create(
        nameof(CurrentProfile),
        typeof(UserProfile),
        typeof(IndividualProfile));
    
    public UserProfile CurrentProfile {
        get => (UserProfile)GetValue(CurrentProfileProperty);
        set => SetValue(CurrentProfileProperty, value);
    }
    public IndividualProfile() {
        InitializeComponent();
    }
}