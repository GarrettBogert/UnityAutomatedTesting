using UnityEngine;
using CharacterInput;

namespace Invector.vCharacterController
{
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