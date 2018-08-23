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
	public partial class IndividualRoom : CustomContentPageBackButton
    {
        int installerId;
        IndividualRoomCS selectedIndividualRoom;

        public IndividualRoom (int getInstallerId, IndividualRoomCS individualRoom)
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
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            this.selectedIndividualRoom.InstallationPhoto = await App.FrendelSOAPService.CountInstallerImages(this.selectedIndividualRoom.RSNo);
            int TotalPhotos = this.selectedIndividualRoom.InstallationPhoto;
            lblTotalInstallationPics.Text = TotalPhotos.ToString();
            lblFormattedTextTotalPics.Text = "Installation Photos : " + TotalPhotos.ToString();
        }

        private async void OnPartsTapped(object sender, EventArgs e)
        {
            const int _animationTime = 50;
            try
            {
                var layout = (StackLayout)sender;
                await layout.FadeTo(0.5, _animationTime);
                await layout.FadeTo(1, _animationTime);
            }
            catch (Exception ex) { }
            await Navigation.PushAsync(new PartsInfo(installerId, this.selectedIndividualRoom));
        }

        private async void OnPhotosTapped(object sender, EventArgs e)
        {
            const int _animationTime = 50;
            try
            {
                var layout = (StackLayout)sender;
                await layout.FadeTo(0.5, _animationTime);
                await layout.FadeTo(1, _animationTime);
            }
            catch (Exception ex) { }
            await Navigation.PushAsync(new PhotoGallery(installerId, this.selectedIndividualRoom));
        }
    }
}