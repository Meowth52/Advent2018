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
using System.Diagnostics;
namespace Advent2018
{
    public partial class MainWindow : Window
    {
        int LastDay = 1;
        public int ChoosenDay;
        private readonly MainView _mainView;
        public MainWindow()
        {
            InitializeComponent();
            _mainView = DataContext as MainView;
            InputBox.Focus();
            ChoosenDay = LastDay;
            DayBox.Text = ChoosenDay.ToString();
        }
        private async void InputBoxKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                Day d = CanIHasDay(ChoosenDay, InputBox.Text);
                d.SetMainView(_mainView);
                Tuple<string,string> OutputTuple;
                await Task.Run(() =>
                {
                    OutputTuple = d.getResult();
                    stopWatch.Stop();
                    TimeSpan ts = stopWatch.Elapsed;
                    _mainView.OutText = "Del 1: " + OutputTuple.Item1 + " och del 2: " + OutputTuple.Item2 + " Executed in " + ts.TotalMilliseconds.ToString() + " ms";
                });
            }
        }
        private void DayBoxKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                try
                {
                    ChoosenDay = Int32.Parse(DayBox.Text);
                    DayBox.Text = ChoosenDay.ToString();
                }
                catch
                {
                    DayBox.Text = ChoosenDay.ToString();
                }
            }

        }
        private Day CanIHasDay(int _day, string _input)
        {
            Day ReturnDay;
            switch (_day)
            {
                case 1:
                    ReturnDay = new Day01(_input);
                    break;
                default:
                    ReturnDay = new Day01(_input);
                    break;
            }
            return ReturnDay;
        }
    }
}
