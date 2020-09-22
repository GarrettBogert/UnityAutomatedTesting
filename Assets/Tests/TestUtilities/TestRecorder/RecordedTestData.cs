using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System;
using UnityEditor;
using System.Collections;
using System.Linq;

[Serializable]
public class RecordedTestData
{
    //This is the input object that gets fed into our custom input provider for 
    //recorded tests. 
    public RecordedTestData(string scene, string scenePath, string time)
    {
        SceneName = scene;
        TimeOfRecord = time;
        Inputs = new List<SingleFrameInputs>();
        ScenePath = scenePath;
    }
    public RecordedTestData(string pathToJsonFile){
        string contents = File.ReadAllText(pathToJsonFile);
        var deserializedObj = JsonUtility.FromJson<RecordedTestData>(contents);
        this.SceneName = deserializedObj.SceneName;
        this.TimeOfRecord = deserializedObj.TimeOfRecord;
        this.Inputs = deserializedObj.JsonFriendlyInputs.ToList();
        this.SetInputs();
    }
    public string SceneName;
     public string ScenePath;
    public string TimeOfRecord;
    private List<SingleFrameInputs> Inputs;

    [SerializeField]
    public SingleFrameInputs[] JsonFriendlyInputs;

    public void CaptureInput(SingleFrameInputs input)
    {
        Inputs.Add(input);
    }
    //TODO: Refactor this so that either A.-There is one single inputs field that
    //is serialize-able, or B.- Get JSON.NET for unity.
    public void SetInputs()
    {
        JsonFriendlyInputs = Inputs.ToArray();
    }
}
