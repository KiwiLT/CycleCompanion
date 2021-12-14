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
        public static double gemsnelheid { get; set; }
        public static double huidigesnelheid { get; set; }
        public static double maxsnelheid { get; set; }
        public static double afstand { get; set; }

        public string sbegintijd
        {
            get { if (begintijd != default(DateTime)) { return begintijd.ToString("HH:mm"); } else { return "..."; } }
        }

        public string seindtijd
        {
            get { if (eindtijd != default(DateTime)) { return eindtijd.ToString("HH:mm"); } else { return "..."; } }
        }

        public string shuidigesnelheid
        {
            get { return Math.Round(huidigesnelheid, 2) + " km/u"; }
        }

        public string smaxsnelheid
        {
            get { return Math.Round(maxsnelheid, 2) + " km/u"; }
        }

        public string sgemsnelheid
        {
            get { return Math.Round(gemsnelheid, 2) + " km/u"; }
        }

        public string safstand
        {
            get { return Math.Round(afstand, 2) + " km"; }
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

        public static void Update_Data()
        {
            string connectionString = Configuration.getConnectionString();
            var connection = new MySqlConnection(connectionString);
            connection.Open();
            string beginstring = "";
            string eindstring = "";
            if (begintijd == default(DateTime)) { beginstring = "null"; }
            else { beginstring = begintijd.ToString("yyyy/MM/dd HH:mm:ss"); }
            if (eindtijd == default(DateTime)) { eindstring = "null"; }
            else { eindstring = eindtijd.ToString("yyyy/MM/dd HH:mm:ss"); }
            string query = "UPDATE `Deelnemers` SET `BeginTijd` = '" + beginstring + "', `EindTijd` = '" + eindstring + "', `MaximaleSnelheid` = '" + Statistieken.maxsnelheid + "', `AfstandGefietst` = '" + afstand + "', `GemiddeldeSnelheid` = '" + gemsnelheid + "' WHERE `ID` = '" + Profiel.deelnemerId + "';";
            Console.WriteLine(query);
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = query;
            var reader = command.ExecuteNonQuery();
            connection.Close();
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
