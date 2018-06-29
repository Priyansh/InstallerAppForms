using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(InstallerAppForms.IndividualRoomCS))]
namespace InstallerAppForms
{
    
    public class IndividualRoomCS : JobsInstallerCS
    {
        public string RSNo { get; set; }

        //public string CSID { get; set; }

        public string Rooms { get; set; }

        public string Style { get; set; }

        public string Colour { get; set; }

        public string FormattedStyleColor { get { return Style + " / " + Colour; } }

        public string Hardware { get; set; }

        public string CounterTop { get; set; }

        public int PartsCount { get; set; }

        public int DeliveryPhoto { get; set; }

        public int InstallationPhoto { get; set; }

        public string FormattedInstallationPhoto { get { return "Installation Photos : " + InstallationPhoto; } }
    }
}