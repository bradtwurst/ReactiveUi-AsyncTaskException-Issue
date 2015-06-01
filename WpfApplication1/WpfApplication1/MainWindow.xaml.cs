using ReactiveUI;
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

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IViewFor<MainWindowViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();

            this.WhenActivated(d =>
            {
                d(this.BindCommand(ViewModel, vm => vm.TaskCommand, v => v.TaskCmdBound));
                d(this.BindCommand(ViewModel, vm => vm.ObsCommand, v => v.ObsCmdBound));
            });
        }

        public static readonly DependencyProperty ViewModelProperty;

        static MainWindow()
        {
            ViewModelProperty = DependencyProperty.Register("ViewModel", typeof(MainWindowViewModel), typeof(MainWindow));
        }

        public MainWindowViewModel ViewModel
        {
            get { return (MainWindowViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (MainWindowViewModel)value; }
        }

        private void TaskExeDirect_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.TaskCommand.Execute(null);
        }

        private void ObsExeDirect_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.ObsCommand.Execute(null);
        }

        private void TaskExeAsyncDirect_Click(object sender, RoutedEventArgs e)
        {
            new Action(async () =>
            {
                await ViewModel.TaskCommand.ExecuteAsyncTask();
            }).Invoke();

        }
    }
}
