using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CycleCompanion
{
    public partial class Profiel : ContentPage
    {
<<<<<<< Updated upstream
=======


        public static int deelnemerId { get; set; }
        public static int nummer { get; set; }
        public static string naam { get; set; }
        public static string achternaam { get; set; }
        public static string email { get; set; }

        public string pnaam { get { return naam; } }
        public string pachternaam { get { return achternaam; } }
        public string pemail { get { return email; } }
        public string pnummer { get { return "" + nummer; } }
        public string pvolnaam { get { return naam + " " + achternaam; } }

>>>>>>> Stashed changes
        public Profiel()
        {
            InitializeComponent();
            BindingContext = this;
        }
        public async void Navigate_Main(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new MainPage());
        }
    }
}
