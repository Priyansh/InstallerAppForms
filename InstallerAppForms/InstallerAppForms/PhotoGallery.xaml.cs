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
            if (photo != null) {
                PhotoImage.Source = ImageSource.FromStream(() => { return photo.GetStream(); });
                PhotoImage1.Source = ImageSource.FromStream(() => { return photo.GetStream(); });
            }
        }
    }
}