namespace PsmsConfigurator.WpfApp.Models {
  public class Loop {
    public Loop(byte index, string name, bool actual,
      ushort delay, bool verified) {
      Index = index;
      Name = name;
      Actual = actual;
      Delay = delay;
      Verified = verified;
    }

    public byte Index { get; }
    public string Name { get; }
    public bool Actual { get; set; }
    public ushort Delay { get; set; }
    public bool Verified { get; set; }
  }
}