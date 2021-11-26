using System.Collections.Generic;

namespace PsmsConfigurator.WpfApp.Models {
  public class ApplicationState {
    private readonly IDictionary<byte, string> _eventTypes;
    private readonly IDictionary<byte, string> _outputs;
    private readonly IDictionary<byte, string> _outBlinkingTypes;
    private readonly IList<string> _groups;
    private readonly IList<string> _loops;

    public ApplicationState() {
      _eventTypes = new Dictionary<byte, string> {
        {0, "Событие 1"},
        {1, "Событие 2"},
        {2, "Событие 3"},
        {3, "Событие 4"},
        {4, "Событие 5"},
        {5, "Событие 6"},
        {6, "Событие 7"},
        {7, "Событие 8"}
      };
      _outputs = new Dictionary<byte, string> {
        {0, "Выход 1"},
        {1, "Выход 2"},
        {2, "Выход 3"},
        {3, "Выход 4"},
        {4, "Выход 5"},
        {5, "Выход 6"},
        {6, "Выход 7"},
        {7, "Выход 8"}
      };
      _outBlinkingTypes = new Dictionary<byte, string> {
        {0, "Тип 1"},
        {1, "Тип 2"},
        {2, "Тип 3"},
        {3, "Тип 4"},
        {4, "Тип 5"},
        {5, "Тип 6"},
        {6, "Тип 7"},
        {7, "Тип 8"}
      };
      _groups = new List<string> {
        "Группа 1",
        "Группа 2",
        "Группа 3",
        "Группа 4",
        "Группа 5",
        "Группа 6",
        "Группа 7",
        "Группа 8"
      };
      _loops = new List<string> {
        "ШС 1",
        "ШС 2",
        "ШС 3",
        "ШС 4",
        "ШС 5",
        "ШС 6",
        "ШС 7",
        "ШС 8",
        "ШС 9",
        "ШС 10",
        "ШС 11",
        "ШС 12",
        "ШС 13",
        "ШС 14",
        "ШС 15",
        "ШС 16"
      };
    }

    public Configuration Configuration { get; set; }

    public IDictionary<byte, string> EventTypes =>
      _eventTypes;

    public IDictionary<byte, string> Outputs =>
      _outputs;

    public IDictionary<byte, string> OutBlinkingTypes =>
      _outBlinkingTypes;

    public IList<string> Groups =>
      _groups;

    public IList<string> Loops =>
      _loops;
  }
}