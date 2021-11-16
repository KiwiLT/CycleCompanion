using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CycleCompanion
{
    public partial class Statistieken : ContentPage
    {
        public string nummer
        {
            get
            {
                return "1018958";
            }
        }

        public Statistieken()
        {
            InitializeComponent();
        }

        public async void Navigate_Main(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new MainPage());
        }
    }
}
