using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace InstallerAppForms
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            ProgrssBarLogin.IsVisible = false;
        }

        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            if (!(string.IsNullOrEmpty(txtUserName.Text) || string.IsNullOrEmpty(txtPassword.Text)))
            {
                var installerId = await App.FrendelSOAPService.LoginSuccess(txtUserName.Text, txtPassword.Text);
                ProgrssBarLogin.IsVisible = true;
                ProgrssBarLogin.Progress = 0;
                await ProgrssBarLogin.ProgressTo(0.8, 1000, Easing.Linear);
                if (installerId == 0){ lblErrorMsg.Text = "Invalid UserName/Password"; }
                else { await Navigation.PushAsync(new MainMenu()); }
            }
            else
            {
                lblErrorMsg.Text = "Invalid UserName/Password";
            }
        }
    }
}
