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
    public void SetInputs()
    {
        JsonFriendlyInputs = Inputs.ToArray();
    }
}
