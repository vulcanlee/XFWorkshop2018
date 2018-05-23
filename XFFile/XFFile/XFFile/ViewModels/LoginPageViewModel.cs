using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace XFFile.ViewModels
{
    using System.ComponentModel;
    using Newtonsoft.Json;
    using PCLStorage;
    using Prism.Events;
    using Prism.Navigation;
    using Prism.Services;
    using XFFile.Models;

    public class LoginPageViewModel : INotifyPropertyChanged, INavigationAware
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string Account { get; set; }
        public string Password { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsShowErrorMessage { get; set; }
        private readonly INavigationService _navigationService;
        public DelegateCommand LoginCommand { get; set; }
        public LoginPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            LoginCommand = new DelegateCommand(async () =>
            {
                if(Account == "123" && Password == "123")
                {
                    IFolder fooFolder = await PCLStorage.FileSystem.Current.LocalStorage.
                    CreateFolderAsync("MyData", CreationCollisionOption.OpenIfExists);
                    IFile fooFile = await fooFolder.CreateFileAsync("UserInfo.txt", CreationCollisionOption.OpenIfExists);
                    var fooItem = new UserInfo()
                    {
                         ID = Account,
                         Name = Account
                    };
                    var fooJSON = JsonConvert.SerializeObject(fooItem);
                    await fooFile.WriteAllTextAsync(fooJSON);
                    _navigationService.NavigateAsync("/MainPage");
                }
                else
                {
                    ErrorMessage = "帳號或密碼錯誤，重新輸入!!";
                    IsShowErrorMessage = true;
                }
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
