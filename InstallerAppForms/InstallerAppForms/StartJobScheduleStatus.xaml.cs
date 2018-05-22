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
    public partial class StartJobScheduleStatus : ContentPage
    {
        int installerId;
        public StartJobScheduleStatus(int getInstallerId, JobsInstallerCS SelectedJobItem)
        {
            InitializeComponent();
            if (SelectedJobItem is null) return;
            string shippedDone = string.IsNullOrEmpty(SelectedJobItem.ShippedDone) ? "" : Convert.ToDateTime(SelectedJobItem.ShippedDone).ToString("MMM dd, yyyy");
            SelectedJobItem.ShippedDone = shippedDone;
            string installerJobStart = string.IsNullOrEmpty(SelectedJobItem.InstallerJobStart) ? "" : Convert.ToDateTime(SelectedJobItem.InstallerJobStart).ToString("MMM dd, yyyy");
            SelectedJobItem.InstallerJobStart = installerJobStart;

            BindingContext = SelectedJobItem;
            installerId = getInstallerId;
        }
    }
}