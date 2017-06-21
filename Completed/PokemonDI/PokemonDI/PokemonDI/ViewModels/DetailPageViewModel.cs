using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokedexNET;
using Prism.Navigation;

namespace PokemonDI.ViewModels
{
    public class DetailPageViewModel : ViewModelBase
    {
        public DetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {

        }

        private PokemonInfo _pokemonInfo;
        public PokemonInfo PokemonInfo
        {
            get => _pokemonInfo;
            set => SetProperty(ref _pokemonInfo, value);
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            PokemonInfo = DataService.GetPokemonInfo(parameters["slug"].ToString());

            Title = "Detail";
        }
    }
}
