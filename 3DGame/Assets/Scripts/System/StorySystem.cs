using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
public class StorySystem : Singleton<StorySystem>
{
    private bool StoryShow = false;
    private GameObject player;
    [SerializeField]
    private PlayableDirector DirectorEnterBossZone;
    [SerializeField]
    private PlayableDirector DirectorExistBossZone;
    [SerializeField]

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(StoryFlowControl());
    }

    // Update is called once per frame
    public IEnumerator StoryFlowControl() {
        for (int i = 1; i <= SceneManager.sceneCount; i++) {
            yield return new WaitUntil(() => GameSceneManager.instance.IsLoadingFinish);

            yield return new WaitUntil(() => findObjectInScene());

            GameSystem.instance.changeModeStory();
            UISystem.instance.getStoryPanel().GetComponent<storyUI>().setloadingFinshFile(i); //設定劇情

            yield return new WaitForSecondsRealtime(2);
            yield return new WaitUntil(() => Input.GetKey(KeyCode.Z)); //關閉劇情

            GameSystem.instance.changeModeFollowPlayer();

            yield return new WaitUntil(() => player.GetComponent<Player>().GetisInBoss()); //進入boss場景
            yield return new WaitForSeconds(1);

            GameSystem.instance.changeModeAnimation();
            TimeLinePlay(DirectorEnterBossZone);

            yield return new WaitUntil(() => isTimeLineCompleted(DirectorEnterBossZone)); //結束進入boss場景的動畫

            GameSystem.instance.changeModeFollowPlayer();

            yield return new WaitUntil(() => MonsterSystem.instance.IsBossDead);

            GameSystem.instance.changeModeAnimation();
            TimeLinePlay(DirectorExistBossZone);

            yield return new WaitUntil(() => isTimeLineCompleted(DirectorExistBossZone)); //結束boss死亡後的動畫

            GameSystem.instance.changeModeStory();
            UISystem.instance.getStoryPanel().GetComponent<storyUI>().setbossDeadFile(i);

            yield return new WaitForSecondsRealtime(2);

            yield return new WaitUntil(() => Input.GetKey(KeyCode.Z)); //關閉劇情

            GameSystem.instance.changeModeFollowPlayer();
        }
    }

    public void TimeLinePlay(PlayableDirector temp)
    {
        temp.Play();

    }
    public bool isTimeLineCompleted(PlayableDirector temp)
    {
        return temp.state != PlayState.Playing;
    }
    public bool findObjectInScene()
    {

        DirectorEnterBossZone = GameObject.FindGameObjectWithTag("EnterBossFieldAnim").GetComponent<PlayableDirector>();
        DirectorExistBossZone = GameObject.FindGameObjectWithTag("BossDeadAnim").GetComponent<PlayableDirector>();

        return true;

    }
}
