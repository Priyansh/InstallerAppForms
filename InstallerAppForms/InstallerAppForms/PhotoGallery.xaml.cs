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
            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions() { });
            if (photo != null)
            {
                var slPhotos = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal
                };
                var alpha = new Image { Source = ImageSource.FromStream(() => { return photo.GetStream(); }),
                                        WidthRequest = 200,
                                        HeightRequest = 200
                                       };
                if(col == 0) grdLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(200, GridUnitType.Absolute) });
                
                grdLayout.ColumnDefinitions.Add(new ColumnDefinition {
                    Width = new GridLength(200, GridUnitType.Absolute)
                });

                grdLayout.Children.Add(alpha, col, row);
                col++;
                if (col == 2) { col = 0; row++; }
            }
        }
    }
}