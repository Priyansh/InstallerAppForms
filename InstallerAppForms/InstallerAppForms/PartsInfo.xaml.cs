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
using XLabs.Serialization;

namespace InstallerAppForms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PartsInfo : CustomContentPageBackButton
    {
        private int _installerId;
        private VmPartsInfo vmPartsInfo;
        private readonly string _masterNum, _roomName;
        public PartsInfo(int getInstallerId, IndividualRoomCS individualRoom)
        {
            InitializeComponent();
            _installerId = getInstallerId;
            
            vmPartsInfo = new VmPartsInfo
            {
                IndividualRoomInfo = individualRoom
            };

            _masterNum = vmPartsInfo.IndividualRoomInfo.MasterNum;
            _roomName = vmPartsInfo.IndividualRoomInfo.Rooms;

            if (EnableBackButtonOverride)
            {
                this.CustomBackButtonAction = async () =>
                {
                    await Navigation.PopAsync(true);
                };
            }

            BindingContext = vmPartsInfo;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            GetPartsInfo();
        }

        public async void GetPartsInfo()
        {
            var result = await App.FrendelSOAPService.GetPartInfo(_masterNum, _roomName);
            vmPartsInfo.LstPartsInfo = new ObservableCollection<PartsInfoCS>(result);
        }

        private void ListView_OnRefreshing(object sender, EventArgs e)
        {
            GetPartsInfo();
            lstViewPartsInfo.EndRefresh();
        }

        private async void LstViewPartsInfo_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedPartInfo = e.SelectedItem as PartsInfoCS;
            if(selectedPartInfo is null)
                return;
            await Navigation.PushAsync(new OrderParts(_installerId, vmPartsInfo.IndividualRoomInfo, selectedPartInfo));
        }
    }
}