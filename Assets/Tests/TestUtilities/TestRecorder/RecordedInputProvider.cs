using UnityEngine;
namespace CharacterInput
{
    //My custom input service that serves automated testing.
    public class RecordedInputProvider : MonoBehaviour, IInput
    {
        int inputIndex;
        public RecordedTestData input;
        private SingleFrameInputs currentFramesInput;
        private ScreenRecorder screenshot;
        void Start()
        {
            inputIndex = 0;
            currentFramesInput = input.JsonFriendlyInputs[inputIndex];
            screenshot = this.gameObject.GetComponent<ScreenRecorder>();
        }

        public float GetAxis(string axisName)
        {
            switch (axisName)
            {
                case "Horizontal":
                    return currentFramesInput.Horizontal;
                case "Vertical":
                    return currentFramesInput.Vertical;
                case "Mouse Y":
                    return currentFramesInput.MouseY;
                case "Mouse X":
                    return currentFramesInput.MouseX;
                default:
                    return 0f;
            }
        }
        
        void Update()
        {
            if (inputIndex < input.JsonFriendlyInputs.Length)
            {
                if(currentFramesInput.ScreenShotPressed)
                screenshot.TakeScreenshot();
                currentFramesInput = input.JsonFriendlyInputs[inputIndex];
                inputIndex++;
            }
        }
    }
}