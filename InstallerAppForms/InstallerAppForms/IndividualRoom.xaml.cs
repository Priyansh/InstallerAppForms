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
        public IndividualRoom (int getInstallerId, JobsInstallerCS SelectedJobItem)
		{
			InitializeComponent();
            installerId = getInstallerId;
            this.SelectedJobItem = SelectedJobItem;
            if (EnableBackButtonOverride)
            {
                this.CustomBackButtonAction = async () =>
                {
                    await Navigation.PopAsync(true);
                };
            }
            if (this.SelectedJobItem is null) return;
            BindingContext = this.SelectedJobItem;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            //lstJobScreen.BeginRefresh();
            //JobListRefreshing();
        }
    }
}