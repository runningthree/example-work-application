using System.Collections.Generic;

namespace PsmsConfigurator.Shared.Models {
  public class CommonSpecification {
    public int Index { get; set; }
    public int Size { get; set; }
    public int LedOnNormalLightIndex { get; set; }
    public int LedOnNormalLightSize { get; set; }
    public int LedOnNormalLightShift { get; set; }
    public ushort LedOnNormalLightMask { get; set; }
    public bool LedOnNormalLightDefaultValue { get; set; }
    public int FireLoopVerifyIndex { get; set; }
    public int FireLoopVerifySize { get; set; }
    public int FireLoopVerifyShift { get; set; }
    public ushort FireLoopVerifyMask { get; set; }
    public bool FireLoopVerifyDefaultValue { get; set; }
    public int RfidUsedIndex { get; set; }
    public int RfidUsedSize { get; set; }
    public int RfidUsedShift { get; set; }
    public ushort RfidUsedMask { get; set; }
    public bool RfidUsedDefaultValue { get; set; }
  }

  public class LoopsConfigurationSpecification {
    public int Index { get; set; }
    public int Size { get; set; }
    public int MaxCount { get; set; }
    public int MinCount { get; set; }
    public int DefaultCount { get; set; }
    public string NamePrefix { get; set; }
    public int LoopIndex { get; set; }
    public int LoopSize { get; set; }
    public int DelayIndex { get; set; }
    public int DelaySize { get; set; }
    public ushort DelayMask { get; set; }
    public int DelayShift { get; set; }
    public ushort DelayMaxValue { get; set; }
    public ushort DelayDefaultValue { get; set; }
    public ushort DelayMinValue { get; set; }
    public int VerifiedIndex { get; set; }
    public int VerifiedSize { get; set; }
    public int VerifiedShift { get; set; }
    public ushort VerifiedMask { get; set; }
    public bool VerifiedDefaultValue { get; set; }
    public int ActualIndex { get; set; }
    public int ActualSize { get; set; }
    public int ActualShift { get; set; }
    public ushort ActualMask { get; set; }
    public bool ActualDefaultValue { get; set; }
  }

  public class OutputsConfigurationSpecification {
    public int Index { get; set; }
    public int Size { get; set; }
    public int MaxCount { get; set; }
    public int MinCount { get; set; }
    public int DefaultCount { get; set; }
    public int OutputIndex { get; set; }
    public int OutputSize { get; set; }
    public string NamePrefix { get; set; }
    public int LocalControlIndex { get; set; }
    public int LocalControlSize { get; set; }
    public int LocalControlShift { get; set; }
    public ushort LocalControlMask { get; set; }
    public bool LocalControlDefaultValue { get; set; }
    public int IsSoundIndex { get; set; }
    public int IsSoundSize { get; set; }
    public int IsSoundShift { get; set; }
    public ushort IsSoundMask { get; set; }
    public bool IsSoundDefaultValue { get; set; }
    public int ActualIndex { get; set; }
    public int ActualSize { get; set; }
    public int ActualShift { get; set; }
    public ushort ActualMask { get; set; }
    public bool ActualDefaultValue { get; set; }
  }

  public class GroupsConfigurationSpecification {
    public int Index { get; set; }
    public int Size { get; set; }
    public int MaxCount { get; set; }
    public int MinCount { get; set; }
    public int DefaultCount { get; set; }
    public string NamePrefix { get; set; }
    public int StateIndex { get; set; }
    public int StateSize { get; set; }
    public int ActualIndex { get; set; }
    public int ActualSize { get; set; }
    public uint ActualMask { get; set; }
    public int ActualShift { get; set; }
    public bool ActualDefaultValue { get; set; }
    public int GroupIndex { get; set; }
    public int GroupSize { get; set; }
    public int LoopStateIncludedIndex { get; set; }
    public int LoopStateIncludedSize { get; set; }
    public ulong LoopStateIncludedMask { get; set; }
    public int LoopStateIncludedShift { get; set; }
    public bool LoopStateIncludedDefaultValue { get; set; }
  }

  public class ScenesConfigurationSpecification {
    public int Index { get; set; }
    public int Size { get; set; }
    public int SceneIndex { get; set; }
    public int SceneSize { get; set; }
    public int MaxCount { get; set; }
    public int DefaultCount { get; set; }
    public int EventTypeIndex { get; set; }
    public int EventTypeShift { get; set; }
    public uint EventTypeMask { get; set; }
    public int EventTypeSize { get; set; }
    public ulong EventTypeDefaultValue { get; set; }
    public ulong DefaultEventTypesIndex { get; set; }
    public Dictionary<ulong, string> StandardEventTypes { get; set; }
    public ulong ManualStartEventTypesIndex { get; set; }
    public Dictionary<ulong, string> ManualStartEventTypes { get; set; }
    public ulong LoopEventTypesStartIndex { get; set; }
    public ulong GroupEventTypeStartIndex { get; set; }
    public int OutputIndex { get; set; }
    public int OutputShift { get; set; }
    public ulong OutputMask { get; set; }
    public int OutputSize { get; set; }
    public ulong OutputDefaultValue { get; set; }
    public int ModeIndex { get; set; }
    public int ModeShift { get; set; }
    public ulong ModeMask { get; set; }
    public int ModeSize { get; set; }
    public ulong ModeDefaultValue { get; set; }
    public Dictionary<ulong, string> ModeTypes { get; set; }
    public int DurationIndex { get; set; }
    public int DurationShift { get; set; }
    public ulong DurationMask { get; set; }
    public int DurationSize { get; set; }
    public ulong DurationMaxValue { get; set; }
    public ulong DurationMinValue { get; set; }
    public ulong DurationDefaultValue { get; set; }
    public int DelayIndex { get; set; }
    public int DelayShift { get; set; }
    public ulong DelayMask { get; set; }
    public int DelaySize { get; set; }
    public ulong DelayMaxValue { get; set; }
    public ulong DelayMinValue { get; set; }
    public ulong DelayDefaultValue { get; set; }
  }

  public class Specification {
    public int Length { get; set; }
    public CommonSpecification Common { get; set; }
    public LoopsConfigurationSpecification Loops { get; set; }
    public OutputsConfigurationSpecification Outputs { get; set; }
    public GroupsConfigurationSpecification Groups { get; set; }
    public ScenesConfigurationSpecification Scenes { get; set; }

    public static Specification Default = new Specification {
      Length = 3526,
      Common = new CommonSpecification {
        Index = 0,
        Size = 2,
        LedOnNormalLightIndex = 0,
        LedOnNormalLightShift = 0,
        LedOnNormalLightMask = 1,
        LedOnNormalLightSize = 2,
        LedOnNormalLightDefaultValue = false,
        FireLoopVerifyIndex = 0,
        FireLoopVerifyShift = 1,
        FireLoopVerifyMask = 1,
        FireLoopVerifySize = 2,
        FireLoopVerifyDefaultValue = false,
        RfidUsedIndex = 0,
        RfidUsedShift = 2,
        RfidUsedMask = 1,
        RfidUsedSize = 2,
        RfidUsedDefaultValue = false
      },
      Loops = new LoopsConfigurationSpecification {
        Index = 2,
        Size = 128,
        MaxCount = 64,
        MinCount = 8,
        DefaultCount = 8,
        NamePrefix = "ШС",
        LoopIndex = 0,
        LoopSize = 2,
        DelayIndex = 0,
        DelayShift = 0,
        DelayMask = 0x0fff,
        DelaySize = 2,
        DelayMaxValue = 0x0fff,
        DelayDefaultValue = 5,
        DelayMinValue = 0,
        VerifiedIndex = 0,
        VerifiedShift = 12,
        VerifiedMask = 1,
        VerifiedSize = 2,
        VerifiedDefaultValue = false,
        ActualIndex = 0,
        ActualShift = 13,
        ActualMask = 1,
        ActualSize = 2,
        ActualDefaultValue = false
      },
      Outputs = new OutputsConfigurationSpecification {
        Index = 130,
        Size = 64,
        MaxCount = 32,
        MinCount = 8,
        DefaultCount = 8,
        OutputIndex = 0,
        OutputSize = 2,
        NamePrefix = "Выход",
        LocalControlIndex = 0,
        LocalControlShift = 0,
        LocalControlMask = 1,
        LocalControlSize = 2,
        LocalControlDefaultValue = false,
        IsSoundIndex = 0,
        IsSoundShift = 1,
        IsSoundMask = 1,
        IsSoundSize = 2,
        IsSoundDefaultValue = false,
        ActualIndex = 0,
        ActualShift = 2,
        ActualMask = 1,
        ActualSize = 2,
        ActualDefaultValue = false
      },
      Groups = new GroupsConfigurationSpecification {
        Index = 194,
        Size = 260,
        MaxCount = 32,
        MinCount = 8,
        DefaultCount = 8,
        NamePrefix = "Группа",
        StateIndex = 0,
        StateSize = 4,
        ActualIndex = 0,
        ActualShift = 1,
        ActualMask = 1,
        ActualSize = 4,
        ActualDefaultValue = false,
        GroupIndex = 4,
        GroupSize = 8,
        LoopStateIncludedIndex = 0,
        LoopStateIncludedShift = 1,
        LoopStateIncludedMask = 1,
        LoopStateIncludedSize = 8,
        LoopStateIncludedDefaultValue = false,
      },
      Scenes = new ScenesConfigurationSpecification {
        Index = 454,
        Size = 3072,
        SceneIndex = 0,
        SceneSize = 6,
        MaxCount = 512,
        DefaultCount = 0,
        EventTypeIndex = 0,
        EventTypeShift = 0,
        EventTypeMask = 0xff,
        EventTypeSize = 4,
        EventTypeDefaultValue = 0,
        DefaultEventTypesIndex = 0,
        StandardEventTypes = new Dictionary<ulong, string> {
          {1, "пожар"},
          {2, "внимание"},
          {3, "неисправность"},
          {4, "тревога"},
          {5, "тест"}
        },
        ManualStartEventTypesIndex = 32,
        ManualStartEventTypes = new Dictionary<ulong, string> {
          {1, "ручной запуск 1"},
          {2, "ручной запуск 2"},
          {3, "ручной запуск 3"},
          {4, "ручной запуск 4"},
          {5, "ручной запуск 5"},
          {6, "ручной запуск 6"},
          {7, "ручной запуск 7"},
          {8, "ручной запуск 8"}
        },
        LoopEventTypesStartIndex = 128,
        GroupEventTypeStartIndex = 64,
        OutputIndex = 0,
        OutputShift = 8,
        OutputMask = 0x3f,
        OutputSize = 4,
        OutputDefaultValue = 0,
        ModeIndex = 0,
        ModeShift = 14,
        ModeMask = 0x0f,
        ModeSize = 4,
        ModeDefaultValue = 0,
        ModeTypes = new Dictionary<ulong, string> {
          {0, "постоянно включен"},
          {1, "переключается с частотой"}
        },
        DurationIndex = 4,
        DurationMask = 0xff,
        DurationShift = 0,
        DurationMinValue = byte.MinValue,
        DurationMaxValue = byte.MaxValue,
        DurationDefaultValue = 5,
        DelayIndex = 5,
        DelayShift = 0,
        DelayMask = 0xff,
        DelayMinValue = byte.MinValue,
        DelayMaxValue = byte.MaxValue,
        DelayDefaultValue = 5
      }
    };
  }
}