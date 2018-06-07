using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InstallerAppForms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JobScreen : CustomContentPageBackButton
    {
        int installerId;
        List<JobsInstallerCS> jobList;
        public JobScreen(int getInstallerId)
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
            SizeChanged += JobScreen_SizeChanged;
            GetJSON();
        }

        private void JobScreen_SizeChanged(object sender, EventArgs e)
        {
            if( Height < Width)
            {
                //In Landscape Mode
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            lstJobScreen.BeginRefresh();
            JobListRefreshing();
        }

        //Method for calling REST API
        public void GetJSON()
        {
            //Check network status
            /*if (NetworkCheckCS.IsInternet())
            {
                var client = new System.Net.Http.HttpClient();
                var response = await client.GetAsync("Replace ur JSON Url");
                string jobsJSON = response.Content.ReadAsStringAsync().Result; //Getting response
                var lstObjJobScreen = new JobScreenList();
                if (jobsJSON != "")
                {
                    lstObjJobScreen = JsonConvert.DeserializeObject<JobScreenList>(jobsJSON);
                }
            }
            else
            {
                await DisplayAlert("JSONParsing", "No network is available.", "Ok");
            }*/
        }

        private async void lstJobScreen_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var JobsInstallerCS = e.Item as JobsInstallerCS;
            await Navigation.PushAsync(new StartJobScheduleStatus(installerId, JobsInstallerCS));
        }

        private async void JobListRefreshing()
        {
            jobList = await App.FrendelSOAPService.GetInstaller(installerId);
            lstJobScreen.ItemsSource = jobList;
            lstJobScreen.EndRefresh();
        }
    }

}
