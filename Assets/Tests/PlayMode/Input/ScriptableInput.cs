using UnityEngine;
using System.Collections;
namespace CharacterInput
{
    public enum Direction { Forward, Backward, Left, Right };
    //My custom input service that serves automated testing.

    //The setting up of this should be a matter of getting a reference to the avatar gameobject, adding a scriptableInput component to it, 
    //and then setting the avatar controller classes IInput dependency to the scriptableInput component. Then everything will work.
    public class ScriptableInput : MonoBehaviour, IInput
    {
        //This field represents the current input being fed to the avatar.
        public MockInput input = new MockInput(Vector2.zero, Vector2.zero);

        //Setting this to true makes avatar wander around aimlessly.
        //TODO: Move this into a separate RandomInput implementation, especially if this ScriptableInput grows much more.
        public bool isRandomInput = false;

        private MockInput getInput(Direction direction)
        {
            switch (direction)
            {
                case Direction.Forward:
                    return new MockInput(Vector2.zero, Vector2.up);
                case Direction.Backward:
                    return new MockInput(Vector2.zero, Vector2.down);
                case Direction.Left:
                    return new MockInput(Vector2.zero, Vector2.left);
                case Direction.Right:
                    return new MockInput(Vector2.zero, Vector2.right);
                default: return new MockInput(Vector2.zero, Vector2.zero);
            }
        }
        private IEnumerator moveDirection(Direction direction, int duration)
        {
            this.input = getInput(direction);
            yield return new WaitForSeconds(duration);
            this.input = new MockInput(Vector2.zero, Vector2.zero);
        }
        public IEnumerator MoveDirection(Direction direction, int duration)
        {
            yield return StartCoroutine(moveDirection(direction,duration));
        }
        public float GetAxis(string axisName)
        {
            switch (axisName)
            {
                case "Horizontal":
                    return input.Horizontal;
                case "Vertical":
                    return input.Vertical;
                case "Mouse Y":
                    return input.MouseY;
                case "Mouse X":
                    return input.MouseX;

                default:
                    return 0f;
            }
        }
        //This makes the avatar inputs simulate a chicken with it's head cut off. Fun!
        void Update()
        {
            if (isRandomInput)
            {
                var xModifier = Random.Range(-1f, 1f);
                var yModifier = Random.Range(-1f, 1f);
                input.Horizontal = input.Horizontal + xModifier;
                input.Vertical = input.Vertical + yModifier;
            }
        }

    }
}