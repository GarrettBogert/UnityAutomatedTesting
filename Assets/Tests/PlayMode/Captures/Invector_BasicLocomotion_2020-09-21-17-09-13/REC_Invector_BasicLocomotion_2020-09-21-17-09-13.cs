using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Invector.vCharacterController;
using UnityEngine.SceneManagement;
using CharacterInput;
using System.Linq;


    public class REC_2020_09_21_17_09_13 : AvatarTestsBase
    {
        [UnityTest]
        public IEnumerator RecordedTest()
        {
            var recInputs = new RecordedTestData("Assets/Tests/PlayMode/Captures/Invector_BasicLocomotion_2020-09-21-17-09-13/Invector_BasicLocomotion_2020-09-21-17-09-13.json");
            SceneManager.LoadScene(recInputs.SceneName, LoadSceneMode.Single);
            yield return null;
            var av = GameObject.Find("Avatar") as GameObject;
            var inputs = av.AddComponent<RecordedInputProvider>();
            var rec = av.AddComponent<ScreenRecorder>();
            inputs.input = recInputs;
            var avInput = av.transform.Find("ThirdPersonController_LITE").GetComponent<vThirdPersonInput>();
            avInput.axisInput = inputs;
            yield return new WaitForSeconds(20);
        }
    }
