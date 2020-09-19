using UnityEngine;
namespace CharacterInput
{
    //My custom input service that serves automated testing.
    public class ScriptableInput : MonoBehaviour, IInput
    {
       public bool isRandomInput = false;
       //These fields are all public so if I ever want to write a test script that is accurate to the rendered frame, I
       //Can jump in and modify these values in between yield return null statements. 
       //If I made this class more robust, I could probably write a test "recorder tool" where I can accurately record inputs down to the frame, and replay them 100 percent indentically for a regression test script, or something. Hmmmmm
       public MockInput input = new MockInput(Vector2.zero,Vector2.zero);
      
       public float GetAxis(string axisName){
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
       void Update(){
           if(isRandomInput){
               var xModifier = Random.Range(-1f,1f);
               var yModifier = Random.Range(-1f,1f);
               input.Horizontal = input.Horizontal + xModifier;
               input.Vertical = input.Vertical + yModifier;
           }
       }
      
    }
}