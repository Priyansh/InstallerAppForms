using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
	    private int _selectedPartType, _selectedLabelNo, _CSID;
		public OrderParts (int getInstallerId, IndividualRoomCS individualRoomCS, PartsInfoCS partInfo)
		{
			InitializeComponent ();
		    vmOrderPartsInfo = new VmOrderPartsInfo
		    {
                IndividualRoomInfo = individualRoomCS,
                PartsInfo = partInfo
            };

		    if (EnableBackButtonOverride)
		    {
		        this.CustomBackButtonAction = async () =>
		        {
		            await Navigation.PopAsync(true);
		        };
		    }

		    _selectedPartType = vmOrderPartsInfo.PartsInfo.PartType;
		    _selectedLabelNo = vmOrderPartsInfo.PartsInfo.LabelNo;
		    _CSID = vmOrderPartsInfo.PartsInfo.CSID;

            BindingContext = vmOrderPartsInfo;
		}

	    protected override void OnAppearing()
	    {
	        base.OnAppearing();
	        GetOrderPartsInfo();
	    }

	    public async void GetOrderPartsInfo()
	    {
	        var result = await App.FrendelSOAPService.GetPartIssueList(_selectedPartType, _selectedLabelNo, _CSID);
	        vmOrderPartsInfo.LstOrderPartsInfo = new ObservableCollection<OrderPartsInfoCS>(result);
	    }

	    private void BtnAddOrder_OnClicked(object sender, EventArgs e)
	    {
	        
	    }

	    private void LstViewOrderPartsInfo_OnRefreshing(object sender, EventArgs e)
	    {
	        GetOrderPartsInfo();
	        lstViewOrderPartsInfo.EndRefresh();
        }

	    private void LstViewOrderPartsInfo_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
	    {

	    }
	}
}