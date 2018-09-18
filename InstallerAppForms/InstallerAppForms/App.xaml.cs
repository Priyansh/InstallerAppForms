using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using InstallerAppForms.Interface;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace InstallerAppForms
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage()) {
                BarBackgroundColor = Color.FromHex("#AFDAE6"),
                BarTextColor = Color.Black
            };
        }

        protected override void OnStart()
        {
            AppCenter.Start("android=feca7798-445c-4e63-8fd2-2ac71125395c;" + "uwp={Your UWP App secret here};" + "ios={Your iOS App secret here}", typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private static IInstallerAppMethods _callBackFrendelService;
        public static IInstallerAppMethods FrendelSOAPService
        {
            get
            {
                if (_callBackFrendelService == null)
                {
                    _callBackFrendelService = DependencyService.Get<IInstallerAppMethods>();
                }

                return _callBackFrendelService;
            }
        }
    }
}
