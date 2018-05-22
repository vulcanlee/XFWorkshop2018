using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace XFQRCode.ViewModels
{
    using System.ComponentModel;
    using Prism.Events;
    using Prism.Navigation;
    using Prism.Services;
    using Xamarin.Forms;

    public class ScanningPageViewModel : INotifyPropertyChanged, INavigationAware
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public bool IsAnalyzing { get; set; }
        public bool IsScanning { get; set; }
        public ZXing.Result ScanResult { get; set; }
        public DelegateCommand ScanResultCommand { get; set; }
        private readonly INavigationService _navigationService;

        public ScanningPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            IsAnalyzing = true;
            IsScanning = true;

            ScanResultCommand = new DelegateCommand(() =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    IsAnalyzing = false;
                    IsScanning = false;

                    var fooPara = new NavigationParameters();
                    fooPara.Add("Result", ScanResult.Text);
                    // 回到上頁，並且把掃描結果帶回去
                    _navigationService.GoBackAsync(fooPara);
                });
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
