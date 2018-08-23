using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InstallerAppForms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PartsInfo : CustomContentPageBackButton
    {
        private int installerId, CSID;
        private IndividualRoomCS selectedIndividualRoom;
        private string RoomNo, RoomName;
        public PartsInfo(int getInstallerId, IndividualRoomCS individualRoom)
        {
            InitializeComponent();
            installerId = getInstallerId;
            this.selectedIndividualRoom = individualRoom;
            CSID = this.selectedIndividualRoom.CSID;
            RoomNo = this.selectedIndividualRoom.RSNo;
            RoomName = this.selectedIndividualRoom.Rooms;

            if (EnableBackButtonOverride)
            {
                this.CustomBackButtonAction = async () =>
                {
                    await Navigation.PopAsync(true);
                };
            }

            if (this.selectedIndividualRoom is null) return;

            BindingContext = this.selectedIndividualRoom;
        }
    }
}