namespace PsmsConfigurator.WpfApp.Models {
  public class GroupLoopState {
    public GroupLoopState(byte loopIndex, bool included) {
      LoopIndex = loopIndex;
      Included = included;
    }

    public byte LoopIndex { get; }
    public bool Included { get; set; }
  }
}