using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace InstaWeather.ViewModels
{
    /// <summary>
    /// Interface for page navigation & notification service provider classes
    /// </summary>
    public interface IPageService
    {
        Task DisplayAlert(string title, string message, string ok);
        Task<bool> DisplayAlert(string title, string message, string ok, string cancel);
        Task PushAsync(Page page);
        Task PopAsync();
        Task PushModalAsync(Page page);
        Task PopModalAsync();
        void Close();
    }
}
