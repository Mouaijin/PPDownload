using System.Reactive.Disposables;
using PPDownload.GUI.ViewModels;
using PPDownload.Models;
using ReactiveUI;

namespace PPDownload.GUI
{
    /// <summary>
    /// Interaction logic for LibrarySearchControl.xaml
    /// </summary>
    public partial class LibrarySearchResultView : ReactiveUserControl<LibrarySearchResultViewModel>
    {
        public LibrarySearchResultView()
        {
            InitializeComponent();
            this.WhenActivated
                (disposableRegistration =>
                 {
                     this.OneWayBind(ViewModel,
                                     viewModel => viewModel.Title,
                                     view => view.Title.Content)
                         .DisposeWith(disposableRegistration);
                     this.OneWayBind(ViewModel,
                                     viewModel => viewModel.Author,
                                     view => view.Author.Content)
                         .DisposeWith(disposableRegistration);
                     this.OneWayBind(ViewModel,
                                     viewModel => viewModel.Description,
                                     view => view.Description.Text)
                         .DisposeWith(disposableRegistration);
                     this.OneWayBind(ViewModel,
                                     viewModel => viewModel.Uploaded,
                                     view => view.UploadDate.DisplayDate)
                         .DisposeWith(disposableRegistration);
                     this.OneWayBind(ViewModel,
                                     viewModel => viewModel.Easy,
                                     view => view.Easy.Text)
                         .DisposeWith(disposableRegistration);
                     this.OneWayBind(ViewModel,
                                     viewModel => viewModel.Normal,
                                     view => view.Normal.Text)
                         .DisposeWith(disposableRegistration);
                     this.OneWayBind(ViewModel,
                                     viewModel => viewModel.Hard,
                                     view => view.Hard.Text)
                         .DisposeWith(disposableRegistration);
                     this.OneWayBind(ViewModel,
                                     viewModel => viewModel.Extreme,
                                     view => view.Extreme.Text)
                         .DisposeWith(disposableRegistration);
                     this.OneWayBind(ViewModel,
                                     viewModel => viewModel.Downloads,
                                     view => view.Downloads.Text)
                         .DisposeWith(disposableRegistration);
                     this.OneWayBind(ViewModel,
                                     viewModel => viewModel.BPM,
                                     view => view.BPM.Text)
                         .DisposeWith(disposableRegistration);
                     this.OneWayBind(ViewModel,
                                     viewModel => viewModel.EvaluationString,
                                     view => view.Evaluation.Text)
                         .DisposeWith(disposableRegistration);
                     this.OneWayBind(ViewModel,
                                     viewModel => viewModel.Length,
                                     view => view.Length.Text)
                         .DisposeWith(disposableRegistration);
                 }
                );
        }
    }
}