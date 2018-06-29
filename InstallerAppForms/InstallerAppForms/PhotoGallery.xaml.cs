using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Media;
using System.IO;

namespace InstallerAppForms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PhotoGallery : CustomContentPageBackButton
    {
        int installerId;
        IndividualRoomCS selectedIndividualRoom;
        int row = 0;  int col = 0;
        public PhotoGallery(int getInstallerId, IndividualRoomCS individualRoom)
        {
            InitializeComponent();
            installerId = getInstallerId;
            this.selectedIndividualRoom = individualRoom;
            if (EnableBackButtonOverride)
            {
                this.CustomBackButtonAction = async () =>
                {
                    await Navigation.PopAsync(true);
                };
            }

            if (this.selectedIndividualRoom is null) return;

            BindingContext = this.selectedIndividualRoom;
        }
        
        private async void BtnPhoto_Clicked(object sender, EventArgs e)
        {
            byte[] imageAsBytes;
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera avaialble.", "OK");
                return;
            }
            
            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions() { });

            if (file == null) return;

            var alpha = new Image
            {
                Source = ImageSource.FromStream(() => { return file.GetStream(); }),
                WidthRequest = 200,
                HeightRequest = 200
            };

            if (col == 0) grdLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(200, GridUnitType.Absolute) });

            grdLayout.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = new GridLength(200, GridUnitType.Absolute)
            });

            grdLayout.Children.Add(alpha, col, row);
            col++;
            if (col == 2) { col = 0; row++; }

            using (var memoryStream = new MemoryStream())
            {
                file.GetStream().CopyTo(memoryStream);
                file.Dispose();
                imageAsBytes = memoryStream.ToArray();
            }

            //Insert photos in DB and fetch info from DB at same time
            //Store images in bitmap array, then upload to Grid
            int CSID = this.selectedIndividualRoom.CSID;
            string RoomNo = this.selectedIndividualRoom.RSNo;
            string RoomName = this.selectedIndividualRoom.Rooms;

            byte[][] lstByteArryImages;
            lstByteArryImages = await App.FrendelSOAPService.InsertInstallerImages(CSID, imageAsBytes, RoomNo, RoomName);
        }
    }
}