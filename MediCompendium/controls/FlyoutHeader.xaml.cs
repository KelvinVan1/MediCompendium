using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediCompendium.Pages;

namespace MediCompendium.controls;

public partial class FlyoutHeader : ContentView {
    public static readonly BindableProperty CurrentUserTextProperty =
        BindableProperty.Create(nameof(CurrentUserText), typeof(string), typeof(FlyoutHeader), string.Empty);

    public string CurrentUserText {
        get => (string)GetValue(CurrentUserTextProperty);
        set => SetValue(CurrentUserTextProperty, value);
    }
    public FlyoutHeader() {
        InitializeComponent();
        BindingContext = this;
    }
}