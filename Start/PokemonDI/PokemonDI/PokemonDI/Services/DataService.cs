using System;
using System.Collections.Generic;
using System.Linq;
using PokedexNET;
using PokemonDI.Models;

namespace PokemonDI.Services
{
    public class DataService : IDataService
    {
        protected Pokedex Pokedex { get; }

        public DataService()
        {
            Pokedex = new Pokedex();
        }

        public IEnumerable<Pokemon> GetPokemons(int count = 152)
        {
            return Pokedex.Pokemons.Take(count);
        }

        public PokemonInfo GetPokemonInfo(Pokemon pokemon)
        {
            return Pokedex.GetPokemonInfo(pokemon);
        }

        public PokemonInfo GetPokemonInfo(string name)
        {
            return Pokedex.GetPokemonInfo(name);
        }

        public List<GeoObject<Pokemon>> GetNearbyPokemon(double lat, double lng, int number, double maxDist)
        {
            var count = Pokedex.Pokemons.Count;
            var random = new Random();
            return Helpers.GeoHelper.CalculateRandomPositions(number, lat, lng, maxDist)
                .Select(x => new GeoObject<Pokemon>(Pokedex.Pokemons[random.Next(0, count - 1)], x.Latitude,
                    x.Latitude))
                .ToList();
        }
    }
}
