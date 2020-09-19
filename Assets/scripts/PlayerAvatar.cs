using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    public class PlayerAvatar : MonoBehaviour
    {
        public PermissionLevel avatarsPermission { get; set; }

        public bool OnClickActionable(ActionableObject thing)
        {
            return thing.OnClick(avatarsPermission);
        }
    }


