using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InstallerAppForms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMenu : CustomContentPageBackButton
    {
        int installerId;
        public MainMenu(int getInstallerId)
        {
            InitializeComponent();
            installerId = getInstallerId;
            GetInstallerName();
        }

        public async void GetInstallerName()
        {
            lblInstallerName.Text = await App.FrendelSOAPService.GetInstallerCompany(installerId);
        }

        void btnJobs_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new JobScreen(installerId));
        }
    }
}