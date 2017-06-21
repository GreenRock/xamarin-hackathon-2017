using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PokemonDI.Controls;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms.Maps;

namespace PokemonDI.ViewModels
{
    public class MapPageViewModel : ViewModelBase
    {
        private List<CustomPin> _pins;
        public List<CustomPin> Pins
        {
            get => _pins;
            set => SetProperty(ref _pins, value);
        }

        private Position _center;
        public Position Center
        {
            get => _center;
            set => SetProperty(ref _center, value);
        }

        public ICommand SearchCommand => new DelegateCommand(ReloadPins);

        public MapPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Pokemon Maps";
        }

        private void ReloadPins()
        {
            Pins = DataService.GetNearbyPokemon(Center.Latitude, Center.Longitude, 10, 1).Select(x => new CustomPin()
            {
                Label = x.Data.Name,
                Latitude = x.Latitude,
                Longitude = x.Longitude,
                Url = x.Data.ImageUrl,
                Id = x.Data.Number
            }).ToList();
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
        }
    }
}
