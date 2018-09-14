using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using InstallerAppForms.Models;
using InstallerAppForms.ViewModels;
using Plugin.Messaging;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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

        private void BtnSubmit_OnClicked(object sender, EventArgs e)
        {
            var lstCheckedItems = GetCheckedItems();
            if (lstCheckedItems.Count > 0)
            {
                var emailTask = CrossMessaging.Current.EmailMessenger;
                if (emailTask.CanSendEmail)
                {

                    var emailText = new StringBuilder();
                    string[] subject = new string[] { "FK #" + vmPartsInfo.IndividualRoomInfo.JobNum, vmPartsInfo.IndividualRoomInfo.Company, vmPartsInfo.IndividualRoomInfo.Project, vmPartsInfo.IndividualRoomInfo.Lot };
                    emailText.AppendLine("--- Installer Company ---");
                    emailText.AppendLine("");
                    emailText.AppendFormat("{0}, {1} \n", vmPartsInfo.IndividualRoomInfo.Company, vmPartsInfo.IndividualRoomInfo.Project);
                    emailText.AppendFormat("{0}, {1} \n", vmPartsInfo.IndividualRoomInfo.Lot, vmPartsInfo.IndividualRoomInfo.JobNum);
                    emailText.AppendLine("");
                    foreach (var items in lstCheckedItems)
                    {
                        emailText.AppendFormat("Cabinet : {0} \n", items.CabinetName);
                        emailText.AppendFormat("LFinish : {0}, RFinish : {1} \n", items.LFinish, items.RFinish);
                        emailText.AppendLine("");
                    }

                    var email = new EmailMessageBuilder()
                        .To("installerparts@frendel.com")
                        .Subject(string.Join(" / ", subject))
                        .Body(emailText.ToString())
                        .Build();

                    emailTask.SendEmail(email);
                }
            }
            
        }

        public List<PartsInfoCS> GetCheckedItems()
        {
            return vmPartsInfo.LstPartsInfo
                .Where(a => a.IsCbSelected == true)
                .Select(p => new PartsInfoCS
                {
                    CabinetName = p.CabinetName,
                    LFinish = p.LFinish,
                    RFinish = p.RFinish,
                    IsCbSelected = true
                }).ToList();
        }
    }
}