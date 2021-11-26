using PsmsConfigurator.Shared.Models;
using PsmsConfigurator.WpfApp.Models;

namespace PsmsConfigurator.WpfApp.Factories {
  public class ConfigurationFactory {
    public Configuration Create(Specification spec) {
      var commonSpec = spec.Common;
      var common = new Common(commonSpec.LedOnNormalLightDefaultValue,
        commonSpec.FireLoopVerifyDefaultValue, commonSpec.RfidUsedDefaultValue);

      var loopSpec = spec.Loops;
      var loops = new Loop[loopSpec.MaxCount];
      for (byte loopIndex = 0; loopIndex < loops.Length; loopIndex++)
        loops[loopIndex] = new Loop(loopIndex, $"{loopSpec.NamePrefix}-{loopIndex+1}",
          loopIndex < spec.Loops.DefaultCount, loopSpec.DelayDefaultValue, loopSpec.VerifiedDefaultValue);

      var outputSpec = spec.Outputs;
      var outputs = new Output[outputSpec.MaxCount];
      for (byte outputIndex = 0; outputIndex < outputs.Length; outputIndex++)
        outputs[outputIndex] = new Output(outputIndex, $"{outputSpec.NamePrefix}-{outputIndex+1}",
          outputIndex < outputSpec.DefaultCount, outputSpec.LocalControlDefaultValue, outputSpec.IsSoundDefaultValue);

      var groupsSpec = spec.Groups;
      var groups = new Group[groupsSpec.MaxCount];
      for (byte groupIndex = 0; groupIndex < groups.Length; groupIndex++) {
        var loopStates = new GroupLoopState[loops.Length];
        for (byte loopStateIndex = 0; loopStateIndex < loopStates.Length; loopStateIndex++)
          loopStates[loopStateIndex] = new GroupLoopState(loopStateIndex,
            groupsSpec.LoopStateIncludedDefaultValue);
        groups[groupIndex] = new Group(groupIndex, $"{groupsSpec.NamePrefix}-{groupIndex+1}",
          groupIndex < groupsSpec.DefaultCount, loopStates);
      }

      var scenesSpec = spec.Scenes;
      var scenes = new Scene[scenesSpec.MaxCount];
      for (var sceneIndex = 0; sceneIndex < scenes.Length; sceneIndex++)
        scenes[sceneIndex] = new Scene(scenesSpec.EventTypeDefaultValue, scenesSpec.OutputDefaultValue,
          scenesSpec.ModeDefaultValue, scenesSpec.DurationDefaultValue,
          scenesSpec.DelayDefaultValue);

      return new Configuration(common, loops, outputs, groups, scenes);
    }
  }
}