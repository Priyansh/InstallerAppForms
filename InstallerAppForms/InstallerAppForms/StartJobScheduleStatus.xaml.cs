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
    public partial class StartJobScheduleStatus : CustomContentPageBackButton
    {
        int installerId;
        bool hasRoomImage = true;
        JobsInstallerCS SelectedJobItem;
        List<RoomInfoCS> roomInfo;
        public StartJobScheduleStatus(int getInstallerId, JobsInstallerCS selectedJobItem)
        {
            InitializeComponent();
            this.SelectedJobItem = selectedJobItem;
            if (EnableBackButtonOverride)
            {
                this.CustomBackButtonAction = async () =>
                {
                    await Navigation.PopAsync(true);
                };
            }

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
                FunStartingJob(this.SelectedJobItem.InstallerJobStatus);
            }
            BindingContext = this.SelectedJobItem;
            installerId = getInstallerId;
            btnSetCompletedJob.BackgroundColor = Color.FromHex("#088da5");
        }

        private async void btnJobStart_Clicked(object sender, EventArgs e)
        {
            this.SelectedJobItem.InstallerJobStatus = 1;
            await App.FrendelSOAPService.UpdateInstallerStatus(this.SelectedJobItem.CSID, this.SelectedJobItem.InstallerJobStatus);
            var installerByCSID = await App.FrendelSOAPService.GetInstaller(installerId);
            var tempList = installerByCSID.Where(i => i.CSID == this.SelectedJobItem.CSID).ToList();
            this.SelectedJobItem.InstallerJobStart = tempList[0].InstallerJobStart;
            this.SelectedJobItem.ImageJobStatus = tempList[0].ImageJobStatus;
            this.SelectedJobItem.JobCurrentStatus = tempList[0].JobCurrentStatus;
            FunStartingJob(this.SelectedJobItem.InstallerJobStatus);
        }

        public async void FunStartingJob(int jobStatus)
        {
            if(jobStatus == 1)
            {
                btnJobStart.IsVisible = false;
                slJobStartedOn.IsVisible = true;
                gridJobScheduleStatus.IsVisible = true;
                btnSetCompletedJob.IsVisible = true;
                string installerJobStart = string.IsNullOrEmpty(this.SelectedJobItem.InstallerJobStart) ? "" : Convert.ToDateTime(this.SelectedJobItem.InstallerJobStart).ToString("MMM dd, yyyy");
                this.SelectedJobItem.InstallerJobStart = installerJobStart;
                lblStartJob.Text = "Select a Room for more options";
                lblInstallerJobStart.Text = this.SelectedJobItem.InstallerJobStart;

                roomInfo = await App.FrendelSOAPService.GetRoomInfo(this.SelectedJobItem.CSID);

                int columnCount = 0;
                for (int row = 0; columnCount != roomInfo.Count; row++)
                {
                    gridJobScheduleStatus.RowDefinitions.Add(new RowDefinition
                    {
                        Height = 80
                    });

                    for(int col = 0; col < 3; col++)
                    {
                        if(columnCount != roomInfo.Count){
                            var button = new InstallerAppForms.WrappedButton { Text = roomInfo[columnCount].Rooms,  HeightRequest = 60, WidthRequest = 100, TextColor = Color.Black, BackgroundColor = Color.Silver };
                            int countRoomImage = await App.FrendelSOAPService.CountInstallerImages(roomInfo[columnCount].RSNo);
                            if (countRoomImage == 0)
                            {
                                hasRoomImage = false;
                                btnSetCompletedJob.FontAttributes = FontAttributes.Bold;
                                btnSetCompletedJob.TextColor = Color.Black;
                            }
                            button.Clicked += Button_Clicked;
                            gridJobScheduleStatus.Children.Add(button, col, row);
                            columnCount++;
                        }
                    }
                }
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var item = ((Button)sender).Text;
            IEnumerable<RoomInfoCS> filteredRoomInfo = roomInfo.Where(w => w.Rooms == item).ToList();
            IndividualRoomCS individualRoom = new IndividualRoomCS();
            individualRoom.Company = SelectedJobItem.Company;
            individualRoom.Project = SelectedJobItem.Project;
            individualRoom.Lot = SelectedJobItem.Lot;
            individualRoom.CSID = SelectedJobItem.CSID;
            individualRoom.JobNum = SelectedJobItem.JobNum;
            individualRoom.MasterNum = SelectedJobItem.MasterNum;
            individualRoom.InstallAssignDate = SelectedJobItem.InstallAssignDate;
            individualRoom.InstallAssignPerson = SelectedJobItem.InstallAssignPerson;
            individualRoom.ShippedDone = SelectedJobItem.ShippedDone;
            individualRoom.JobNum = SelectedJobItem.JobNum;
            individualRoom.InstallerAssignID = SelectedJobItem.InstallerAssignID;
            individualRoom.InstallerJobStatus = SelectedJobItem.InstallerJobStatus;
            individualRoom.InstallerJobStart = SelectedJobItem.InstallerJobStart;
            individualRoom.InstallerJobComplete = SelectedJobItem.InstallerJobComplete;
            individualRoom.ImageJobStatus = SelectedJobItem.ImageJobStatus;
            individualRoom.JobCurrentStatus = SelectedJobItem.JobCurrentStatus;
            individualRoom.JobStatusTextColor = SelectedJobItem.JobStatusTextColor;

            individualRoom.RSNo = filteredRoomInfo.ElementAt(0).RSNo;
            individualRoom.Rooms = filteredRoomInfo.ElementAt(0).Rooms;
            individualRoom.Style = roomInfo.ElementAt(0).Style;
            individualRoom.Colour = roomInfo.ElementAt(0).Colour;
            individualRoom.Hardware = roomInfo.ElementAt(0).Hardware;
            individualRoom.CounterTop = roomInfo.ElementAt(0).CounterTop;
            var lstPartsInfo = await App.FrendelSOAPService.GetPartInfo(SelectedJobItem.MasterNum,individualRoom.Rooms);
            individualRoom.PartsCount = lstPartsInfo.Count;
            individualRoom.InstallationPhoto = await App.FrendelSOAPService.CountInstallerImages(individualRoom.RSNo);
            await Navigation.PushAsync(new IndividualRoom(installerId, individualRoom));
        }

        private async void btnSetCompletedJob_Clicked(object sender, EventArgs e)
        {
            if (hasRoomImage)
            {
                var option = await DisplayAlert("JobSchedule!!", "Want to complete job?", "Yes", "Cancel");
                if (option) //Success
                {
                    await App.FrendelSOAPService.UpdateInstallerStatus(this.SelectedJobItem.CSID, 2);
                    await Navigation.PopAsync(true);
                }
            }
        }
    }
}