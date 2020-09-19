using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
    public class ActionableObject : MonoBehaviour
    {
        public string NameOfThisObject;
        public PermissionLevel permissionNeeded;
        //Thing that happens is the side effect of a properly permissioned player click that we can verify.
        private void ThingThatHappens()
        {
            //Debug.Log(NameOfThisObject + " has been activated.");
        }
        // Start is called before the first frame update
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

