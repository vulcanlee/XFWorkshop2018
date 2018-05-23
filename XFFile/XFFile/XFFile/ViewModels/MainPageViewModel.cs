using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XFFile.ViewModels
{
    using System.ComponentModel;
    using Newtonsoft.Json;
    using PCLStorage;
    using Prism.Events;
    using Prism.Navigation;
    using Prism.Services;
    using XFFile.Models;

    public class MainPageViewModel : INotifyPropertyChanged, INavigationAware
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public DelegateCommand LogoutCommand { get; set; }
        private readonly INavigationService _navigationService;

        public MainPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            LogoutCommand = new DelegateCommand(async () =>
            {
                IFolder fooFolder = await PCLStorage.FileSystem.Current.LocalStorage.
                CreateFolderAsync("MyData", CreationCollisionOption.OpenIfExists);
                IFile fooFile = await fooFolder.CreateFileAsync("UserInfo.txt", CreationCollisionOption.OpenIfExists);
                var fooItem = new UserInfo()
                {
                    ID = "",
                    Name = ""
                };
                var fooJSON = JsonConvert.SerializeObject(fooItem);
                await fooFile.WriteAllTextAsync(fooJSON);
                _navigationService.NavigateAsync("/LoginPage");
            });
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {

        }

    }
}
