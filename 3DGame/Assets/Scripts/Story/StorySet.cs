using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class StorySet : MonoBehaviour
{
    // Start is called before the first frame update
   
    void Start()
    {
        getTheOpenningFile(1);
    }

    public string getTheOpenningFile(int index) {
        string filePath = "Assets/Resources/story/0" + index + ".txt";
        StreamReader reader = new StreamReader(filePath);
        Debug.Log(reader.ReadToEnd());
        return reader.ReadToEnd();
    }
}
