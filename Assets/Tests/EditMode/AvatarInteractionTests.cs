﻿using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using System.Linq;
using System;
using System.IO;
namespace Tests
{
    public class AdminPermissionActionablesProvider : IEnumerable<GameObject>
    {
        public IEnumerator<GameObject> GetEnumerator()
        {
            foreach (var guid in AssetDatabase.FindAssets("", new[] { "Assets/Resources/Interactables" }))
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var root = (GameObject)AssetDatabase.LoadMainAssetAtPath(path);
                if (root.GetComponent<ActionableObject>() && root.GetComponent<ActionableObject>().permissionNeeded == PermissionLevel.Teacher)
                    yield return root;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<GameObject>)this).GetEnumerator();
    }
    //This test fixture will exhuast a standard "student" level permissions avatar 
    //Interacting with every Admin permissioned object and ensure that they cannot
    //Trigger it.
    [TestFixture]
    [TestFixtureSource(typeof(AdminPermissionActionablesProvider))]
    public class AvatarInteractions
    {
        private readonly GameObject prefab;
        private readonly ActionableObject actionable;

        public AvatarInteractions(GameObject interactable)
        {
            prefab = interactable;
            actionable = interactable.GetComponent<ActionableObject>();
        }

        [Test]
        public void Student_privileged_player_cannot_interact_with_teacher_permissioned_clickable()
        {
            var avatar = Resources.Load("avatar") as GameObject;
            var playerAvatar = avatar.GetComponent<PlayerAvatar>();
            avatar.SetActive(true);//Because this is an edit mode test, and UnityEngine's StartCoroutine requires the actual game object to be active.
            playerAvatar.avatarsPermission = PermissionLevel.Student;    
            var result = playerAvatar.OnClickActionable(actionable);      
            Assert.IsFalse(result);                 
        }
    }
}
