using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimeLineManager : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayableDirector Director;
    [SerializeField]
    private bool entered;
    private void Start()
    {
        Director = GetComponent<PlayableDirector>();
        entered = false;
    }
    

    public void TimeLinePlay() {
        if (!entered)
        {
            Director.Play();
            entered = true;
        }
       
    }
    public bool isTimeLineCompleted() {
        return Director.state != PlayState.Playing;
    }
}
