namespace PsmsConfigurator.WpfApp.Models {
  /// <summary>
  /// Описание сцены
  /// </summary>
  public class Scene {
    public Scene(ulong eventType, ulong output, ulong mode,
      ulong duration, ulong delay) {
      EventType = eventType;
      Output = output;
      Mode = mode;
      Duration = duration;
      Delay = delay;
    }

    /// <summary>
    /// Тип событие
    /// </summary>
    public ulong EventType { get; set; }

    /// <summary>
    /// Номер выхода
    /// </summary>
    public ulong Output { get; set; }

    /// <summary>
    /// Тип включения выхода
    /// </summary>
    public ulong Mode { get; set; }

    /// <summary>
    /// Продолжительность включения
    /// </summary>
    public ulong Duration { get; set; }

    /// <summary>
    /// Задержка включения
    /// </summary>
    public ulong Delay { get; set; }
  }
}