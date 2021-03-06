﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
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
using PPDownload.GUI.ViewModels;
using ReactiveUI;

namespace PPDownload.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ReactiveWindow<LibrarySearchViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new LibrarySearchViewModel();
            this.WhenActivated(disposableRegistration =>
                               {
                                   this.Bind(ViewModel,
                                             viewModel => viewModel.SearchText,
                                             view => view.SearchBox.Text)
                                       .DisposeWith(disposableRegistration);

                                   this.OneWayBind(ViewModel,
                                                   viewModel => viewModel.SearchResults,
                                                   view => view.SearchResults.ItemsSource)
                                       .DisposeWith(disposableRegistration);

                                   this.BindCommand(ViewModel,
                                                    viewModel => viewModel.SearchLibrary,
                                                    view => view.SearchButton)
                                       .DisposeWith(disposableRegistration);
                               });
        }
    }
}