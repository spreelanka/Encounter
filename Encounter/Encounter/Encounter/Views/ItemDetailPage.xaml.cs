using System.ComponentModel;
using Xamarin.Forms;
using Encounter.ViewModels;

namespace Encounter.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}
