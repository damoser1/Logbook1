using Logbook.LogBookCore.ViewModel;
using Syncfusion.Maui.Charts;

namespace Logbook.LogBookApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainViewModel ViewModel)
        {
            InitializeComponent();
            this.BindingContext = ViewModel;
        }
    }

}
