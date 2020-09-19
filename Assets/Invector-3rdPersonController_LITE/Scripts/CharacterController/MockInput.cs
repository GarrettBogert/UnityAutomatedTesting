using UnityEngine;
namespace CharacterInput
{
    public class MockInput
    {
        public float MouseY { get; set; }
        public float MouseX { get; set; }
        public float Horizontal { get; set; }
        public float Vertical { get; set; }
        public MockInput(float mouseY, float mouseX, float x, float y)
        {
            MouseY = mouseY;
            MouseX = mouseX;
            Horizontal = x;
            Vertical = y;
        }
        public MockInput(Vector2 mouse, Vector2 axis){
            MouseY = mouse.y;
            MouseX = mouse.x;
            Horizontal = axis.x;
            Vertical = axis.y;
        }
    }
}