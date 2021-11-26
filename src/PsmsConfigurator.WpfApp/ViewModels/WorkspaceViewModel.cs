using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using PsmsConfigurator.WpfApp.Events;
using PsmsConfigurator.WpfApp.Extensions;
using PsmsConfigurator.WpfApp.Models;

namespace PsmsConfigurator.WpfApp.ViewModels {
  public class WorkspaceViewModel : BindableBase {
    private readonly IContainerExtension _containerExtension;
    private readonly IEventAggregator _eventAggregator;

    private object _content;

    public WorkspaceViewModel(IContainerExtension containerExtension, IEventAggregator eventAggregator) {
      _containerExtension = containerExtension;
      _eventAggregator = eventAggregator;
      Subscribe();
    }

    public object Content {
      get => _content;
      set => SetProperty(ref _content, value);
    }

    private void Subscribe() {
      _eventAggregator.GetEvent<ConfigurationCreatedEvent>()
        .Subscribe(OnConfigurationCreated);
    }

    private void OnConfigurationCreated(ConfigurationEventArgs args) {
      Content = _containerExtension.CreateConfigurationViewModel(args.Configuration);
    }
  }
}