using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace PokemonDI.ViewModels
{
    public class ViewModelBase : BindableBase, INavigationAware
    {
        private bool _isBusy;
        private string _title;
        private ICommand _backCommand;
        private ICommand _navigateCommand;

        protected INavigationService NavigationService { get; }

        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public ICommand BackCommand => _backCommand ??
                                       (_backCommand = new DelegateCommand(GoBack));

        public ICommand NavigateCommand => _navigateCommand ??
                                           (_navigateCommand = new DelegateCommand<string>(Navigate));

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public bool IsBusy
        {
            get => _isBusy;
            set { SetProperty(ref _isBusy, value, () => RaisePropertyChanged(nameof(IsNotBusy))); }
        }

        public bool IsNotBusy => !IsBusy;

        public virtual void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(NavigationParameters parameters)
        {
        }

        public virtual void OnNavigatingTo(NavigationParameters parameters)
        {

        }

        private void GoBack()
        {
            NavigationService.GoBackAsync();
        }

        private void Navigate(string view)
        {
            NavigationService.NavigateAsync(view);
        }
    }
}
