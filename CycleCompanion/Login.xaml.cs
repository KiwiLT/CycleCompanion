using System;
using System.Collections.Generic;

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
        public async void Navigate_Inchecken(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Inchecken());
        }
    }

}