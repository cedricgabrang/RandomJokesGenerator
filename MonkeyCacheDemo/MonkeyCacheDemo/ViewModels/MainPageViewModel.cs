using MonkeyCacheDemo.Models;
using MonkeyCacheDemo.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MonkeyCacheDemo.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {

        #region Fields

        public event PropertyChangedEventHandler PropertyChanged;
        private readonly JokesDataService _jokesDataService;
        private List<Jokes> _jokes;
        private bool _isRefreshing;

        #endregion

        #region Properties

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set
            {
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        public List<Jokes> Jokes
        {
            get => _jokes;
            set
            {
                _jokes = value;
                OnPropertyChanged(nameof(Jokes));
            }
        }

        #endregion

        #region Commands

        public ICommand SearchCommand { get; set; }

        public ICommand RefreshCommand { get; set; }

        #endregion

        #region Constructor

        public MainPageViewModel()
        {

            _jokesDataService = new JokesDataService();

            SearchCommand = new Command(GetData);
            RefreshCommand = new Command(async () => await PerformSearch());

            GetData();
        }

        #endregion

        private async void GetData()
        {
            if (!IsRefreshing)
            {
                IsRefreshing = true;
                await GetRandomJokesAsync();
                IsRefreshing = false;
            }
        }

        private async Task PerformSearch()
        {
            await GetRandomJokesAsync();
            IsRefreshing = false;
        }

        #region Methods

        private async Task GetRandomJokesAsync()
        {
            var result = await _jokesDataService.GetRandomJokesAsync();
            Jokes = result;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion



    }
}
