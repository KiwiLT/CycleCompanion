using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using MySqlConnector;

namespace CycleCompanion
{
    public partial class Inchecken : ContentPage
    {
        public string userName
        {
            get
            {
                return "Kees";
            }
        }
        public Inchecken()
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
        public async void Get_Location(object sender, EventArgs e)
        {
            Location myLocation = null;
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}\n" +
                                      $"Longitude: {location.Longitude}\n" +
                                      $"Altitude: {location.Altitude}\n" +
                                      $"Speed: {location.Speed}\n");
                    myLocation = location;
                    
                }
            } catch (FeatureNotSupportedException fnsEx) { Console.WriteLine("Feature not supported."); }
              catch(FeatureNotEnabledException fneEx) { Console.WriteLine("Feature not enabled."); }
              catch(PermissionException pEx) { Console.WriteLine("Please enable permission to get location."); }
              catch(Exception ex) { Console.WriteLine("Unable to get location"); }
            if (myLocation != null)
            {
                string connectionString = Configuration.getConnectionString();
                var connection = new MySqlConnection(connectionString);
                connection.Open();
                string getlocid = "SELECT MAX(`LocatieID`) FROM `Locaties`";
                MySqlCommand maxloccommand = new MySqlCommand(getlocid, connection);
                MySqlDataReader locreader = maxloccommand.ExecuteReader();
                locreader.Read();
                string deelnemerid = "2";
                string locatieId = "" + (locreader.GetInt32(0) + 1);
                locreader.Close();
                //DateTime tijd = '17:21:38";
                string tijd = DateTime.Now.ToString("HH:mm:ss");
                string query = "INSERT INTO " +
                    "`Locaties`(`DeelnemerID`, `LocatieID`, `Tijd`, `YCoordinaat`, `XCoordinaat`) " +
                    $"VALUES({deelnemerid}, {locatieId}, '{tijd}', {myLocation.Latitude}, {myLocation.Longitude});";
                Console.WriteLine(query);
                //string query = "INSERT INTO `Locaties` (`DeelnemerID`, `LocatieID`, `Tijd`, `YCoordinaat`, `XCoordinaat`) VALUES (1,5,'15:21:38',12.3728,1.8936);";
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = query;
                var reader = await command.ExecuteNonQueryAsync();
                connection.Close();
            }

        }
    }
}
