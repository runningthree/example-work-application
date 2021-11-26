using PsmsConfigurator.WpfApp.Models;

namespace PsmsConfigurator.WpfApp.ViewModels {
  public class CommonSettingsViewModels {
    private Common _common;

    public bool LedOnNormalLight {
      get => _common.LedOnNormalLight;
      set => _common.LedOnNormalLight = value;
    }

    public bool FirePlumeVerification {
      get => _common.FirePlumeVerification;
      set => _common.FirePlumeVerification = value;
    }

    public bool RfiUsed {
      get => _common.RfiUsed;
      set => _common.RfiUsed = value;
    }

    public void Initialize(Common settings) => 
      _common = settings;
  }
}