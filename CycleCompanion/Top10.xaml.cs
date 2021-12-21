using System;
using System.Collections.Generic;
using MySqlConnector;
using Xamarin.Forms;

namespace CycleCompanion
{
    public partial class Top10 : ContentPage
    {
        public static deelnemertop10[] deelnemerlijst;

        public Top10()
        {
            update_data();
            BindingContext = this;
            InitializeComponent();
            
        }

        public string deelnemer1 { get { return deelnemerlijst[0].naam; } }
        public string snelheid1 { get { return deelnemerlijst[0].maxsnelheid + " km/u"; } }
        public string deelnemer2 { get { return deelnemerlijst[1].naam; } }
        public string snelheid2 { get { return deelnemerlijst[1].maxsnelheid + " km/u"; } }
        public string deelnemer3 { get { return deelnemerlijst[2].naam; } }
        public string snelheid3 { get { return deelnemerlijst[2].maxsnelheid + " km/u"; } }
        public string deelnemer4 { get { return deelnemerlijst[3].naam; } }
        public string snelheid4 { get { return deelnemerlijst[3].maxsnelheid + " km/u"; } }
        public string deelnemer5 { get { return deelnemerlijst[4].naam; } }
        public string snelheid5 { get { return deelnemerlijst[4].maxsnelheid + " km/u"; } }
        public string deelnemer6 { get { return deelnemerlijst[5].naam; } }
        public string snelheid6 { get { return deelnemerlijst[5].maxsnelheid + " km/u"; } }
        public string deelnemer7 { get { return deelnemerlijst[6].naam; } }
        public string snelheid7 { get { return deelnemerlijst[6].maxsnelheid + " km/u"; } }
        public string deelnemer8 { get { return deelnemerlijst[7].naam; } }
        public string snelheid8 { get { return deelnemerlijst[7].maxsnelheid + " km/u"; } }
        public string deelnemer9 { get { return deelnemerlijst[8].naam; } }
        public string snelheid9 { get { return deelnemerlijst[8].maxsnelheid + " km/u"; } }
        public string deelnemer10 { get { return deelnemerlijst[9].naam; } }
        public string snelheid10 { get { return deelnemerlijst[9].maxsnelheid + " km/u"; } }


        public void update_data()
        {
            string connectionString = Configuration.getConnectionString();
            var connection = new MySqlConnection(connectionString);
            string top10query = "SELECT CONCAT(`Naam`,' ',`Achternaam`), `MaximaleSnelheid` FROM `Deelnemers` order by `MaximaleSnelheid` desc limit 10;";
            connection.Open();
            MySqlCommand cmd = new MySqlCommand(top10query, connection);
            MySqlDataReader top10reader = cmd.ExecuteReader();
            int i = 0;
            deelnemerlijst = new deelnemertop10[10];
            while (top10reader.Read() && i < 10)
            {
                string naam = top10reader.GetString(0);
                int maxsnelheid = top10reader.GetInt32(1);
                deelnemertop10 mijndeelnemer = new deelnemertop10(naam, maxsnelheid);
                deelnemerlijst[i] = mijndeelnemer;
                i++;
            }
            top10reader.Close();
            connection.Close();
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

    public class deelnemertop10
    {
        public deelnemertop10(string naam, int maxsnelheid)
        {
            this.naam = naam;
            this.maxsnelheid = maxsnelheid;
        }

        public string naam { get; set; }
        public int maxsnelheid { get; set; }
    }
}
