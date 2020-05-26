using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimeLineManager : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayableDirector Director;
    private bool entered;
    private void Start()
    {
        Director = GetComponent<PlayableDirector>();
        entered = false;
    }
    /*private void Update()
    {
        print(isTimeLineCompleted());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            TimeLinePlay();
        }
    }*/
    public void TimeLinePlay() {
        Director.Play();
    }
    public bool isTimeLineCompleted() {
        return Director.state != PlayState.Playing;
    }
}
