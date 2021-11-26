using System.Configuration;
using Prism.Events;
using Prism.Mvvm;
using PsmsConfigurator.WpfApp.Events;

namespace PsmsConfigurator.WpfApp.ViewModels {
  public class MainWindowViewModel : BindableBase {
    private readonly IEventAggregator _eventAggregator;
    private readonly string _defaultTitle;

    private string _title;

    public MainWindowViewModel(IEventAggregator eventAggregator) {
      _eventAggregator = eventAggregator;

      _title = _defaultTitle = ConfigurationManager.AppSettings["app-title"];

      Subscribe();
    }

    public string Title {
      get => _title;
      set => SetProperty(ref _title, value);
    }

    private void Subscribe() {
      _eventAggregator.GetEvent<ConfigurationCreatedEvent>()
        .Subscribe(OnConfigurationCreated);
      _eventAggregator.GetEvent<ConfigurationOpenedEvent>()
        .Subscribe(OnConfigurationOpened);
      _eventAggregator.GetEvent<ConfigurationSavedEvent>()
        .Subscribe(OnConfigurationSaved);
    }

    private void OnConfigurationCreated(ConfigurationEventArgs args) => 
      UpdateTitle(args.FileName);

    private void OnConfigurationOpened(ConfigurationEventArgs args) =>
      UpdateTitle(args.FileName);

    private void OnConfigurationSaved(ConfigurationEventArgs args) =>
      UpdateTitle(args.FileName);

    private void UpdateTitle(string fileName) => 
      Title = $"{_defaultTitle} | {fileName}";
  }
}