using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Firebase.Database.Query;

namespace XFInvoice.ViewModels
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using Prism.Events;
    using Prism.Navigation;
    using Prism.Services;
    using XFInvoice.Helpers;
    using XFInvoice.Models;

    public class MainPageViewModel : INotifyPropertyChanged, INavigationAware
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public InvoiceVM InvoiceSelected { get; set; }
        public ObservableCollection<InvoiceVM> InvoiceSource { get; set; } = new ObservableCollection<InvoiceVM>();
        public bool IsRefreshing { get; set; }
        private readonly INavigationService _navigationService;
        public DelegateCommand TestCommand { get; set; }
        public DelegateCommand AddCommand { get; set; }
        public DelegateCommand RefreshCommand { get; set; }
        public DelegateCommand InvoiceTapCommand { get; set; }
        public MainPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            TestCommand = new DelegateCommand(async () =>
            {
                var child = MainHelper.FirebaseClient.Child("Invoices");
                await child.DeleteAsync();
                for (int i = 1; i < 5; i++)
                {
                    await child.PostAsync<Invoice>(new Invoice()
                    {
                        InvoiceNo = i.ToString(),
                        Amount = 100 + i * 5,
                        Date = DateTime.Now.AddDays(-1 * i),
                        Memo = $"Memo{i}",
                        Title = $"Invoice {i}"
                    });
                }

            });
            AddCommand = new DelegateCommand(async () =>
            {
                var fooPara = new NavigationParameters();
                fooPara.Add("Action", "Add");
                fooPara.Add("Item", null);
                _navigationService.NavigateAsync("DetailPage", fooPara);
            });
            RefreshCommand = new DelegateCommand(async () =>
            {
                await RefreshData();
                IsRefreshing = false;
            });
            InvoiceTapCommand = new DelegateCommand(async () =>
            {
                Firebase.Database.FirebaseObject<Invoice> fooInvoice = await GetInvoice(InvoiceSelected.Key);
                var fooPara = new NavigationParameters();
                fooPara.Add("Action", "Update");
                fooPara.Add("Item", fooInvoice);
                _navigationService.NavigateAsync("DetailPage", fooPara);
            });
        }

        private async Task RefreshData()
        {
            InvoiceSource.Clear();
            var child = MainHelper.FirebaseClient.Child("Invoices");
            var fooPosts = await child.OnceAsync<Invoice>();
            foreach (var item in fooPosts)
            {
                InvoiceSource.Add(new InvoiceVM()
                {
                    Key = item.Key,
                    InvoiceNo = item.Object.InvoiceNo,
                    Amount = item.Object.Amount,
                    Title = item.Object.Title,
                    Date = item.Object.Date,
                    Memo = item.Object.Memo,
                });
            }
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {

        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            if(parameters.ContainsKey("NeedRefresh"))
            {
                IsRefreshing = true;
                await RefreshData();
                IsRefreshing = false;
            }
        }

      async Task<Firebase.Database.FirebaseObject<Invoice>> GetInvoice(string key)
        {
            var child = MainHelper.FirebaseClient.Child("Invoices");
            var fooPosts = await child.OnceAsync<Invoice>();
            var fooObject = fooPosts.FirstOrDefault(x => x.Key == key);
            return fooObject;
        }
    }
}
