using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerAvatar : MonoBehaviour
{ 
    public PermissionLevel avatarsPermission { get; set; }
    void DisplayMessage(string message)
    {
        //This is just an example of a side effect of some interactive functionality that needs to be visually verified by a human being (at least until I make machine learning that can do it for us).
        GameObject.Find("Text").GetComponent<Text>().text = message;

    }
    public bool OnClickActionable(ActionableObject thing)
    {
        var result = thing.OnClick(avatarsPermission);
        if (!result && this.gameObject.activeSelf)
        {            
            var message = $"This avatar with permission {avatarsPermission} is unable to interact with {thing.gameObject.name} given it's {thing.permissionNeeded} minimum required permission.";
            DisplayMessage(message);
        }

        return result;
    }
    
    
}


