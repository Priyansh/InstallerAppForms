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
	    private int _selectedPartType, _selectedLabelNo, _CSID, _partOrderId, _insertRequest = 0;
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
            _partOrderId = result.Item2;
	        vmOrderPartsInfo.LstOrderPartsInfo = new ObservableCollection<OrderPartsInfoCS>(result.Item1);
	    }

	    private async void BtnAddOrder_OnClicked(object sender, EventArgs e)
	    {
            var result = vmOrderPartsInfo.LstOrderPartsInfo
                                         .Where(c => c.IsCbEnabled == true && c.IsCbSelected == true);
            if(result.Count() > 0)
            {
                var command = await DisplayAlert("Parts Issues!!", "Want to add part Issue?", "Yes", "Cancel");
                if (command)
                {
                    
                    foreach(var item in result)
                    {
                        _insertRequest = 1; //If insertRequest = 1 , record will insert and fetch
                        var lstPartOrderIssueID = await App.FrendelSOAPService.InsertPartsOrderIssue(_partOrderId, item.PartIssueListId, _insertRequest);
                        if (lstPartOrderIssueID.Length == 0) {
                            await DisplayAlert("Unsuccessful", "Records not added", "Ok");
                            return;
                        }
                    }
                    await Navigation.PopAsync(true);
                }
                else
                    return;
            }
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