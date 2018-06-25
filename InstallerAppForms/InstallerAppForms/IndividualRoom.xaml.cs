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
        public IndividualRoom (int getInstallerId, IndividualRoomCS individualRoom)
		{
			InitializeComponent();
            installerId = getInstallerId;
            
            if (EnableBackButtonOverride)
            {
                this.CustomBackButtonAction = async () =>
                {
                    await Navigation.PopAsync(true);
                };
            }
            if (individualRoom is null) return;
            
            BindingContext = individualRoom;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            //lstJobScreen.BeginRefresh();
            //JobListRefreshing();
        }

        private async void OnPartsTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainMenu(installerId));
        }

        private async void OnPhotosTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainMenu(installerId));
        }
    }
}