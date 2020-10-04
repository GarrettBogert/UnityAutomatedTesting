using UnityEngine;
using System.IO;
using UnityEditor;
using System.Collections;
using System.Linq;
using UnityEditor.TestTools;

[assembly:TestPlayerBuildModifier(typeof(BuildModifier))]
public class BuildModifier : ITestPlayerBuildModifier
{    
   public BuildPlayerOptions ModifyOptions(BuildPlayerOptions playerOptions){
       var avatarTestsBase = new AvatarTestsBase();
       if(!playerOptions.scenes.Contains(new AvatarTestsBase().MovementTestsScene)){
           playerOptions.scenes = playerOptions.scenes.Append(avatarTestsBase.MovementTestsScene).ToArray();
       }
       return playerOptions;
   }
}