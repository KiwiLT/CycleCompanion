using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CycleCompanion
{
    public partial class MainPage : ContentPage
    {
        public string userName
        {
            get
            {
                return "Kees";
            }
        }
        public MainPage()
        {
            BindingContext = this;
            InitializeComponent();
        }

        private async void Navigate_Inchecken(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new Inchecken());
        }

        private async void Navigate_Statistieken(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new Statistieken());
        }

        private async void Navigate_Login(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new Login());
        }

        private async void Navigate_Menu(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new Menu());
        }
    }
}
