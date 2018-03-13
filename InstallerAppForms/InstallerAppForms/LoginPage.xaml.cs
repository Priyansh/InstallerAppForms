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
        }

        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            if (!(string.IsNullOrEmpty(txtUserName.Text) || string.IsNullOrEmpty(txtPassword.Text)))
            {
                var installerId = await App.FrendelSOAPService.LoginSuccess(txtUserName.Text, txtPassword.Text);
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
