using UnityEngine;
using System.Collections;
using CharacterInput;
using UnityEngine.SceneManagement;
using Invector.vCharacterController;
using UnityEngine.TestTools;

public class AvatarTestsBase
{
    //The avatar we will control in our tests.
    public GameObject avatar;
    //The avatars input service.
    public vThirdPersonInput avatarInput;
    //The avatar's camera we will control in our tests.
    public GameObject avatarCamera;
    //Our scriptable input reference that we can modify, and cause character to move.
    public ScriptableInput scriptableInput;
    public Canvas guiCanvas = new Canvas();
    public string MovementTestsScene = @"Test3rdPersonCameraNotClippingThroughWalls";
    public string LargeScenePath = @"Invector_BasicLocomotion";

    //This attribute allows our SetUp method to be an IEnumerator, which NUnit's [test] attribute doesn't support.
    [UnitySetUp]
    public IEnumerator InitializeScene()
    {
        SceneManager.LoadScene(this.MovementTestsScene, LoadSceneMode.Single);
        yield return null;
        avatar = GameObject.Find("ThirdPersonController_LITE");    
        avatarCamera = GameObject.FindGameObjectWithTag("MainCamera");
        scriptableInput = avatar.AddComponent<ScriptableInput>();
        avatarInput = avatar.GetComponent<vThirdPersonInput>();
        avatarInput.axisInput = scriptableInput;
        yield return null;
    }
}
