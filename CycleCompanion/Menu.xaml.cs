using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CycleCompanion
{
    public partial class Menu : ContentPage
    {
        public Menu()
        {
            BindingContext = this;
            InitializeComponent();
            
        }
        public async void Navigate_Inchecken(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Inchecken());
        }
        public async void Navigate_Statistieken(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Statistieken());
        }
        public string userName
        {
            get
            {
                return "Kees";
            }
        }
    }
}