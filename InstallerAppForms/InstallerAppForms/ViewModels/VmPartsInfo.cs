using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using InstallerAppForms.Models;

namespace InstallerAppForms.ViewModels
{
    public class VmPartsInfo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private IndividualRoomCS _individualRoomInfo;
        private ObservableCollection<PartsInfoCS> _lstPartsInfo;

        public IndividualRoomCS IndividualRoomInfo
        {
            get => _individualRoomInfo;
            set
            {
                _individualRoomInfo = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<PartsInfoCS> LstPartsInfo
        {
            get => _lstPartsInfo;
            set
            {
                _lstPartsInfo = value;
                OnPropertyChanged();
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
