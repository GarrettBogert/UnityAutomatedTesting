using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
    public class ActionableObject : MonoBehaviour
    {
        public string NameOfThisObject;
        public PermissionLevel permissionNeeded;
       
        private void ThingThatHappens()
        {
            //Nothing happens in this demo!! :)
        }
      
        public bool OnClick(PermissionLevel level)
        {
            if (permissionNeeded == level)
            {
                ThingThatHappens();
                return true;
            }
            else
            {
               return false;
            }
        }
    }

