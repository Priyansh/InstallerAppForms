using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace InstallerAppForms.Interface
{
    public interface IInstallerAppMethods
    {
        Task<int> LoginSuccess(string uName, string Pwd);
        Task<List<JobsInstallerCS>> GetInstaller(int installerId);
    }
}
