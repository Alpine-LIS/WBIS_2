using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;

namespace WBIS_2.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public static MainViewModel Create()
        {
            return ViewModelSource.Create(() => new MainViewModel());
        }
    }
}
