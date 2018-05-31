using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstallerAppForms.Interface;

namespace InstallerAppForms
{
    public class JobsInstallerCS
    {
        public string Company { get; set; }
        
        public string Project { get; set; }

        public int CSID { get; set; }

        public string Lot { get; set; }

        public string JobNum { get; set; }

        public string FullJobNum => string.Format("{0} : {1}", "Job Number", JobNum);

        public string MasterNum { get; set; }

        public string InstallAssignDate { get; set; }

        public string InstallAssignPerson { get; set; }

        public string ShippedDone { get; set; }

        public int InstallerAssignID { get; set; }

        public int InstallerJobStatus { get; set; }

        public string InstallerJobStart { get; set; }

        public string InstallerJobComplete { get; set; }

        public string ImageJobStatus { get; set; }

        public string JobCurrentStatus { get; set; }

        public string JobStatusTextColor { get; set; }
    }
}
