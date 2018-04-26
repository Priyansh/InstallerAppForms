using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Foundation;
using UIKit;
using Xamarin.Forms;
using InstallerAppForms.Interface;

[assembly: Dependency(typeof(InstallerAppForms.iOS.FrendelSOAPService))]
namespace InstallerAppForms.iOS
{
    public class FrendelSOAPService : IInstallerAppMethods
    {
        FrendelWebService.phonegap FrendelWS;
        public FrendelSOAPService()
        {
            FrendelWS = new FrendelWebService.phonegap()
            {
                Url = "http://ws.frendel.com/mobile/phonegap.asmx"
            };
        }
        public async Task<int> LoginSuccess(string uName, string Pwd)
        {
            return await Task.Run(() =>
            {
                var result = FrendelWS.InsKP_Login(uName, Pwd);
                int installerId = result;
                return installerId;
            });
        }
        public async Task<List<JobsInstallerCS>> GetInstaller(int installerId)
        {
            var lstInstallerInfoClass = new List<JobsInstallerCS>();
            return await Task.Run(() =>
            {
                var result = FrendelWS.InsKP_GetInstaller(installerId);
                for (int i = 0; i < result.Length; i++)
                {
                    // Only display jobs if InstallerJobStatus != 2
                    if (result[i].InstallerJobStatus != 2)
                    {
                        var fillInstallerProperties = new JobsInstallerCS
                        {
                            Company = result[i].Company,
                            Project = result[i].Project,
                            CSID = result[i].CSID,
                            Lot = result[i].Lot,
                            JobNum = result[i].MasterNum.ToString().Substring(6),
                            MasterNum = result[i].MasterNum,
                            ShippedDone = result[i].ShippedDone,
                            InstallerJobStatus = result[i].InstallerJobStatus,
                            InstallerJobStart = result[i].InstallerJobStart,
                            InstallerJobComplete = result[i].InstallerJobComplete
                        };

                        if (result[i].InstallerJobStatus == 0 || string.IsNullOrEmpty(result[i].InstallerJobStatus.ToString()))
                        {
                            fillInstallerProperties.ImageJobStatus = "Schedule.png";
                            fillInstallerProperties.JobCurrentStatus = "Scheduled";
                            fillInstallerProperties.JobStatusTextColor = "#FF3333";
                        }
                        else if (result[i].InstallerJobStatus == 1)
                        {
                            fillInstallerProperties.ImageJobStatus = "Progress.png";
                            fillInstallerProperties.JobCurrentStatus = "InProgress";
                            fillInstallerProperties.JobStatusTextColor = "#00CC00";
                        }
                        lstInstallerInfoClass.Add(fillInstallerProperties);
                    }
                }
                return lstInstallerInfoClass;
            });
        }
    }
}