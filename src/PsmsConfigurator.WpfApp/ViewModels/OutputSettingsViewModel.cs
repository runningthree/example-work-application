using PsmsConfigurator.WpfApp.Models;

namespace PsmsConfigurator.WpfApp.ViewModels {
  public class OutputSettingsViewModel {
    public Output Model { get; private set; }

    public string Name =>
      Model.Name;

    public bool LocalControl {
      get => Model.LocalControl;
      set => Model.LocalControl = value;
    }

    public bool IsSound {
      get => Model.IsSound;
      set => Model.IsSound = value;
    }

    public void Initialize(Output output) =>
      Model = output;
  }
}