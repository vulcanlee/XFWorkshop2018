using Prism;
using Prism.Ioc;
using XFTabbed2.ViewModels;
using XFTabbed2.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism.Unity;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XFTabbed2
{
    public partial class App : PrismApplication
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("MainTabbedPage?createTab=Page1&createTab=NavigationPage|Page2&createTab=Page3&createTab=Page4&selectedTab=Page3");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage>();
            containerRegistry.RegisterForNavigation<Page1>();
            containerRegistry.RegisterForNavigation<Page2>();
            containerRegistry.RegisterForNavigation<Page3>();
            containerRegistry.RegisterForNavigation<Page4>();
            containerRegistry.RegisterForNavigation<Page21>();
            containerRegistry.RegisterForNavigation<MainTabbedPage>();
        }
    }
}
