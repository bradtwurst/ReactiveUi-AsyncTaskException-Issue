using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Observable
                .FromEventPattern
                <DispatcherUnhandledExceptionEventHandler,
                    DispatcherUnhandledExceptionEventArgs>(
                                                           ev => this.DispatcherUnhandledException += ev,
                                                           ev => this.DispatcherUnhandledException -= ev)
                .Subscribe(evt => OnUnhandledException(evt.EventArgs));

            var disconnectHandler = UserError.RegisterHandler(error =>
            {
                var result = MessageBox.Show(error.ErrorCauseOrResolution,
                                "REACTIVE UI ERROR HANDLER",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);

                return Task.FromResult(RecoveryOptionResult.FailOperation);

            });
        }

        private static void OnUnhandledException(
            DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.ToString(),
                            "ON UNHANDLED EXCEPTION",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
            e.Handled = true;
        }

        protected override void OnStartup(StartupEventArgs e)
        {

            var vm = new MainWindowViewModel();

            var view = new MainWindow { ViewModel = vm, WindowStartupLocation = WindowStartupLocation.CenterScreen };
            view.Activate();
            view.Show();

        }

    }
}
