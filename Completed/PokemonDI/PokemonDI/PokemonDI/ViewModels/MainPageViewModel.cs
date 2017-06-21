using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokedexNET;
using Prism.Navigation;

namespace PokemonDI.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {

        public MainPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            this.PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectedPokemon) && SelectedPokemon != null)
            {
                var name = "Detail?slug=" + SelectedPokemon.Slug;
                SelectedPokemon = null;
                NavigationService.NavigateAsync(name);
            }
        }

        private Pokemon _selectedPokemon;
        public Pokemon SelectedPokemon
        {
            get => _selectedPokemon;
            set => SetProperty(ref _selectedPokemon, value);
        }

        private List<Pokemon> _pokemons;
        public List<Pokemon> Pokemons
        {
            get => _pokemons;
            set => SetProperty(ref _pokemons, value);
        }


        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            Title = "Pokemons";
            Pokemons = new List<Pokemon>(DataService.GetPokemons(152));
        }
    }
}
