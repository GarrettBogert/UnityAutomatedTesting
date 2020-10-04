using System.Collections;
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
    public class GraphicalPerformanceTests : AvatarTestsBase
    {
        private IEnumerator testGraphicalPerformance(int avatarCount)
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);
            yield return null;
            var spawnPoints = GameObject.FindGameObjectsWithTag("spawner").Select(o => o.transform.position).ToList();
            for (int i = 0; i < avatarCount; i++)
            {
                var spawnLocation = spawnPoints[Random.Range(0, spawnPoints.Count() - 1)];
                var av = GameObject.Instantiate(Resources.Load("Avatar"), spawnLocation, Quaternion.identity) as GameObject;
                var avInput = av.transform.Find("ThirdPersonController_LITE").GetComponent<vThirdPersonInput>();
                var testInput = av.AddComponent<ScriptableInput>();
                var camObj = av.transform.Find("ThirdPersonCamera_LITE");
                camObj.GetComponent<AudioListener>().enabled = false;
                camObj.GetComponentInChildren<Camera>().enabled = false;
                testInput.isRandomInput = true; //This causes the script to change its inputs every frame smoothly.           
                avInput.axisInput = testInput;
            }

            yield return new WaitForSeconds(10);
            var avgFps = GameObject.Find("Fps counter").GetComponent<FpsTracker>().Average();
            Debug.Log("The average fps for this session was: " + avgFps);
            Assert.IsTrue(avgFps > 30);
        }
        [UnityTest]
        public IEnumerator Fifty_avatars_fps_test()
        {
            yield return testGraphicalPerformance(50);            
        }
        [UnityTest]
        public IEnumerator Hundred_avatars_fps_test()
        {
            yield return testGraphicalPerformance(100);            
        }
        [UnityTest]
        public IEnumerator TwoHundred_avatars_fps_test()
        {
            yield return testGraphicalPerformance(200);
        }
        [UnityTest]
        public IEnumerator FiveHundred_avatars_fps_test()
        {
            yield return testGraphicalPerformance(500);
        }
    }
}
