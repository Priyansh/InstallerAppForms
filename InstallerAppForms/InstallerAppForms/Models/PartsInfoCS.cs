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

        public string FormattedLFinish => "Left : " + LFinish;

        public string FormattedRFinish => "Right : " + RFinish;

        public int PartType { get; set; }

        public int LabelNo { get; set; }

        public int CSID { get; set; }

        public bool IsCbSelected { get; set; }

        public string OrderPartsStatus { get; set; }

        public string OrderPartsTextColor { get; set; }
    }
}
