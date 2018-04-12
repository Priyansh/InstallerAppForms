using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InstallerAppForms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JobScreen : ContentPage
    {
        int installerId;
        public JobScreen(int getInstallerId)
        {
            InitializeComponent();
            installerId = getInstallerId;
            GetJSON();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            lstJobScreen.BeginRefresh();
            var jobList = await App.FrendelSOAPService.GetInstaller(installerId);
            lstJobScreen.ItemsSource = jobList;
            lstJobScreen.EndRefresh();
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
    }

}
