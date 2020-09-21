using UnityEngine;
using System.IO;
using System.Collections;

//Messy base class, but it served its purpose for creating a recorded demo.
public class AvatarTestsBase
{
    public Canvas guiCanvas = new Canvas();
    public string TestScenePath = @"Assets/Scenes/Test3rdPersonCameraNotClippingThroughWalls.unity";
    public string LargeScenePath = @"Assets/Invector_3rdPersonController/Invector_BasicLocomotion";
}
