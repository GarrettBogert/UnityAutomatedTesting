using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    public class PlayerAvatar : MonoBehaviour
    {
        public PermissionLevel avatarsPermission { get; set; }

        public void OnClickActionable(ActionableObject thing)
        {
            thing.OnClick(avatarsPermission);
        }
    }


