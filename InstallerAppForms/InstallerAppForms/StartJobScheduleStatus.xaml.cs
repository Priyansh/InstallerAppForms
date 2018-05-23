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
        JobsInstallerCS SelectedJobItem;
        public StartJobScheduleStatus(int getInstallerId, JobsInstallerCS SelectedJobItem)
        {
            InitializeComponent();
            this.SelectedJobItem = SelectedJobItem;

            if (this.SelectedJobItem is null) return;
            string shippedDone = string.IsNullOrEmpty(this.SelectedJobItem.ShippedDone) ? "" : Convert.ToDateTime(this.SelectedJobItem.ShippedDone).ToString("MMM dd, yyyy");
            this.SelectedJobItem.ShippedDone = shippedDone;

            if(this.SelectedJobItem.InstallerJobStatus == 0) //InstallerJobStatus = 0, then first start job and do something
            {
                lblStartJob.Text = "Press the button to start the job";
                btnJobStart.IsVisible = true;
            }
            else if(this.SelectedJobItem.InstallerJobStatus == 1) //InstallerJobStatus = 1, means jobs already started
            {
                funStartingJob(this.SelectedJobItem.InstallerJobStatus);
            }
            BindingContext = this.SelectedJobItem;
            installerId = getInstallerId;
        }

        private void btnJobStart_Clicked(object sender, EventArgs e)
        {
            //TODO update JobStart date in database and update value of this.SelectedJobItem.InstallerJobStart
            this.SelectedJobItem.InstallerJobStatus = 1;
            funStartingJob(this.SelectedJobItem.InstallerJobStatus);
        }

        public void funStartingJob(int jobStatus)
        {
            if(jobStatus == 1)
            {
                btnJobStart.IsVisible = false;
                slJobStartedOn.IsVisible = true;
                string installerJobStart = string.IsNullOrEmpty(this.SelectedJobItem.InstallerJobStart) ? "" : Convert.ToDateTime(this.SelectedJobItem.InstallerJobStart).ToString("MMM dd, yyyy");
                this.SelectedJobItem.InstallerJobStart = installerJobStart;
                lblStartJob.Text = "Select a Room for more options";
                //TODO Grid of dynamic buttons
            }
        }
    }
}