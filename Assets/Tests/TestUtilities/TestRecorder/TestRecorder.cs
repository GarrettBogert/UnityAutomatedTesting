using System.Collections;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System;
using UnityEditor;
public class TestRecorder : MonoBehaviour
{
    
    RecordedTestData test;

    //I decided for now, it was most simple to represent this list of frame-by-frame inputs as a json document
    void Start()
    {
        test = new RecordedTestData(SceneManager.GetActiveScene().name,SceneManager.GetActiveScene().path, DateTime.UtcNow.ToString("yyyy-MM-dd-HH-mm-ss"));
    }

    void OnDisable()
    {
        CaptureRecording();
    }

    void CaptureRecording()
    {
        string fileName = $"{test.SceneName}_{test.TimeOfRecord}";
        test.SetInputs();

        var testFolderPath = $"Assets/Tests/PlayMode/Captures/{fileName}";
        Directory.CreateDirectory(testFolderPath);

        //Write the JSON recording here.
        var jsonFilePath = testFolderPath + $"/{fileName}.json";
        using (var sw = new StreamWriter(jsonFilePath))
        {
            var serialized = JsonUtility.ToJson(test);
            sw.Write(JsonUtility.ToJson(test));
        }
        //Write the test C# code
       using (var sw = new StreamWriter(testFolderPath + $"/REC_{fileName}.cs"))
        {     
            var csharpClassFriendlyName = "REC_" + test.TimeOfRecord.Replace('-','_');
            sw.Write(cSharpTemplate(jsonFilePath,csharpClassFriendlyName));
        }        
    }

    SingleFrameInputs getInput()
    {
        var input = new SingleFrameInputs
        {
            Vertical = Input.GetAxis("Vertical"),
            Horizontal = Input.GetAxis("Horizontal"),
            MouseX = Input.GetAxis("Mouse X"),
            MouseY = Input.GetAxis("Mouse Y"),
            ScreenShotPressed = Input.GetKeyDown(KeyCode.L)
            
        };
        return input;
    }

    // Update is called once per frame
    void Update()
    {
        test.CaptureInput(getInput());
        if (Input.GetKeyDown(KeyCode.P))//Arbitrarily assigned P to stopping the test record 
            CaptureRecording();
    }


    string cSharpTemplate(string pathToJson, string testClass){
        using(StreamReader sr = new StreamReader(@"Assets/Tests/TestUtilities/TestRecorder/CSharpTestTemplate.txt")){
            return sr.ReadToEnd().Replace("PATHTOJSON",pathToJson).Replace("TESTCLASS",testClass);
        }
    }
}
