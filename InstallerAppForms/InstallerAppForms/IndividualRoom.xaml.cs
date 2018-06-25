﻿using System;
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
            await Navigation.PushAsync(new PhotoGallery(installerId, this.selectedIndividualRoom));
        }
    }
}