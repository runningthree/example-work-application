namespace PsmsConfigurator.WpfApp.Models {
  /// <summary>
  /// Конфигурация.
  /// </summary>
  public class Configuration {
    public Configuration(Common common, Loop[] loops, Output[] outputs, 
      Group[] groups, Scene[] scenes) {
      Common = common;
      Loops = loops;
      Outputs = outputs;
      Groups = groups;
      Scenes = scenes;
    }

    public Common Common { get; }
    public Loop[] Loops { get; }
    public Output[] Outputs { get; }
    public Group[] Groups { get; }
    public Scene[] Scenes { get; }
  }
}