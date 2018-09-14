using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using InstallerAppForms.Interface;
using InstallerAppForms.Models;
using Xamarin.Forms;

[assembly: Dependency(typeof(InstallerAppForms.Droid.FrendelSOAPService))]
namespace InstallerAppForms.Droid
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

                        if(result[i].InstallerJobStatus == 0 || string.IsNullOrEmpty(result[i].InstallerJobStatus.ToString()))
                        {
                            fillInstallerProperties.ImageJobStatus = "Schedule.png";
                            fillInstallerProperties.JobCurrentStatus = "Scheduled";
                            fillInstallerProperties.JobStatusTextColor = "#FF3333";
                        }
                        else if(result[i].InstallerJobStatus == 1)
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

        public async Task<List<RoomInfoCS>> GetRoomInfo(int CSID)
        {
            var lstRoomInfo = new List<RoomInfoCS>();
            return await Task.Run(() =>
            {
                var result = FrendelWS.InsKP_GetRoomInfo(CSID);
                for (int i = 0; i < result.Length; i++)
                {
                    var fillRoomInfoProperties = new RoomInfoCS
                    {
                        RSNo = result[i].RSNo,
                        CSID = result[i].CSID,
                        Rooms = result[i].Rooms,
                        Style = result[i].Style,
                        Colour = result[i].Colour,
                        Hardware = result[i].Hardware,
                        CounterTop = result[i].CounterTop
                    };
                    lstRoomInfo.Add(fillRoomInfoProperties);
                }
                return lstRoomInfo;
            });
        }
        public async Task<List<PartsInfoCS>> GetPartInfo(string fkNo, string roomName)
        {
            List<PartsInfoCS> lstPartsInfo = new List<PartsInfoCS>();
            return await Task.Run(() =>
            {
                var result = FrendelWS.InsKP_GetPartInfo(fkNo, roomName);

                foreach (var item in result)
                {
                    var fillPartsInfo = new PartsInfoCS
                    {
                        CSID = item.CSID,
                        CabinetName = item.CabinetName,
                        LFinish = item.LFinish,
                        RFinish = item.RFinish,
                        PartType = item.PartType,
                        LabelNo = item.LabelNo
                    };
                    lstPartsInfo.Add(fillPartsInfo);
                }
                return lstPartsInfo;
            });
        }
        public async Task<List<OrderPartsInfoCS>> GetPartIssueList(int partType, int labelNo, int CSID)
        {
            List<OrderPartsInfoCS> lstOrderPartsInfo = new List<OrderPartsInfoCS>();
            return await Task.Run(() =>
            {
                int partsOrderId = FrendelWS.InsKP_PartsOrder(partType, labelNo, CSID);
                var result = FrendelWS.InsKP_GetPartIssueList(partType, partsOrderId);

                foreach (var item in result)
                {
                    var fillOrderPartsInfo = new OrderPartsInfoCS
                    {
                        PartIssueListId = item.PartIssueListID,
                        PartDescription = item.PartDescription,
                        IsCbSelected = item.IsCbSelected,
                        IsCbEnabled = item.IsCbEnabled
                    };
                    lstOrderPartsInfo.Add(fillOrderPartsInfo);
                }
                return lstOrderPartsInfo;
            });
        }
        public async Task<string> GetInstallerCompany(int installerId)
        {
            return await Task.Run(() =>
            {
                var result = FrendelWS.InsKP_GetInstallerCompany(installerId);
                return result;
            });
        }

        public async Task<int> CountInstallerImages(string RoomNo)
        {
            return await Task.Run(() =>
            {
                var result = FrendelWS.InsKP_CountInstallerImages(RoomNo);
                return result;
            });
        }

        public async Task<byte[][]> GetInstallerImages(string RoomNo)
        {
            return await Task.Run(() =>
            {
                return FrendelWS.insKP_getInstallerImages(RoomNo);
            });
        }

        //------------------------ALL UPDATES METHODS STARTS HERE -------------------------------------
        public async Task UpdateInstallerStatus(int CSID, int InstallerJobStatus)
        {
            await Task.Run(() =>
            {
                FrendelWS.InsKP_UpdateInstallerStatus(CSID, InstallerJobStatus);
            });
        }

        //-----------------------ALL INSERT METHODS STARTS HERE------------------------------------------
        public async Task<byte[][]> InsertInstallerImages(int CSID, byte[] InstallerImages, string RoomNo, string RoomName)
        {
            return await Task.Run(() =>
            {
                return FrendelWS.insKP_InsertInstallerImages(CSID.ToString(), InstallerImages, RoomNo, RoomName);
            });
        }
    }
}
