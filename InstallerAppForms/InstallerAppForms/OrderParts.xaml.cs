using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstallerAppForms.Models;
using InstallerAppForms.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InstallerAppForms
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class OrderParts : CustomContentPageBackButton
	{
	    private VmOrderPartsInfo vmOrderPartsInfo;
		public OrderParts (int getInstallerId, IndividualRoomCS individualRoomCS, PartsInfoCS partInfo)
		{
			InitializeComponent ();
		    vmOrderPartsInfo = new VmOrderPartsInfo
		    {
                IndividualRoomInfo = individualRoomCS
            };

		    if (EnableBackButtonOverride)
		    {
		        this.CustomBackButtonAction = async () =>
		        {
		            await Navigation.PopAsync(true);
		        };
		    }

            BindingContext = vmOrderPartsInfo;
		}
	}
}