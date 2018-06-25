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
        JobsInstallerCS SelectedJobItem;
        public JobsInstallerCS MyJobsInstallerCS { get; set; }
        public IndividualRoomCS MyIndividualRoomCS { get; set; }
        public IndividualRoom (int getInstallerId, IndividualRoomCS individualRoom)
		{
			InitializeComponent();
            installerId = getInstallerId;
            //this.SelectedJobItem = SelectedJobItem;
            if (EnableBackButtonOverride)
            {
                this.CustomBackButtonAction = async () =>
                {
                    await Navigation.PopAsync(true);
                };
            }
            if (individualRoom is null) return;

            //MyJobsInstallerCS = DependencyService.Get<JobsInstallerCS>();
            //MyIndividualRoomCS = DependencyService.Get<IndividualRoomCS>();
            
            BindingContext = individualRoom;
            //RoomLabel.SetBinding(Label.TextProperty, new Binding(""));
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            //lstJobScreen.BeginRefresh();
            //JobListRefreshing();
        }
    }
}