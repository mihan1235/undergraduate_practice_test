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
using Problem;

namespace undergraduate_practice
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //InverseProblem1 task = new InverseProblem1();

        public MainWindow()
        {
            InitializeComponent();
        }

        bool CheckForErrors()
        {
            if (Validation.GetHasError(F1X))
            {
                return true;
            }
            
            if (Validation.GetHasError(F2X))
            {
                return true;
            }

            if (Validation.GetHasError(P1T))
            {
                return true;
            }


            if (Validation.GetHasError(P2T))
            {
                return true;
            }

            if (Validation.GetHasError(x1))
            {
                return true;
            }

            if (Validation.GetHasError(x2))
            {
                return true;
            }

            if (Validation.GetHasError(a))
            {
                return true;
            }

            if (Validation.GetHasError(t0))
            {
                return true;
            }

            if (Validation.GetHasError(t1))
            {
                return true;
            }

            if (Validation.GetHasError(GridSpaceText))
            {
                return true;
            }
            return false;
        }

        private void CountButton_Click(object sender, RoutedEventArgs e)
        {
            //var task = (InverseProblem)this.FindResource("InverseProblem1");
            //if (CheckForErrors() == false)
            //{
            //    //MessageBox.Show("Solving");
            //    List<double> g;
            //    double[] t;
            //    task.Solve(out g, out t);
            //    var Model = (MainViewModel)this.DataContext;
            //    Model.UpdateModel(g, t);
            //}
        }
    }
}
