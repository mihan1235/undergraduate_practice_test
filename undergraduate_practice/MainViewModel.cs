using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Series;
using System.Windows;
using System.ComponentModel;
//using Volter2;

namespace undergraduate_practice
{
    public class MainViewModel: INotifyPropertyChanged

    {
        public MainViewModel(){ }

        public void UpdateModel(List<double> g, double[] t)
        {
            this.MyModel = new PlotModel { Title = "Inverse Problem" };
            

            LineSeries LineSeries1 = new LineSeries();
            for(int i =0; i<g.Count;i++)
            {
                LineSeries1.Points.Add(new DataPoint(t[i], g.ElementAt(i)));
            }
            LineSeries1.Title = "g(t)";
            this.MyModel.Series.Add(LineSeries1);
            OnPropertyChanged("MyModel");
        }

        //public void TestModel(Func<double, double> g_t_exact)
        //{
        //    //Func<double, double>  = (t) => t - Math.Pow(t, 2) / 2;
        //    this.MyModel = new PlotModel { Title = "Inverse Problem" };
        //    this.MyModel.Series.Add(new FunctionSeries(g_t_exact, 0, 5, 0.01, "g(t) test"));
        //    OnPropertyChanged("MyModel");
        //}

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
                
        }

        public PlotModel MyModel { get; private set; }

    }
}
