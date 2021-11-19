using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CycleCompanion
{
    public partial class Profiel : ContentPage
    {
        public Profiel()
        {
            InitializeComponent();
        }
        public async void Navigate_Menu(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Menu());
        }
    }
}
