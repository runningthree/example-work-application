using System.Linq;
using PsmsConfigurator.Shared.Models;
using PsmsConfigurator.WpfApp.Extensions;
using PsmsConfigurator.WpfApp.Models;

namespace PsmsConfigurator.WpfApp.Converters {
  public class ConfigurationConverter {
    public byte[] Convert(Configuration configuration, Specification specification) {
      var rawData = new byte[specification.Length];

      var commonSpec = specification.Common;
      var commonData = new byte[commonSpec.Size];
      commonData.SetUshort((ushort) (configuration.Common.LedOnNormalLight ? 1 : 0),
        commonSpec.LedOnNormalLightIndex, commonSpec.LedOnNormalLightSize,
        commonSpec.LedOnNormalLightMask, commonSpec.LedOnNormalLightShift);
      commonData.SetUshort((ushort) (configuration.Common.FirePlumeVerification ? 1 : 0),
        commonSpec.FireLoopVerifyIndex, commonSpec.FireLoopVerifySize,
        commonSpec.FireLoopVerifyMask, commonSpec.FireLoopVerifyShift);
      commonData.SetUshort((ushort) (configuration.Common.RfiUsed ? 1 : 0),
        commonSpec.RfidUsedIndex, commonSpec.RfidUsedSize,
        commonSpec.RfidUsedMask, commonSpec.RfidUsedShift);
      rawData.SetByteRange(commonData, commonSpec.Index, commonData.Length);

      var loopsSpec = specification.Loops;
      var loopsData = new byte[loopsSpec.Size];
      for (var loopIndex = 0; loopIndex < configuration.Loops.Length; loopIndex++) {
        var loopData = new byte[loopsSpec.LoopSize];
        var loop = configuration.Loops[loopIndex];
        loopData.SetUshort(loop.Delay, loopsSpec.DelayIndex, loopsSpec.DelaySize,
          loopsSpec.DelayMask, loopsSpec.DelayShift);
        loopData.SetUshort((ushort) (loop.Verified ? 1 : 0), loopsSpec.VerifiedIndex,
          loopsSpec.VerifiedSize, loopsSpec.VerifiedMask, loopsSpec.VerifiedShift);
        loopData.SetUshort((ushort) (loop.Actual ? 1 : 0), loopsSpec.ActualIndex,
          loopsSpec.ActualSize, loopsSpec.ActualMask, loopsSpec.ActualShift);
        loopsData.SetByteRange(loopData, loopsSpec.LoopIndex + loopsSpec.LoopSize * loopIndex,
          loopsSpec.LoopSize);
      }

      rawData.SetByteRange(loopsData, loopsSpec.Index, loopsSpec.Size);

      var outputsSpec = specification.Outputs;
      var outputsData = new byte[outputsSpec.Size];
      for (var outputIndex = 0; outputIndex < configuration.Outputs.Length; outputIndex++) {
        var outputData = new byte[outputsSpec.OutputSize];
        var output = configuration.Outputs[outputIndex];
        outputData.SetUshort((ushort) (output.LocalControl ? 1 : 0),
          outputsSpec.LocalControlIndex, outputsSpec.LocalControlSize,
          outputsSpec.LocalControlMask, outputsSpec.LocalControlShift);
        outputData.SetUshort((ushort) (output.IsSound ? 1 : 0),
          outputsSpec.IsSoundIndex, outputsSpec.IsSoundSize,
          outputsSpec.IsSoundMask, outputsSpec.IsSoundShift);
        outputData.SetUshort((ushort) (output.Actual ? 1 : 0),
          outputsSpec.ActualIndex, outputsSpec.ActualSize,
          outputsSpec.ActualMask, outputsSpec.ActualShift);
        outputsData.SetByteRange(outputData, outputsSpec.OutputIndex + outputsSpec.OutputSize * outputIndex,
          outputsSpec.OutputSize);
      }

      rawData.SetByteRange(outputsData, outputsSpec.Index, outputsSpec.Size);

      var groupsSpec = specification.Groups;
      var groupStateData = new byte[groupsSpec.StateSize];
      var groupsData = new byte[groupsSpec.Size];
      for (var groupIndex = 0; groupIndex < configuration.Groups.Length; groupIndex++) {
        var group = configuration.Groups[groupIndex];
        groupStateData.SetUint((uint) (group.Actual ? 1 : 0),
          groupsSpec.ActualIndex, groupsSpec.ActualSize,
          groupsSpec.ActualMask, groupsSpec.ActualShift * groupIndex);
        var groupData = new byte[groupsSpec.GroupSize];
        for (var loopStateIndex = 0; loopStateIndex < group.LoopStates.Length; loopStateIndex++) {
          var loopState = group.LoopStates[loopStateIndex];
          groupData.SetUlong((ulong) (loopState.Included ? 1 : 0),
            groupsSpec.LoopStateIncludedIndex, groupsSpec.LoopStateIncludedSize,
            groupsSpec.LoopStateIncludedMask, groupsSpec.LoopStateIncludedShift * loopStateIndex);
        }

        groupsData.SetByteRange(groupData, groupsSpec.GroupIndex + groupsSpec.GroupSize * groupIndex,
          groupsSpec.GroupSize);
      }

      groupsData.SetByteRange(groupStateData, groupsSpec.StateIndex, groupsSpec.StateSize);
      rawData.SetByteRange(groupsData, groupsSpec.Index, groupsSpec.Size);

      var scenesSpec = specification.Scenes;
      var scenesData = new byte[scenesSpec.Size];
      var sortedScenes = configuration.Scenes
        .OrderBy(scene => scene.EventType)
        .ToArray();
      for (var sceneIndex = 0; sceneIndex < sortedScenes.Length; sceneIndex++) {
        var sceneData = new byte[scenesSpec.SceneSize];
        var scene = sortedScenes[sceneIndex];

        if (scene.EventType == scenesSpec.EventTypeDefaultValue)
          for (var sceneDataIndex = 0; sceneDataIndex < sceneData.Length; sceneDataIndex++)
            sceneData[sceneDataIndex] = 0xFF;
        else {
          sceneData.SetUlong(scene.EventType, scenesSpec.EventTypeIndex,
            scenesSpec.EventTypeSize, scenesSpec.EventTypeMask, scenesSpec.EventTypeShift);
          sceneData.SetUlong(scene.Output, scenesSpec.OutputIndex,
            scenesSpec.OutputSize, scenesSpec.OutputMask, scenesSpec.OutputShift);
          sceneData.SetUlong(scene.Mode, scenesSpec.ModeIndex,
            scenesSpec.ModeSize, scenesSpec.ModeMask, scenesSpec.ModeShift);
          sceneData.SetUlong(scene.Duration, scenesSpec.DurationIndex, scenesSpec.DurationSize,
            scenesSpec.DurationMask, scenesSpec.DurationShift);
          sceneData.SetUlong(scene.Delay, scenesSpec.DelayIndex, scenesSpec.DelaySize,
             scenesSpec.DelayMask, scenesSpec.DelayShift);
        }

        scenesData.SetByteRange(sceneData, scenesSpec.SceneIndex + scenesSpec.SceneSize * sceneIndex,
          scenesSpec.SceneSize);
      }

      rawData.SetByteRange(scenesData, scenesSpec.Index, scenesSpec.Size);

      return rawData;
    }

    public Configuration Convert(byte[] rawData, Specification specification) {
      var commonSpec = specification.Common;
      var commonData = rawData.GetRange(commonSpec.Index, commonSpec.Size);
      var common = new Common(
        commonData.GetUshort(commonSpec.LedOnNormalLightIndex, commonSpec.LedOnNormalLightSize,
          commonSpec.LedOnNormalLightMask, commonSpec.LedOnNormalLightShift) != 0,
        commonData.GetUshort(commonSpec.FireLoopVerifyIndex, commonSpec.FireLoopVerifySize,
          commonSpec.FireLoopVerifyMask, commonSpec.FireLoopVerifyShift) != 0,
        commonData.GetUshort(commonSpec.RfidUsedIndex, commonSpec.RfidUsedSize,
          commonSpec.RfidUsedMask, commonSpec.RfidUsedShift) != 0);

      var loopsSpec = specification.Loops;
      var loopsData = rawData.GetRange(loopsSpec.Index, loopsSpec.Size);
      var loops = new Loop[loopsSpec.MaxCount];
      for (byte loopIndex = 0; loopIndex < loops.Length; loopIndex++) {
        var loopData = loopsData.GetRange(loopsSpec.LoopIndex + loopsSpec.LoopSize * loopIndex,
          loopsSpec.LoopSize);
        loops[loopIndex] = new Loop(loopIndex, $"{loopsSpec.NamePrefix}-{loopIndex + 1}",
          loopData.GetUshort(loopsSpec.ActualIndex, loopsSpec.ActualSize,
            loopsSpec.ActualMask, loopsSpec.ActualShift) != 0,
          loopData.GetUshort(loopsSpec.DelayIndex, loopsSpec.DelaySize,
            loopsSpec.DelayMask, loopsSpec.DelayShift),
          loopData.GetUshort(loopsSpec.VerifiedIndex, loopsSpec.VerifiedSize,
            loopsSpec.VerifiedMask, loopsSpec.VerifiedShift) != 0);
      }

      var outputsSpec = specification.Outputs;
      var outputsData = rawData.GetRange(outputsSpec.Index, outputsSpec.Size);
      var outputs = new Output[outputsSpec.MaxCount];
      for (byte outputIndex = 0; outputIndex < outputs.Length; outputIndex++) {
        var outputData = outputsData.GetRange(outputsSpec.OutputIndex + outputsSpec.OutputSize * outputIndex,
          outputsSpec.OutputSize);
        outputs[outputIndex] = new Output(outputIndex, $"{outputsSpec.NamePrefix}-{outputIndex + 1}",
          outputData.GetUshort(outputsSpec.ActualIndex, outputsSpec.ActualSize,
            outputsSpec.ActualMask, outputsSpec.ActualShift) != 0,
          outputData.GetUshort(outputsSpec.LocalControlIndex, outputsSpec.LocalControlSize,
            outputsSpec.LocalControlMask, outputsSpec.LocalControlShift) != 0,
          outputData.GetUshort(outputsSpec.IsSoundIndex, outputsSpec.IsSoundSize,
            outputsSpec.IsSoundMask, outputsSpec.IsSoundShift) != 0);
      }

      var groupsSpec = specification.Groups;
      var groupsData = rawData.GetRange(groupsSpec.Index, groupsSpec.Size);
      var groups = new Group[groupsSpec.MaxCount];
      var stateData = groupsData.GetRange(groupsSpec.StateIndex, groupsSpec.StateSize);
      for (byte groupIndex = 0; groupIndex < groups.Length; groupIndex++) {
        var actual = stateData.GetUint(groupsSpec.ActualIndex, groupsSpec.ActualSize,
          groupsSpec.ActualMask, groupsSpec.ActualShift * groupIndex) != 0;
        var groupData = groupsData.GetRange(groupsSpec.GroupIndex + groupsSpec.GroupSize * groupIndex,
          groupsSpec.GroupSize);
        var loopStates = new GroupLoopState[loopsSpec.MaxCount];
        for (byte loopStateIndex = 0; loopStateIndex < loopStates.Length; loopStateIndex++)
          loopStates[loopStateIndex] = new GroupLoopState(loopStateIndex,
            groupData.GetUlong(groupsSpec.LoopStateIncludedIndex, groupsSpec.LoopStateIncludedSize,
              groupsSpec.LoopStateIncludedMask, groupsSpec.LoopStateIncludedShift * loopStateIndex) != 0);
        groups[groupIndex] = new Group(groupIndex, $"{groupsSpec.NamePrefix}-{groupIndex + 1}",
          actual, loopStates);
      }

      var scenesSpec = specification.Scenes;
      var scenesData = rawData.GetRange(scenesSpec.Index, scenesSpec.Size);
      var scenes = new Scene[scenesSpec.MaxCount];
      for (var sceneIndex = 0; sceneIndex < scenes.Length; sceneIndex++) {
        var sceneData = scenesData.GetRange(scenesSpec.SceneIndex + scenesSpec.SceneSize * sceneIndex,
          scenesSpec.SceneSize);
        scenes[sceneIndex] = sceneData.All(b => b == 0xFF)
          ? new Scene(scenesSpec.EventTypeDefaultValue, scenesSpec.OutputDefaultValue, scenesSpec.ModeDefaultValue, 
            scenesSpec.DurationDefaultValue, scenesSpec.DelayDefaultValue)
          : new Scene(
            sceneData.GetUlong(scenesSpec.EventTypeIndex, scenesSpec.EventTypeSize, scenesSpec.EventTypeMask,
              scenesSpec.EventTypeShift),
            sceneData.GetUlong(scenesSpec.OutputIndex, scenesSpec.OutputSize, scenesSpec.OutputMask,
              scenesSpec.OutputShift),
            sceneData.GetUlong(scenesSpec.ModeIndex, scenesSpec.ModeSize, scenesSpec.ModeMask,
              scenesSpec.ModeShift),
            sceneData.GetUlong(scenesSpec.DurationIndex, scenesSpec.DurationSize, scenesSpec.DurationMask, 
              scenesSpec.DurationShift),
            sceneData.GetUlong(scenesSpec.DelayIndex, scenesSpec.DelaySize, scenesSpec.DelayMask, 
              scenesSpec.DelayShift));
      }

      return new Configuration(common, loops, outputs, groups, scenes);
    }
  }
}