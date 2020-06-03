using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimeLineManager : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayableDirector DirectorEnterBossZone;
    public PlayableDirector DirectorExistBossZone;
    private GameObject player;
    [SerializeField]
    private bool entered;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(ActionStart());

    }
    

    public void TimeLinePlay(PlayableDirector temp) {
        if (!entered)
        {
            temp.Play();
            entered = true;
        }
       
    }
    private IEnumerator ActionStart() {
        entered = false;
        yield return new WaitUntil(() => player.GetComponent<Player>().GetisInBoss());
        GameSystem.instance.changeModeAnimation();
        TimeLinePlay(DirectorEnterBossZone);
        yield return new WaitUntil(() => isTimeLineCompleted(DirectorEnterBossZone));
        GameSystem.instance.changeModeFollowPlayer();
        entered = false;
        yield return new WaitUntil(() => MonsterSystem.instance.IsBossDead);
        GameSystem.instance.changeModeAnimation();
        TimeLinePlay(DirectorExistBossZone);
        yield return new WaitUntil(() => isTimeLineCompleted(DirectorExistBossZone));
        GameSystem.instance.changeModeFollowPlayer();
    }

    public bool isTimeLineCompleted(PlayableDirector temp) {
        return temp.state != PlayState.Playing;
    }
}
