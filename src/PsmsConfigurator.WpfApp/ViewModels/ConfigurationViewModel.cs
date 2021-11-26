using Prism.Ioc;
using PsmsConfigurator.WpfApp.Extensions;
using PsmsConfigurator.WpfApp.Models;

namespace PsmsConfigurator.WpfApp.ViewModels {
  public class ConfigurationViewModel {
    private readonly IContainerExtension _containerExtension;

    public ConfigurationViewModel(IContainerExtension containerExtension) {
      _containerExtension = containerExtension;
    }

    public CommonSettingsViewModels CommonSettings { get; private set; }
    public LoopsConfigurationViewModel Loops { get; private set; }
    public OutputsConfigurationViewModel Outputs { get; private set; }
    public GroupsConfigurationViewModel Groups { get; private set; }
    public ScenariosConfigurationViewModel Scenarios { get; private set; }

    public void Initialize(Configuration configuration) {
      CommonSettings = _containerExtension.CreateCommonParameterViewModels(configuration.Common);
      Loops = _containerExtension.CreateLoopsConfigurationViewModel(configuration);
      Outputs = _containerExtension.CreateOutputsConfigurationVm(configuration);
      Groups = _containerExtension.CreateGroupsConfigurationVm(configuration);
      Scenarios = _containerExtension.CreateScenariosConfigurationVm(configuration);
    }
  }
}