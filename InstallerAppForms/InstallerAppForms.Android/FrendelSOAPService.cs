using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using InstallerAppForms.Interface;
using Xamarin.Forms;

[assembly: Dependency(typeof(InstallerAppForms.Droid.FrendelSOAPService))]
namespace InstallerAppForms.Droid
{
    public sealed class FrendelSOAPService : IJobsInstallerList
    {
        FrendelWebService.phonegap FrendelWS;

        public FrendelSOAPService()
        {
            FrendelWS = new FrendelWebService.phonegap()
            {
                Url = "http://ws.frendel.com/mobile/phonegap.asmx"
            };
        }

        public async Task<List<JobsInstallerCS>> GetInstaller(string criteria = null)
        {
            var lstInstallerInfoClass = new List<JobsInstallerCS>();
            return await Task.Run(() =>
            {
                var result = FrendelWS.InsKP_GetInstaller();
                for (int i = 0; i < result.Length; i++)
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
                    lstInstallerInfoClass.Add(fillInstallerProperties);
                }
                return lstInstallerInfoClass;
            });
        }
    }
}
