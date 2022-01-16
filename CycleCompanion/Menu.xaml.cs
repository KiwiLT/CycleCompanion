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
        public async void Navigate_Profiel(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Profiel());
        }
        private async void Navigate_Login(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new Login());
        }
        private async void Navigate_Top10(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new Top10());
        }
        private async void Navigate_Badge(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new Badge());
        }
        private async void Navigate_Geschiedenis(System.Object sender, System.EventArgs e)
        {
            Console.WriteLine("Naar geschiedenis");
            await Navigation.PushModalAsync(new Geschiedenis());
        }


        public string userName
        {
            get
            {
                return Profiel.naam;
            }
        }

        public string nummer { get { return "" + Profiel.nummer; } }
    }
}