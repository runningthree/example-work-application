using System;
using System.IO;
using System.Windows;
using Newtonsoft.Json;
using Prism.Ioc;
using Prism.Unity;
using PsmsConfigurator.Shared.Models;
using PsmsConfigurator.WpfApp.Models;
using PsmsConfigurator.WpfApp.ViewModels;
using PsmsConfigurator.WpfApp.Views;
using IMessageBoxService = MvvmDialogs.IDialogService;
using MessageBoxService = MvvmDialogs.DialogService;

namespace PsmsConfigurator.WpfApp {
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : PrismApplication {
    protected override void RegisterTypes(IContainerRegistry containerRegistry) {
      containerRegistry.RegisterSingleton<ApplicationState>();
      containerRegistry.RegisterSingleton<IMessageBoxService, MessageBoxService>();
      
      containerRegistry.RegisterDialog<CustomMessageBox, CustomMessageBoxViewModel>();
      containerRegistry.RegisterDialog<CreateScenarioWindow, CreateScenarioWindowViewModel>();
      containerRegistry.RegisterDialog<EditSceneWindow, EditSceneWindowViewModel>();

      RegisterSpecifications(containerRegistry);
    }

    private static void RegisterSpecifications(IContainerRegistry containerRegistry) {
      var specificationFileContent = File.ReadAllText("specification.json");
      var configurationSpecification =
        JsonConvert.DeserializeObject<Specification>(specificationFileContent);
      containerRegistry.RegisterInstance(configurationSpecification);
    }

    protected override Window CreateShell() => 
      Container.Resolve<MainWindow>();
  }
}