using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Moments.Views
{
    public class MapaPage : ContentPage
    {
        public MapaPage()
        {
            Content = new StackLayout()
            {
                Children = {new Map()}
            };


        }
    }
}
