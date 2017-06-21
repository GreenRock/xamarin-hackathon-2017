using System;
using System.Collections.Generic;
using System.Linq;
using PokedexNET;
using PokemonDI.Controls;
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
            var info = Pokedex.GetPokemonInfo(name);
            info.ImageUrl = Pokedex.Pokemons.FirstOrDefault(x => x.Slug == name).ImageUrl;
            return info;
        }

        public List<CustomPin> GetNearbyPokemonAsPins(double lat, double lng, int number, double maxDist = 1)
        {
            return GetNearbyPokemon(lat, lng, number, maxDist).Select(x => new CustomPin()
            {
                Label = x.Data.Name,
                Latitude = x.Latitude,
                Longitude = x.Longitude,
                Url = x.Data.ImageUrl,
                Id = x.Data.Number
            }).ToList();
        }

        public List<GeoObject<Pokemon>> GetNearbyPokemon(double lat, double lng, int number, double maxDist)
        {
            var count = Pokedex.Pokemons.Count;
            var random = new Random();
            return Helpers.GeoHelper.CalculateRandomPositions(number, lat, lng, maxDist)
                .Select(x => new GeoObject<Pokemon>(Pokedex.Pokemons[random.Next(0, count - 1)], x.Latitude,
                    x.Longitude))
                .ToList();
        }
    }
}
