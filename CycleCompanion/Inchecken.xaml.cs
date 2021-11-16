using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;

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

        public async void Get_Location(object sender, EventArgs e)
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}\n" +
                                      $"Longitude: {location.Longitude}\n" +
                                      $"Altitude: {location.Altitude}\n" +
                                      $"Speed: {location.Speed}\n");
                }
            } catch (FeatureNotSupportedException fnsEx) { Console.WriteLine("Feature not supported."); }
              catch(FeatureNotEnabledException fneEx) { Console.WriteLine("Feature not enabled."); }
              catch(PermissionException pEx) { Console.WriteLine("Please enable permission to get location."); }
              catch(Exception ex) { Console.WriteLine("Unable to get location"); }

            Console.WriteLine("My location");
        }
    }
}
