using PsmsConfigurator.WpfApp.Models;

namespace PsmsConfigurator.WpfApp.ViewModels {
  public class LoopSettingsViewModel {
    public Loop Model { get; private set; }

    public string Name =>
      Model.Name;

    public ushort Delay {
      get => Model.Delay;
      set => Model.Delay = value;
    }

    public bool Verified {
      get => Model.Verified;
      set => Model.Verified = value;
    }
    
    public void Initialize(Loop loop) => 
      Model = loop;
  }
}