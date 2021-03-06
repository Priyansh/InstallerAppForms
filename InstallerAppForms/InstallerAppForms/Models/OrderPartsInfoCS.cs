﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace InstallerAppForms.Models
{
    public class OrderPartsInfoCS
    {
        public int PartIssueListId { get; set; }

        public string PartDescription { get; set; }
        
        public bool IsCbSelected { get; set; }

        public bool IsCbEnabled { get; set; }

        public string CbBackgroundColor { get; set; }

        public string PartIssueStatus { get; set; }

        public string PartIssueTextColor { get; set; }
    }
}
