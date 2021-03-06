﻿using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Windows;

namespace WpfApplication1
{
    public class MainWindowViewModel
    {

        public MainWindowViewModel()
        {
            TaskCommand = ReactiveCommand.CreateAsyncTask(_ => { throw new Exception("DIE"); });
            TaskCommand.Subscribe(x => { var dummy = x; });
            TaskCommand.ThrownExceptions
                .Select(ex =>
                    new UserError("Message", "Resolution"))
                .SelectMany(UserError.Throw)
                .Subscribe(result => MessageBox.Show(result.ToString()));

            ObsCommand = ReactiveCommand.CreateAsyncObservable(_ => Observable.Throw<Unit>(new Exception("DIE")));
            ObsCommand.Subscribe(x => { var dummy = x; });
            ObsCommand.ThrownExceptions
                .Select(ex =>
                    new UserError("Message", "Resolution"))
                .SelectMany(UserError.Throw)
                .Subscribe(result => MessageBox.Show(result.ToString()));

        }


        public ReactiveCommand<Unit> TaskCommand { get; set; }
        public ReactiveCommand<Unit> ObsCommand { get; set; }
    }

}
