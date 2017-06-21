using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Navigation;

namespace PokemonDI.ViewModels
{
    public class DetailPageViewModel : ViewModelBase
    {
        public DetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Pokemon";
        }
    }
}
