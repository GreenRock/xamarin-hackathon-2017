using System.Collections.Generic;
using PokedexNET;

namespace PokemonDI.Services
{
    public interface IDataService
    {
        IEnumerable<Pokemon> GetPokemons(int count = 152);
        PokemonInfo GetPokemonInfo(Pokemon pokemon);
        PokemonInfo GetPokemonInfo(string name);
    }
}