using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CycleCompanion
{
    public partial class Geschiedenis : ContentPage
    {
        public Geschiedenis()
        {
            InitializeComponent();
        }
        public async void Navigate_Menu(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Menu());
        }
        public async void Navigate_Profiel(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Profiel());
        }
        public async void Navigate_Badge(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Badge());
        }
    }
}

