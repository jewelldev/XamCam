using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamCam.Models;
using XamCam.Views;
using static Xamarin.Essentials.Permissions;

namespace XamCam.ViewModels
{
    public class PermissionsViewModel : INotifyPropertyChanged
    {
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

        public ICommand OnSelectPermissionChangeCommand { get; set; }
        public ICommand GoHomeCommand { get; set; }
        public ICommand LoadPermissionCommand { get; set; }
        public List<PermissionInfo> PermissionsList { get; set; }
        public PermissionInfo PermissionSelected { get; set; }
   
        public PermissionsViewModel()
        {
            LoadPermissionCommand = new Command(async () => await LoadPermissions());
            LoadPermissionCommand.Execute(null);

            OnSelectPermissionChangeCommand = new Command(async () =>
            {
                if(PermissionSelected != null)
                {
                    PermissionSelected.IsGranted = await CheckAndRequestPermissionAsync(PermissionSelected.Permission) == PermissionStatus.Granted;
                }
            });

            GoHomeCommand = new Command(async () =>
            {
                await App.Current.MainPage.DisplayAlert("Hey", "Welcome to my App", "Ok");

                //await App.Current.MainPage.Navigation.PushAsync(new MainPage());
            });

            //GoHomeCommand = new Command(DisplayMyAlert());
        }

        //async Task DisplayMyAlert()
        //{
        //    await App.Current.MainPage.DisplayAlert("Hey", "Welcome to my App", "Ok");
        //}

        async Task LoadPermissions()
        {
            PermissionsList = new List<PermissionInfo>()
            {
                { await CreatePermission(new Camera(), "", "Camera", "So your friends can see you")},
                //{ await CreatePermission(new Microphone(), "", "Mic", "So your friends can hear you") },
                //{ await CreatePermission(new LocationWhenInUse(), "", "Location", "So your friends can find you") }
            };
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
