using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Navigation;

namespace PokemonDI.ViewModels
{
    public class MapPageViewModel : ViewModelBase
    {
        public MapPageViewModel(INavigationService navigationService) : base(navigationService)
        {

        }
    }
}
