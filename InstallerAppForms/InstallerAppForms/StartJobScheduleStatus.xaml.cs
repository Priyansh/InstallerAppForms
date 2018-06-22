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
        JobsInstallerCS SelectedJobItem;
        List<RoomInfoCS> roomInfo;
        public StartJobScheduleStatus(int getInstallerId, JobsInstallerCS SelectedJobItem)
        {
            InitializeComponent();
            this.SelectedJobItem = SelectedJobItem;
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
            IEnumerable<RoomInfoCS> roomFound = roomInfo.Where(w => w.Rooms == item);
            IndividualRoomCS individualRoom = new IndividualRoomCS();
            individualRoom.RSNo = roomFound.ElementAt(0).RSNo;
            individualRoom.CSID = roomFound.ElementAt(0).CSID;
            individualRoom.Rooms = roomFound.ElementAt(0).Rooms;
            individualRoom.Style = roomFound.ElementAt(0).Style;
            individualRoom.Colour = roomFound.ElementAt(0).Colour;
            individualRoom.Hardware = roomFound.ElementAt(0).Hardware;
            individualRoom.CounterTop = roomFound.ElementAt(0).CounterTop;
            individualRoom.PartsCount = await App.FrendelSOAPService.GetPartInfo(SelectedJobItem.MasterNum,individualRoom.Rooms);
            individualRoom.InstallationPhoto = await App.FrendelSOAPService.CountInstallerImages(individualRoom.RSNo);
            await Navigation.PushAsync(new IndividualRoom(installerId, SelectedJobItem, individualRoom));
        }
    }
}