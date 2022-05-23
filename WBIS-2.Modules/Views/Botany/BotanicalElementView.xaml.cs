using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WBIS_2.Modules.ViewModels;

namespace WBIS_2.Modules.Views.Botany
{
    /// <summary>
    /// Interaction logic for BotanicalElementView.xaml
    /// </summary>
    public partial class BotanicalElementView : UserControl
    {
        public BotanicalElementView()
        {
            InitializeComponent();
            new UserControlExtension(this);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            WBIS_2.DataModel.WBIS2Model databse = new DataModel.WBIS2Model();
            Guid guid = ((BotanicalElementViewModel)DataContext).RecordId;
            if (databse.BotanicalPlantsOfInterest.Any(_=>_.Guid == guid))
            {
                PlantSpeciesListView plantSpeciesListView = new PlantSpeciesListView();
                this.Content = plantSpeciesListView;
            }
            else if (databse.BotanicalPointsOfInterest.Any(_=>_.Guid == guid))
            {

            }
            else
            {

            }
        }
    }
}
