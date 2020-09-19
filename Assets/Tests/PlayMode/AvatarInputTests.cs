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
    public class AvatarTests : AvatarTestsBase
    {
        [UnityTest]
        public IEnumerator Third_person_camera_doesnt_phase_through_walls()
        {
            SceneManager.LoadScene(this.TestScenePath, LoadSceneMode.Single);
            yield return null;
            var avatar = GameObject.Find("ThirdPersonController_LITE");
            var avatarCamera = GameObject.FindGameObjectWithTag("MainCamera");
            //Creating a bunch of vector2 noise to see if we can cause the player cam to clip through a collider's surface set at z = 5;
            var movementsPerFrame = getRandomVector2s(2500, -12, 12);

            var inputService = new ScriptableInput();
            avatar.GetComponent<vThirdPersonInput>().axisInput = inputService;

            List<float> allCameraZPositions = new List<float>();
            for (int i = 0; i < movementsPerFrame.Count; i++)
            {
                allCameraZPositions.Add(avatarCamera.transform.position.z);
                var input = new MockInput(movementsPerFrame[i], Vector2.zero);
                inputService.input = input;
                yield return null; // Yield return null allows the next frame to render.
            }
            Assert.IsFalse(allCameraZPositions.Where(p => p > 5).Any());
        }

        [UnityTest]
        public IEnumerator Third_person_movement_test()
        {
            SceneManager.LoadScene(this.TestScenePath, LoadSceneMode.Single);
            yield return null;
            var avatar = GameObject.Find("ThirdPersonController_LITE");
            var inputService = avatar.AddComponent<ScriptableInput>();
            avatar.GetComponent<vThirdPersonInput>().axisInput = inputService;
            Vector3 lastPosition = avatar.transform.position;

            //Move player forward 
            var input = new MockInput(Vector2.zero, Vector2.up);
            inputService.input = input;
            yield return new WaitForSeconds(1);
            Assert.IsTrue(avatar.transform.position.z < lastPosition.z);//Avatar is facing negative z, so some of these value comparisons will appear flipped.
            lastPosition = avatar.transform.position;

            //Move player back 
            input = new MockInput(Vector2.zero, Vector2.down);
            inputService.input = input;
            yield return new WaitForSeconds(1);
            Assert.IsTrue(avatar.transform.position.z > lastPosition.z);
            lastPosition = avatar.transform.position;

            //Move player left
            input = new MockInput(Vector2.zero, Vector2.left);
            inputService.input = input;
            yield return new WaitForSeconds(1);
            Assert.IsTrue(avatar.transform.position.x > lastPosition.x);
            lastPosition = avatar.transform.position;
            
            //Move player right 
            input = new MockInput(Vector2.zero, Vector2.right);
            inputService.input = input;
            yield return new WaitForSeconds(1);
            Assert.IsTrue(avatar.transform.position.x < lastPosition.x);       
        }

        List<Vector2> getRandomVector2s(int count, int min, int max)
        {
            List<Vector2> vectors = new List<Vector2>();
            for (int i = 0; i < count; i++)
            {
                vectors.Add(new Vector2(Random.Range(min, max), Random.Range(min, max)));
            }
            return vectors;
        }
    }
}
