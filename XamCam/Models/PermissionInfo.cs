using System.ComponentModel;

namespace XamCam.Models
{
    public class PermissionInfo : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }

        private bool _isGranted;
        public bool IsGranted 
        {
            get => _isGranted; 
            set
            {
                _isGranted = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsGranted)));
            }
        }
        public Xamarin.Essentials.Permissions.BasePermission Permission { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
