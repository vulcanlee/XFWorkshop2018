using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace XFInvoice.ViewModels
{
    using System.ComponentModel;
    using Prism.Events;
    using Prism.Navigation;
    using Prism.Services;
    using XFInvoice.Helpers;
    using XFInvoice.Models;
    using Firebase.Database.Query;

    public class DetailPageViewModel : INotifyPropertyChanged, INavigationAware
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public bool IsAdd { get; set; }
        public bool IsUpdate { get; set; }
        public InvoiceVM InvoiceSelected { get; set; }
        public string Action { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand DeleteCommand { get; set; }

        private readonly INavigationService _navigationService;

        public DetailPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            SaveCommand = new DelegateCommand(async () =>
            {
                if (Action == "Add")
                {
                    var child = MainHelper.FirebaseClient.Child("Invoices");
                    await child.PostAsync<Invoice>(new Invoice()
                    {
                        InvoiceNo = InvoiceSelected.InvoiceNo,
                        Amount = InvoiceSelected.Amount,
                        Date = InvoiceSelected.Date,
                        Memo = InvoiceSelected.Memo,
                        Title = InvoiceSelected.Title,
                    });
                }
                else
                {
                    var child = MainHelper.FirebaseClient.Child("Invoices");
                    await child.Child(InvoiceSelected.Key).PutAsync(new Invoice()
                    {
                        InvoiceNo = InvoiceSelected.InvoiceNo,
                        Amount = InvoiceSelected.Amount,
                        Date = InvoiceSelected.Date,
                        Memo = InvoiceSelected.Memo,
                        Title = InvoiceSelected.Title,
                    });
                }

                var fooPara = new NavigationParameters();
                fooPara.Add("NeedRefresh", "");
                await _navigationService.GoBackAsync(fooPara);
            });
            DeleteCommand = new DelegateCommand(async () =>
            {
                var child = MainHelper.FirebaseClient.Child("Invoices");
                await child.Child(InvoiceSelected.Key).DeleteAsync();

                var fooPara = new NavigationParameters();
                fooPara.Add("NeedRefresh", "");
                await _navigationService.GoBackAsync(fooPara);
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
            if (parameters.ContainsKey("Action"))
            {
                Action = parameters["Action"] as string;
                if (Action == "Update")
                {
                    if (parameters.ContainsKey("Item"))
                    {
                        var fooItem = parameters["Item"] as Firebase.Database.FirebaseObject<Invoice>;
                        InvoiceSelected = new InvoiceVM()
                        {
                            Key = fooItem.Key,
                            Title = fooItem.Object.Title,
                            Amount = fooItem.Object.Amount,
                            Date = fooItem.Object.Date,
                            InvoiceNo = fooItem.Object.InvoiceNo,
                            Memo = fooItem.Object.Memo,
                        };
                        IsUpdate = true;
                    }
                }
                else
                {
                    InvoiceSelected = new InvoiceVM();
                    IsAdd = true;
                }
            }
        }

    }
}
