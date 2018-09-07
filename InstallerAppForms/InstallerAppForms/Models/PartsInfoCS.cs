using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstallerAppForms.Models
{
    public class PartsInfoCS
    {
        public string CabinetName { get; set; }
        
        public string LFinish { get; set; }
        
        public string RFinish { get; set; }

        public int PartType { get; set; }

        public int LabelNo { get; set; }

        public int CSID { get; set; }

        public int OrderPartsStatus { get; set; }

        public bool IsCbSelected { get; set; }
    }
}
