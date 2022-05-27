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

namespace WBIS_2.Modules.Views.UserControls
{
    /// <summary>
    /// Interaction logic for BandingPatternControl.xaml
    /// </summary>
    public partial class BandingPatternControl : UserControl
    {
        string Leg = "";
        string Pattern = "";
        string Prominent = "";
        string Second = "";
        public string ReturnValue { get; set; }
        public BandingPatternControl(string val)
        {
            InitializeComponent();
            if (val != "")
            {
                //string leg = "";
                //string pattern = "";
                //string prominent = "";
                //string second = "";

                var vals = val.Split(':');
                Leg = vals[0];
                SetLeg(Leg);

                if (vals.Count() > 1)
                {
                    vals = vals[1].Split('(');
                    Pattern = vals[0];
                    if (vals.Count() > 1)
                    {
                        vals = vals[1].Split(',');
                        Prominent = vals[0].Substring(0, 3);
                        if (vals.Count() > 1)
                        {
                            Second = vals[1].Substring(0, 3);
                        }
                    }
                }
                SetPattern(Pattern);
                SetProminentColor(Prominent);
                SetSecondColor(Second);
            }
            else
            {
                PatternEnabled(false);
                ProminentEnabled(false);
                SecondEnabled(false);
            }
        }
        private void SetLeg(string leg)
        {
            if (leg == "")
            {
                PatternEnabled(false);
                ProminentEnabled(false);
                SecondEnabled(false);
            }
            else if (leg == "Unknown")
            {
                ChbxUnknown.IsChecked = true;
                PatternEnabled(false);
                ProminentEnabled(false);
                SecondEnabled(false);
            }
            else if (leg == "Unbanded")
            {
                ChbxNotBanded.IsChecked = true;
                PatternEnabled(false);
                ProminentEnabled(false);
                SecondEnabled(false);
            }
            else if (leg == "Both")
            {
                ChbxLeftLeg.IsChecked = true;
                ChbxRightLeg.IsChecked = true;
                PatternEnabled(true);
                ProminentEnabled(false);
                SecondEnabled(false);
            }
            else if (leg == "Left Leg")
            {
                ChbxLeftLeg.IsChecked = true;
                PatternEnabled(true);
                ProminentEnabled(false);
                SecondEnabled(false);
            }
            else if (leg == "Right Leg")
            {
                ChbxRightLeg.IsChecked = true;
                PatternEnabled(true);
                ProminentEnabled(false);
                SecondEnabled(false);
            }
        }
        private void SetPattern(string pattern)
        {
            if (pattern == "")
            {
                ProminentEnabled(false);
                SecondEnabled(false);
            }
            else
            {
                foreach (var chbx in GridPattern.Children)
                {
                    if (((CheckBox)chbx).Name.ToUpper().Contains(pattern))
                    {
                        ((CheckBox)chbx).IsChecked = true;
                        break;
                    }
                }
                ProminentEnabled(true);
                SecondEnabled(true);
            }
        }
        private void SetProminentColor(string prominent)
        {
            if (prominent != "")
            {
                foreach (var chbx in GridProminentColor.Children)
                {
                    if (((CheckBox)chbx).Name.ToUpper().Contains(prominent))
                    {
                        ((CheckBox)chbx).IsChecked = true;
                        break;
                    }
                }
            }
        }
        private void SetSecondColor(string second)
        {
            if (second != "")
            {
                foreach (var chbx in GridSecondColor.Children)
                {
                    if (((CheckBox)chbx).Name.ToUpper().Contains(second))
                    {
                        ((CheckBox)chbx).IsChecked = true;
                        break;
                    }
                }
            }
        }

        private void ChbxUnknown_Click(object sender, RoutedEventArgs e)
        {
            //ChbxUnknown.IsChecked = true;
            ChbxNotBanded.IsChecked = false;
            ChbxLeftLeg.IsChecked = false;
            ChbxRightLeg.IsChecked = false;

            PatternEnabled(false);
            ProminentEnabled(false);
            SecondEnabled(false);

            if (ChbxUnknown.IsChecked.Value) { Leg = "Unknown"; }
            else { Leg = ""; }
        }

        private void ChbxNotBanded_Click(object sender, RoutedEventArgs e)
        {
            ChbxUnknown.IsChecked = false;
            //ChbxNotBanded.IsChecked = true;
            ChbxLeftLeg.IsChecked = false;
            ChbxRightLeg.IsChecked = false;

            PatternEnabled(false);
            ProminentEnabled(false);
            SecondEnabled(false);

            if (ChbxNotBanded.IsChecked.Value) { Leg = "Unbanded"; }
            else { Leg = ""; }
        }

        private void ChbxLeftLeg_Click(object sender, RoutedEventArgs e)
        {
            ChbxUnknown.IsChecked = false;
            ChbxNotBanded.IsChecked = false;
            //  ChbxLeftLeg.IsChecked = true;

            PatternEnabled(ChbxLeftLeg.IsChecked.Value || ChbxRightLeg.IsChecked.Value);
            if (!ChbxLeftLeg.IsChecked.Value && !ChbxRightLeg.IsChecked.Value)
            {
                ProminentEnabled(false);
                SecondEnabled(false);
            }

            if (ChbxLeftLeg.IsChecked.Value && ChbxRightLeg.IsChecked.Value) { Leg = "Both"; }
            else if (ChbxLeftLeg.IsChecked.Value) { Leg = "Left Leg"; }
            else { Leg = ""; }
        }

        private void ChbxRightLeg_Click(object sender, RoutedEventArgs e)
        {
            ChbxUnknown.IsChecked = false;
            ChbxNotBanded.IsChecked = false;
            // ChbxRightLeg.IsChecked = true;

            PatternEnabled(ChbxLeftLeg.IsChecked.Value || ChbxRightLeg.IsChecked.Value);
            if (!ChbxLeftLeg.IsChecked.Value && !ChbxRightLeg.IsChecked.Value)
            {
                ProminentEnabled(false);
                SecondEnabled(false);
            }

            if (ChbxLeftLeg.IsChecked.Value && ChbxRightLeg.IsChecked.Value) { Leg = "Both"; }
            else if (ChbxRightLeg.IsChecked.Value) { Leg = "Right Leg"; }
            else { Leg = ""; }
        }

        private void ChbxPattern_Click(object sender, RoutedEventArgs e)
        {
            CheckBox clicked = (CheckBox)sender;
            bool isChecked = clicked.IsChecked.Value;

            foreach (var chbx in GridPattern.Children)
            {
                if (((CheckBox)chbx).Name != clicked.Name)
                {
                    ((CheckBox)chbx).IsChecked = false;
                }
            }
            clicked.IsChecked = isChecked;

            ProminentEnabled(isChecked);
            SecondEnabled(isChecked);

            if (clicked.IsChecked.Value) { Pattern = clicked.Name.Replace("Chbx", "").ToUpper(); }
            else { Pattern = ""; }
        }

        private void ChbxPriminentColor_Click(object sender, RoutedEventArgs e)
        {
            CheckBox clicked = (CheckBox)sender;
            bool isChecked = clicked.IsChecked.Value;

            foreach (var chbx in GridProminentColor.Children)
            {
                if (((CheckBox)chbx).Name != clicked.Name)
                {
                    ((CheckBox)chbx).IsChecked = false;
                }
            }
            clicked.IsChecked = isChecked;

            if (clicked.IsChecked.Value) { Prominent = clicked.Name.Substring(4, 3).ToUpper(); }
            else { Prominent = ""; }
        }

        private void ChbxSecondColor_Click(object sender, RoutedEventArgs e)
        {
            CheckBox clicked = (CheckBox)sender;
            bool isChecked = clicked.IsChecked.Value;

            foreach (var chbx in GridSecondColor.Children)
            {
                if (((CheckBox)chbx).Name != clicked.Name)
                {
                    ((CheckBox)chbx).IsChecked = false;
                }
            }
            clicked.IsChecked = isChecked;

            if (clicked.IsChecked.Value) { Second = clicked.Name.Substring(4, 3).ToUpper(); }
            else { Second = ""; }
        }

        private void PatternEnabled(bool enable)
        {
            GridPattern.IsEnabled = enable;
            if (!enable)
            {
                foreach (var chbx in GridPattern.Children)
                {
                    ((CheckBox)chbx).IsChecked = false;
                }
                Pattern = "";
            }
        }
        private void ProminentEnabled(bool enable)
        {
            GridProminentColor.IsEnabled = enable;
            if (!enable)
            {
                foreach (var chbx in GridProminentColor.Children)
                {
                    ((CheckBox)chbx).IsChecked = false;
                }
                Prominent = "";
            }
        }
        private void SecondEnabled(bool enable)
        {
            GridSecondColor.IsEnabled = enable;
            if (!enable)
            {
                foreach (var chbx in GridSecondColor.Children)
                {
                    ((CheckBox)chbx).IsChecked = false;
                }
                Second = "";
            }
        }


        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            window.DialogResult = false;
            window.Close();
        }
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            ReturnValue = Leg;
            if (Pattern != "")
            {
                ReturnValue += ":" + Pattern;
                if (Prominent != "" || Second != "")
                {
                    if (Prominent != "")
                    {
                        ReturnValue += "(" + Prominent;
                        if (Second != "")
                        {
                            ReturnValue += "," + Second;
                        }
                    }
                    else
                    {
                        ReturnValue += "(" + Second;
                    }
                    ReturnValue += ")";
                }
            }

            Window window = Window.GetWindow(this);
            window.DialogResult = true;
            window.Close();
        }
    }
}
