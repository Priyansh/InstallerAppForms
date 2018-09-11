using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using InstallerAppForms.Models;

namespace InstallerAppForms.ViewModels
{
    public class VmOrderPartsInfo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private IndividualRoomCS _individualRoomInfo;
        private ObservableCollection<OrderPartsInfoCS> _lstOrderPartsInfo;

        public IndividualRoomCS IndividualRoomInfo
        {
            get => _individualRoomInfo;
            set
            {
                _individualRoomInfo = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<OrderPartsInfoCS> LstOrderPartsInfo
        {
            get => _lstOrderPartsInfo;
            set
            {
                _lstOrderPartsInfo = value;
                OnPropertyChanged();
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
