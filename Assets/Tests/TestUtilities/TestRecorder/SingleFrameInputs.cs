using System;
//This captures all of the inputs that we currently care about in recorded tests.
//Easy to add to, if need be.
[Serializable]
public class SingleFrameInputs
{
    public float Vertical;
    public float Horizontal;
    public float MouseX;
    public float MouseY;
    //Right now, this is done by the 'L' key, which is a pretty arbitrary binding.
    public bool ScreenShotPressed;
}
