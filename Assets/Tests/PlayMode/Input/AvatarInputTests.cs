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
            //Creating a bunch of vector2 noise to see if we can cause the player cam to clip through a collider's surface set at z = 5;
            var movementsPerFrame = getRandomVector2s(2500, -12, 12);

            List<float> allCameraZPositions = new List<float>();
            for (int i = 0; i < movementsPerFrame.Count; i++)
            {
                allCameraZPositions.Add(avatarCamera.transform.position.z);
                var input = new MockInput(movementsPerFrame[i], Vector2.zero);
                scriptableInput.input = input;
                yield return null; // Yield return null allows the next frame to render.
            }
            Assert.IsFalse(allCameraZPositions.Where(p => p > 5).Any());
        }

        [UnityTest]
        public IEnumerator Third_person_movement_test()
        {               
            Vector3 lastPosition = avatar.transform.position;

            //TODO: Next refactor, try removing the need to have StartCoroutine() calls at the test level (maybe not possible).
            //The problem here is that StartCoroutine is a method that only lives in monobehaviors, hence choosing testInputService to do .StartCoroutine() on was arbitrary
            //Arbitrary in the sense that, I could have chosen any other monobehavior (eg avatarInput.StartCoroutine) and it would have been fine.

            //Move player forward 
            yield return scriptableInput.StartCoroutine(scriptableInput.MoveDirection(Direction.Forward, 1));
            Assert.IsTrue(avatar.transform.position.z < lastPosition.z);//Avatar is facing negative z, so some of these value comparisons will appear flipped.
            lastPosition = avatar.transform.position;

            //Move player back 
            yield return scriptableInput.StartCoroutine(scriptableInput.MoveDirection(Direction.Backward, 1));
            Assert.IsTrue(avatar.transform.position.z > lastPosition.z);
            lastPosition = avatar.transform.position;

            //Move player left
            yield return scriptableInput.StartCoroutine(scriptableInput.MoveDirection(Direction.Left, 1));
            Assert.IsTrue(avatar.transform.position.x > lastPosition.x);
            lastPosition = avatar.transform.position;

            //Move player right 
            yield return scriptableInput.StartCoroutine(scriptableInput.MoveDirection(Direction.Right, 1));
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
