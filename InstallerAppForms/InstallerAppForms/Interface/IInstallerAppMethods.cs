using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace InstallerAppForms.Interface
{
    public interface IInstallerAppMethods
    {
        Task<int> LoginSuccess(string uName, string Pwd);
        
        //Get Methods
        Task<List<JobsInstallerCS>> GetInstaller(int installerId);
        Task<List<RoomInfoCS>> GetRoomInfo(int CSID);
        Task<int> GetPartInfo(string FkNo, string RoomName);
        Task<int> CountInstallerImages(string RoomNo);
        //Update Methods
        Task UpdateInstallerStatus(int CSID, int InstallerJobStatus);
        //Insert Methods
        Task<byte[][]> InsertInstallerImages(int CSID, byte[] InstallerImages, string RoomNo, string RoomName);
    }
}
