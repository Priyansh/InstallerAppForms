using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Media;
using System.IO;
using Rg.Plugins.Popup.Services;

namespace InstallerAppForms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PhotoGallery : CustomContentPageBackButton
    {
        int installerId;
        IndividualRoomCS selectedIndividualRoom;
        int row = 0;  int col = 0, CSID, TotalImages = 0;
        string RoomNo, RoomName;
        
        /*ActivityIndicator indicator = new ActivityIndicator
        {
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.FillAndExpand,
            Color = Color.White,
            BackgroundColor = Color.Gray,
            IsVisible = false
        };*/

        public PhotoGallery(int getInstallerId, IndividualRoomCS individualRoom)
        {
            InitializeComponent();
            installerId = getInstallerId;
            this.selectedIndividualRoom = individualRoom;
            CSID = this.selectedIndividualRoom.CSID;
            RoomNo = this.selectedIndividualRoom.RSNo;
            RoomName = this.selectedIndividualRoom.Rooms;

            if (EnableBackButtonOverride)
            {
                this.CustomBackButtonAction = async () =>
                {
                    await Navigation.PopAsync(true);
                };
            }

            if (this.selectedIndividualRoom is null) return;

            BindingContext = this.selectedIndividualRoom;

            GetInstallerImages();
        }
        
        private async void BtnPhoto_Clicked(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera avaialble.", "OK");
                return;
            }

            try
            {
                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    SaveToAlbum = true,
                    AllowCropping = true
                });
                //Get the public album path
                var aPpath = file.AlbumPath;

                //Get private path
                var path = file.Path;

                if (file == null) return;

                /*var alpha = new Image
                {
                    Source = ImageSource.FromStream(() => { return file.GetStream(); }),
                    Aspect = Aspect.AspectFit
                };*/

                indicator.IsRunning = true;
                indicator.IsVisible = true;
                //grdLayout.Children.Add(indicator);
                //Grid.SetColumnSpan(indicator, 2);

                var alpha = new Image();
                using (var memoryStream = new MemoryStream())
                {
                    file.GetStream().CopyTo(memoryStream);
                    file.Dispose();

                    //Insert photos in DB and fetch info from DB at same time
                    //Store images in byte array, then upload to Grid
                    var item = memoryStream.ToArray();
                    alpha.Source = ImageSource.FromStream(() => new MemoryStream(item));
                    await App.FrendelSOAPService.InsertInstallerImages(CSID, memoryStream.ToArray(), RoomNo, RoomName);
                }

                indicator.IsRunning = false;
                indicator.IsVisible = false;

                TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer
                {
                    Command = new Command(OnGridImageTapped),
                    CommandParameter = alpha
                };
                alpha.GestureRecognizers.Add(tapGestureRecognizer);

                if (col == 0) grdLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(200, GridUnitType.Absolute) });

                grdLayout.ColumnDefinitions.Add(new ColumnDefinition
                {
                    Width = new GridLength(200, GridUnitType.Absolute)
                });

                grdLayout.Children.Add(alpha, col, row);
                col++;
                if (col == 2) { col = 0; row++; }
            }
            catch(Exception ex)
            {
                return;   
            }
            
        }

        public async void GetInstallerImages(){
            indicator.IsRunning = true;
            indicator.IsVisible = true;
            //grdLayout.HorizontalOptions = LayoutOptions.FillAndExpand;
            //grdLayout.VerticalOptions = LayoutOptions.FillAndExpand;
            //grdLayout.Children.Add(indicator);
            //Grid.SetColumnSpan(indicator, 2);

            byte[][] lstByteArryImages = await App.FrendelSOAPService.GetInstallerImages(RoomNo);
            TotalImages = lstByteArryImages.Count();

            foreach(var imageBytes in lstByteArryImages){
                var newImage = new Image
                {
                    Source = ImageSource.FromStream(() => new MemoryStream(imageBytes)),
                    Aspect = Aspect.AspectFit
                };
                TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer
                {
                    Command = new Command(OnGridImageTapped),
                    CommandParameter = newImage
                };
                newImage.GestureRecognizers.Add(tapGestureRecognizer);

                if (col == 0) grdLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(200, GridUnitType.Absolute) });

                grdLayout.ColumnDefinitions.Add(new ColumnDefinition
                {
                    Width = new GridLength(200, GridUnitType.Absolute)
                });

                grdLayout.Children.Add(newImage, col, row);
                col++;
                if (col == 2) { col = 0; row++; }

            }

            //Calculation for Rows
            if (TotalImages % 2 == 0) row = TotalImages / 2;
            else { 
                row = (TotalImages / 2) + 1;
                col = 1;
                row--; //row always starts from 0, so if row = 2 it means starts from (0, 1) = 2 , same like index
            }
            indicator.IsRunning = false;
            indicator.IsVisible = false;
            btnCamera.IsEnabled = true;
        } //End of Method

        private async void OnGridImageTapped(object obj)
        {
            Image img = obj as Image;
            //img = obj as Image;
            //img.WidthRequest = 400;
            //img.HeightRequest = 500;
            img.Aspect = Aspect.AspectFit;

            await PopupNavigation.Instance.PushAsync(new ImagePopUp(new Image { Source = img.Source }));
            //await Navigation.PushPopupAsync(new ImagePopUp(img));
        }
    }
}