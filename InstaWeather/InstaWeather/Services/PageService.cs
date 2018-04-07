using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using InstaWeather.ViewModels;
using Xamarin.Forms;

namespace InstaWeather.Services
{
    public class PageService : IPageService
    {
        public async Task DisplayAlert(string title, string message, string ok)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, ok); // MainPage is a property from App.xaml.cs
        }

        public async Task<bool> DisplayAlert(string title, string message, string ok, string cancel)
        {
            return await Application.Current.MainPage.DisplayAlert(title, message, ok, cancel); // MainPage is a property from App.xaml.cs
        }

        public async Task PushAsync(Page page)
        {
            await Application.Current.MainPage.Navigation.PushAsync(page);
        }

        public async Task PopAsync()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        public async Task PushModalAsync(Page page)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(page);
        }

        public async Task PopModalAsync()
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }

        public void Close()
        {
            Environment.Exit(0);
        }
    }
}
