using System.Collections.Generic;
using PokedexNET;
using PokemonDI.Models;

namespace PokemonDI.Services
{
    public interface IDataService
    {
        IEnumerable<Pokemon> GetPokemons(int count = 152);
        PokemonInfo GetPokemonInfo(Pokemon pokemon);
        PokemonInfo GetPokemonInfo(string name);
        List<GeoObject<Pokemon>> GetNearbyPokemon(double lat, double lng, int number, double maxDist);
    }
}