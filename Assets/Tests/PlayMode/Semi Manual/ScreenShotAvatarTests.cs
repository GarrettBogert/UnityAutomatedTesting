﻿using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Invector.vCharacterController;
using UnityEngine.SceneManagement;
using CharacterInput;
using System.Linq;


namespace Tests
{
    //Todo: Set up this fixture to take a source of IInteractables and iterate executions on.
    public class ScreenshotAvatarTests : AvatarTestsBase
    {
        [UnityTest]
        public IEnumerator Verify_object_interaction_with_screenshot()
        {            
            var avatar = GameObject.Find("Avatar") as GameObject;
            var playerAvatar = avatar.GetComponent<PlayerAvatar>();
            avatar.SetActive(true);
            playerAvatar.avatarsPermission = PermissionLevel.Student;
            var screen = GameObject.Instantiate(Resources.Load("Interactables/screen") as GameObject);
            yield return null;//Wait one frame for instantiated GameObject to appear in hierarchy.
            var actionable = screen.GetComponent<ActionableObject>();
            var result = playerAvatar.OnClickActionable(actionable);            
            yield return null;//This allows onGUI to be called so our screenshot includes the GUI cue that says the object clicked didn't match permissions.        
            var recorderObj = GameObject.Find("Recorder");
            recorderObj.AddComponent<ScreenRecorder>();
            var recorderComponent = recorderObj.GetComponent<ScreenRecorder>();
            recorderComponent.TakeScreenshot();
            Assert.Inconclusive("Test ran without error but needs human eyes on screenshot to verify pass/fail.");
        }
    }
}
