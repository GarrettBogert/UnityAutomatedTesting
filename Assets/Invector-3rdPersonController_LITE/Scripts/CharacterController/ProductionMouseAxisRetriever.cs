using UnityEngine;
using CharacterInput;

namespace Invector.vCharacterController
{
    //This is the production input service that just uses unity's Input class. I didn't invert every possible input from the avatar controller (Jump, strafe, and something else). Just these two were okay for my purpose.
    public class ProductionInput : IInput
    {
        public float GetAxisRaw(string axisName)
        {
            return Input.GetAxisRaw(axisName);
        }
        public float GetAxis(string axisName){
            return Input.GetAxis(axisName);
        }
    }
}