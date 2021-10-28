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

        private async void Navigate_Profiel(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new Profiel());
        }
    }
}
