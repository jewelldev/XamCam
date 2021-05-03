using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamCam.Models;
using static Xamarin.Essentials.Permissions;

namespace XamCam.ViewModels
{
    public class CameraPermissionViewModel : INotifyPropertyChanged
    {
        public ICommand LoadPermissionCommand { get; set; }

        private PermissionInfo _cameraPermission;
        public PermissionInfo CameraPermission
        {
            get => _cameraPermission;
            set
            {
                _cameraPermission = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CameraPermission)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public CameraPermissionViewModel()
        {
            LoadPermissionCommand = new Command(async () => await LoadPermission());
            LoadPermissionCommand.Execute(null);
        }

        async Task LoadPermission()
        {
            CameraPermission = await CreatePermission(new Camera(), "", "Camera", "Use of the camera is required for taking photos");

            if(!CameraPermission.IsGranted)
            {
                CameraPermission.IsGranted = await CheckAndRequestPermissionAsync(new Camera()) == PermissionStatus.Granted;
            }
        }

        async Task<PermissionInfo> CreatePermission(BasePermission permission, string icon, string name, string description)
        {
            return new PermissionInfo()
            {
                Icon = icon,
                Permission = permission,
                Name = name,
                Description = description,
                IsGranted = await permission.CheckStatusAsync() == PermissionStatus.Granted
            };
        }

        async Task<PermissionStatus> CheckAndRequestPermissionAsync(BasePermission permission)
        {
            var status = await permission.CheckStatusAsync();
            if (status != PermissionStatus.Granted)
            {
                status = await permission.RequestAsync();
            }
            return status;
        }
    }
}
