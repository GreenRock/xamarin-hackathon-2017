using Prism.Navigation;

namespace PokemonDI.ViewModels
{
    public class MapPageViewModel : ViewModelBase
    {
        public MapPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Pokemon Map";
        }
    }
}
