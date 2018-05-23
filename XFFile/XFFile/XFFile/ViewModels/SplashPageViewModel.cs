using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace XFFile.ViewModels
{
    using System.ComponentModel;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using PCLStorage;
    using Prism.Events;
    using Prism.Navigation;
    using Prism.Services;
    using XFFile.Models;

    public class SplashPageViewModel : INotifyPropertyChanged, INavigationAware
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly INavigationService _navigationService;

        public SplashPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {

        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            await Task.Delay(2000);

            IFolder fooFolder = await PCLStorage.FileSystem.Current.LocalStorage.
                CreateFolderAsync("MyData", CreationCollisionOption.OpenIfExists);
            IFile fooFile = await fooFolder.CreateFileAsync("UserInfo.txt", CreationCollisionOption.OpenIfExists);
            var fooContent = await fooFile.ReadAllTextAsync();
            var fooItem = JsonConvert.DeserializeObject<UserInfo>(fooContent);
            if(fooItem == null || string.IsNullOrEmpty(fooItem.ID))
            {
                _navigationService.NavigateAsync("/LoginPage");
            }
            else
            {
                _navigationService.NavigateAsync("/NavigationPage/MainPage");
            }
        }

    }
}
