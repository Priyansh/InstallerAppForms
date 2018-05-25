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

        private async void btnJobStart_Clicked(object sender, EventArgs e)
        {
            this.SelectedJobItem.InstallerJobStatus = 1;
            await App.FrendelSOAPService.UpdateInstallerStatus(this.SelectedJobItem.CSID, this.SelectedJobItem.InstallerJobStatus);
            var installerByCSID = await App.FrendelSOAPService.GetInstaller(installerId);
            var tempList = installerByCSID.Where(i => i.CSID == this.SelectedJobItem.CSID).ToList();
            this.SelectedJobItem.InstallerJobStart = tempList[0].InstallerJobStart;
            funStartingJob(this.SelectedJobItem.InstallerJobStatus);
        }

        public async void funStartingJob(int jobStatus)
        {
            if(jobStatus == 1)
            {
                btnJobStart.IsVisible = false;
                slJobStartedOn.IsVisible = true;
                gridJobScheduleStatus.IsVisible = true;
                string installerJobStart = string.IsNullOrEmpty(this.SelectedJobItem.InstallerJobStart) ? "" : Convert.ToDateTime(this.SelectedJobItem.InstallerJobStart).ToString("MMM dd, yyyy");
                this.SelectedJobItem.InstallerJobStart = installerJobStart;
                lblStartJob.Text = "Select a Room for more options";
                lblInstallerJobStart.Text = this.SelectedJobItem.InstallerJobStart;

                var roomInfo = await App.FrendelSOAPService.GetRoomInfo(this.SelectedJobItem.CSID);

                for (int row = 0; row < 3; row++)
                {
                    for(int col = 0; col < 3; col++)
                    {
                        var button = new InstallerAppForms.WrappedButton { Text = "Kitchen Drawing is amazing", HeightRequest = 60, WidthRequest = 100, TextColor = Color.Black, BackgroundColor = Color.Silver };
                        gridJobScheduleStatus.Children.Add(button, col, row);
                    }
                }
                
            }
        }
    }
}