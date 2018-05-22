using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace XFTabbed2.ViewModels
{
    using System.ComponentModel;
    using Prism.Events;
    using Prism.Navigation;
    using Prism.Services;
    public class Page2ViewModel : INotifyPropertyChanged, INavigationAware
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string Title { get; set; }
        private readonly INavigationService _navigationService;
        public DelegateCommand GoNextCommand { get; set; }
        public Page2ViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Title = "頁面 2";
            GoNextCommand = new DelegateCommand(() =>
            {
                _navigationService.NavigateAsync("Page21");
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
