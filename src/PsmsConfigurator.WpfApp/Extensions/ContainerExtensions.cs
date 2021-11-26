using System.Collections.Generic;
using Prism.Ioc;
using PsmsConfigurator.WpfApp.Models;
using PsmsConfigurator.WpfApp.ViewModels;

namespace PsmsConfigurator.WpfApp.Extensions {
  public static class ContainerExtensions {
    public static ConfigurationViewModel CreateConfigurationViewModel(this IContainerExtension containerExtension,
      Configuration configuration) {
      var configurationViewModel = containerExtension.Resolve<ConfigurationViewModel>();
      configurationViewModel.Initialize(configuration);
      return configurationViewModel;
    }

    public static GroupsConfigurationViewModel CreateGroupsConfigurationVm(this IContainerExtension containerExtension,
      Configuration configuration) {
      var configurationVm = containerExtension.Resolve<GroupsConfigurationViewModel>();
      configurationVm.Initialize(configuration);
      return configurationVm;
    }

    public static GroupSettingsViewModel CreateGroupSettings(this IContainerExtension containerExtension,
      Group settings, Configuration configuration) {
      var settingsVm = containerExtension.Resolve<GroupSettingsViewModel>();
      settingsVm.Initialize(settings, configuration);
      return settingsVm;
    }

    public static GroupLoopStateViewModel CreateGroupLoopStateSettings(this IContainerExtension containerExtension,
      GroupLoopState groupLoopState, Group owner) {
      var stateVm = containerExtension.Resolve<GroupLoopStateViewModel>();
      stateVm.Initialize(groupLoopState, owner);
      return stateVm;
    }

    public static CommonSettingsViewModels CreateCommonParameterViewModels(
      this IContainerExtension containerExtension,
      Common common) {
      var commonParametersViewModel = containerExtension.Resolve<CommonSettingsViewModels>();
      commonParametersViewModel.Initialize(common);
      return commonParametersViewModel;
    }

    public static OutputsConfigurationViewModel CreateOutputsConfigurationVm(this IContainerExtension containerExtension,
      Configuration configuration) {
      var outputsConfigurationVm = containerExtension.Resolve<OutputsConfigurationViewModel>();
      outputsConfigurationVm.Initialize(configuration);
      return outputsConfigurationVm;
    }

    public static OutputSettingsViewModel CreateOutputSettings(this IContainerExtension containerExtension,
      Output output) {
      var vm = containerExtension.Resolve<OutputSettingsViewModel>();
      vm.Initialize(output);
      return vm;
    }

    public static LoopSettingsViewModel CreateLoopSettings(this IContainerExtension containerExtension,
      Loop loop) {
      var loopSettingsViewModel = containerExtension.Resolve<LoopSettingsViewModel>();
      loopSettingsViewModel.Initialize(loop);
      return loopSettingsViewModel;
    }

    public static LoopsConfigurationViewModel CreateLoopsConfigurationViewModel(
      this IContainerExtension containerExtension,
      Configuration configuration) {
      var loopsConfigurationViewModel = containerExtension.Resolve<LoopsConfigurationViewModel>();
      loopsConfigurationViewModel.Initialize(configuration);
      return loopsConfigurationViewModel;
    }

    public static ScenariosConfigurationViewModel CreateScenariosConfigurationVm(
      this IContainerExtension containerExtension,
      Configuration configuration) {
      var scenariosConfigurationVm = containerExtension.Resolve<ScenariosConfigurationViewModel>();
      scenariosConfigurationVm.Initialize(configuration);
      return scenariosConfigurationVm;
    }

    public static ScenarioSettingsViewModel CreateScenarioSettings(
      this IContainerExtension containerExtension, ulong type) {
      var scenarioSettingsVm = containerExtension.Resolve<ScenarioSettingsViewModel>();
      scenarioSettingsVm.Initialize(type);
      return scenarioSettingsVm;
    }
  }
}