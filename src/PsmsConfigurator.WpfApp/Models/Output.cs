namespace PsmsConfigurator.WpfApp.Models {
  /// <summary>
  /// Параметры выходов 
  /// </summary>
  public class Output {
    public Output(byte index, string name, bool actual, 
      bool localControl, bool isSound) {
      Index = index;
      Name = name;
      Actual = actual;
      LocalControl = localControl;
      IsSound = isSound;
    }

    public byte Index { get; }
    public string Name { get; }
    public bool Actual { get; set; }
    public bool LocalControl { get; set; }
    public bool IsSound { get; set; }
  }
}