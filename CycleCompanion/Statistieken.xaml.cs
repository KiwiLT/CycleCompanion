using System;
using System.Collections.Generic;
using MySqlConnector;
using Xamarin.Forms;

namespace CycleCompanion
{
    public partial class Statistieken : ContentPage
    {
        public static DateTime begintijd { get; set; }
        public static DateTime eindtijd { get; set; }
        public static int gemsnelheid { get; set; }
        public static int maxsnelheid { get; set; }
        public static double afstand { get; set; }

        public string sbegintijd
        {
            get { if (begintijd != default(DateTime)) { return begintijd.ToString("HH:mm"); } else { return "..."; } }
        }

        public string seindtijd
        {
            get { if (eindtijd != default(DateTime)) { return eindtijd.ToString("HH:mm"); } else { return "..."; } }
        }

        public string fietstijd
        {
            get {
                if (begintijd != default(DateTime) && eindtijd == default(DateTime))
                { return DateTime.Now.Subtract(begintijd).ToString("hh\\:mm\\:ss") ; }
                else if (begintijd != default(DateTime) && eindtijd != default(DateTime))
                {
                    return eindtijd.Subtract(begintijd).ToString("hh\\:mm\\:ss");
                }
                else { return "..."; }
            }
        }

        public string nummer
        {
            get
            {
                return "" + Profiel.nummer;
            }
        }

        public Statistieken()
        {
            BindingContext = this;
            InitializeComponent();
        }

        public async void Navigate_Main(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new MainPage());
        }
        public async void Navigate_Menu(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Menu());
        }
        public async void Navigate_Profiel(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Profiel());
        }
    }
}
