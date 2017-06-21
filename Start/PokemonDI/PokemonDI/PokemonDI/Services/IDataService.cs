using System.Collections.Generic;
using PokedexNET;
using PokemonDI.Controls;
using PokemonDI.Models;

namespace PokemonDI.Services
{
    public interface IDataService
    {
        List<CustomPin> GetNearbyPokemonAsPins(double lat, double lng, int number, double maxDist = 1);
        IEnumerable<Pokemon> GetPokemons(int count = 152);
        PokemonInfo GetPokemonInfo(Pokemon pokemon);
        PokemonInfo GetPokemonInfo(string name);
        List<GeoObject<Pokemon>> GetNearbyPokemon(double lat, double lng, int number, double maxDist);
    }
}