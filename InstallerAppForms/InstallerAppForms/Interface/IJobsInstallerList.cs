using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace InstallerAppForms.Interface
{
    public interface IJobsInstallerList
    {
        Task<List<JobsInstallerCS>> GetInstaller(string criteria = null);
    }
}
