using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CycleCompanion
{
    public partial class Profiel : ContentPage
    {


        public static int deelnemerId { get; set; }
        public static int nummer { get; set; }
        public static string naam { get; set; }
        public static string achternaam { get; set; }
        public static string email { get; set; }

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
