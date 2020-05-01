using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showStory : MonoBehaviour
{
    public TextAsset TxtFile;
    private string story;
    // Start is called before the first frame update
    void Start()
    {
        story = TxtFile.text;
        Debug.Log(story);
    }

    public string getStory() {
        return story;
    }

}
