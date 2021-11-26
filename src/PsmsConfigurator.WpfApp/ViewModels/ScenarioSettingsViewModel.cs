using System.Collections.ObjectModel;
using Prism.Mvvm;
using PsmsConfigurator.WpfApp.Models;

namespace PsmsConfigurator.WpfApp.ViewModels {
  public class ScenarioSettingsViewModel : BindableBase {
    private string _header;
    private Scene _scene;
    
    public ScenarioSettingsViewModel() {
      Scenes = new ObservableCollection<Scene>();
    }

    public ulong EventType { get; private set; }
    
    public string Header {
      get => _header;
      set => SetProperty(ref _header, value);
    }

    public Scene SelectedScene {
      get => _scene;
      set => SetProperty(ref _scene, value);
    }

    public ObservableCollection<Scene> Scenes { get; }

    public void AddScene(Scene scene) {
      Scenes.Add(scene);
      SelectedScene = scene;
    }

    public void Initialize(ulong eventType) => 
      EventType = eventType;
  }
}