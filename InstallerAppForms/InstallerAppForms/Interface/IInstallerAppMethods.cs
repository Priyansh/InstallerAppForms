using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using InstallerAppForms.Models;

namespace InstallerAppForms.Interface
{
    public interface IInstallerAppMethods
    {
        Task<int> LoginSuccess(string uName, string pwd);
        
        //Get Methods
        Task<List<JobsInstallerCS>> GetInstaller(int installerId);
        Task<List<RoomInfoCS>> GetRoomInfo(int CSID);
        Task<List<PartsInfoCS>> GetPartInfo(string fkNo, string roomName);
        Task<Tuple<List<OrderPartsInfoCS>, int>> GetPartIssueList(int partType, int labelNo, int CSID);
        Task<int> CountInstallerImages(string roomNo);
        Task<byte[][]> GetInstallerImages(string roomNo);
        Task<string> GetInstallerCompany(int installerId);
        //Update Methods
        Task UpdateInstallerStatus(int CSID, int installerJobStatus);
        //Insert Methods
        Task<byte[][]> InsertInstallerImages(int CSID, byte[] installerImages, string roomNo, string roomName);
        Task<int[]> InsertPartsOrderIssue(int PartOrderId, int PartIssueListId, int InsertRequest);
    }
}
