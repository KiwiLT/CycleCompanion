using System;
using System.Collections.Generic;
using MySqlConnector;

using Xamarin.Forms;

namespace CycleCompanion
{
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
            entAppName.Text = "";
        }

        public void checkMail(object sender, EventArgs e)
        {
            string connectionString = Configuration.getConnectionString();
            var connection = new MySqlConnection(connectionString);
            string query = "SELECT `Email` FROM `Deelnemers`;";
            connection.Open();
            var cmd = new MySqlCommand(query, connection);
            MySqlDataReader emailreader = cmd.ExecuteReader();
            List<string> emails = new List<string>();
            while (emailreader.Read())
            {
                emails.Add(emailreader.GetString(0));
            }
            emailreader.Close();
            string mymail = entAppName.Text.ToLower();

            if (emails.Contains(mymail))
            {
                string userinfoquery = "SELECT * FROM `Deelnemers` WHERE `Email`= @email;";
                cmd = new MySqlCommand(userinfoquery, connection);
                cmd.Parameters.Add("@email", MySqlDbType.String);
                cmd.Parameters["@email"].Value = mymail;
                MySqlDataReader userinforeader = cmd.ExecuteReader();
                userinforeader.Read();
                Profiel.deelnemerId = userinforeader.GetInt32(0);
                Profiel.nummer = userinforeader.GetInt32(1);
                Profiel.naam = userinforeader.GetString(2);
                Profiel.achternaam = userinforeader.GetString(3);
                Profiel.email = userinforeader.GetString(4);
                try
                {
                    Statistieken.begintijd = (DateTime)userinforeader.GetMySqlDateTime(5);
                }
                catch
                {
                    Statistieken.begintijd = default(DateTime);
                }
                try
                {
                    Statistieken.eindtijd = (DateTime)userinforeader.GetMySqlDateTime(6);
                }
                catch
                {
                    Statistieken.eindtijd = default(DateTime);
                }
                Statistieken.gemsnelheid = userinforeader.GetInt32(7);
                Statistieken.maxsnelheid = userinforeader.GetInt32(8);
                Statistieken.afstand = userinforeader.GetDouble(9);
                Badge.hasBadgeOne = userinforeader.GetInt16(10) == 1;
                userinforeader.Close();
                connection.Close();
                Navigate_Menu(sender, e);
            } else
            {
                connection.Close();
                DisplayAlert("Onjuiste Email", "Voer alstublieft het email address van een deelnemer in.", "Ok");
            }

        }

        public async void Navigate_Menu(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Menu());
        }
    }

}