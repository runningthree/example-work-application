namespace PsmsConfigurator.WpfApp.Models {
  /// <summary>
  /// Состав групп
  /// </summary>
  public class Group {
    public Group(byte index, string name, bool actual, 
      GroupLoopState[] groupLoopStates) {
      Index = index;
      Name = name;
      Actual = actual;
      LoopStates = groupLoopStates;
    }

    public byte Index { get; }
    public string Name { get; }
    public bool Actual { get; set; }
    public GroupLoopState[] LoopStates { get; }
  }
}