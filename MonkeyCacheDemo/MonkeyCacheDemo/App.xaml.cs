using MonkeyCacheDemo.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MonkeyCache.SQLite;

namespace MonkeyCacheDemo
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Barrel.ApplicationId = "random_jokes_cache";
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
