namespace CharacterInput
{
    public class ScriptableInput : IInput
    {
       public MockInput input;
       public ScriptableInput(MockInput input){
           this.input = input;
       }
        public ScriptableInput(){
          
       }

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
      
    }
}