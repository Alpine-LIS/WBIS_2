﻿using System;
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

namespace WBIS_2.Modules.Views.Botany
{
    /// <summary>
    /// Interaction logic for AmphibianSurveyView.xaml
    /// </summary>
    public partial class AmphibianSurveyView : UserControl
    {
        public AmphibianSurveyView()
        {
            InitializeComponent();
            new UserControlExtension(this);
        }
    }
}