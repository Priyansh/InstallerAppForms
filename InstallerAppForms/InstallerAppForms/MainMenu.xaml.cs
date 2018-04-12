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
    public partial class MainMenu : ContentPage
    {
        int installerId;
        public MainMenu(int getInstallerId)
        {
            InitializeComponent();
            installerId = getInstallerId;
        }

        void btnJobs_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new JobScreen(installerId));
        }
    }
}