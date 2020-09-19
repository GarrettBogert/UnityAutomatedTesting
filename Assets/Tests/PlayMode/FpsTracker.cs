using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FpsTracker : MonoBehaviour
{
    List<int> allFps = new List<int>();
    public int Average(){
        return (int)allFps.Average();
    }
    void Update()
    {
        if(allFps.Count >1000)
        allFps = new List<int>{Average()};//To avoid an overflow, I average every 1000 records stored and replace the 1000 with a single averaged fps.
        var current = (int)(1f / Time.unscaledDeltaTime);
        allFps.Add(current);
    }
}
