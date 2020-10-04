using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.TestTools;
using UnityEngine.TestTools;

[assembly: TestPlayerBuildModifier(typeof(HeadlessPlayModeSetup))]
[assembly: PostBuildCleanup(typeof(HeadlessPlayModeSetup))]
public class HeadlessPlayModeSetup : ITestPlayerBuildModifier, IPostBuildCleanup
{
    private static bool s_RunningPlayerTests;
    public BuildPlayerOptions ModifyOptions(BuildPlayerOptions playerOptions)
    {
        // Do not launch the player after the build completes.
        playerOptions.options &= ~BuildOptions.AutoRunPlayer;

        var headlessBuildLocation = Path.GetFullPath(@"C:\Users\Garrett\Desktop\Tests");
     
        playerOptions.locationPathName = headlessBuildLocation;

        // Instruct the cleanup to exit the Editor if the run came from the command line. 
        // The variable is static because the cleanup is being invoked in a new instance of the class.
        s_RunningPlayerTests = true;
        return playerOptions;
    }

    public void Cleanup()
    {
        if (s_RunningPlayerTests && IsRunningTestsFromCommandLine())
        {
            // Exit the Editor on the next update, allowing for other PostBuildCleanup steps to run.
            EditorApplication.update += () => { EditorApplication.Exit(0); };
        }
    }

    private static bool IsRunningTestsFromCommandLine()
    {
        var commandLineArgs = Environment.GetCommandLineArgs();
        return commandLineArgs.Any(value => value == "-runTests");
    }
}
