using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Playables;

public class storyTypeBase : MonoBehaviour
{
    public float delayTime;
    public GameObject ownCamera;
    public GameObject mainCamera;
    public List<TextAsset> storyContents = new List<TextAsset>();
    public List<PlayableDirector> Directors = new List<PlayableDirector>();
    private void Start()
    {
        ownCameraClose();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }
    public IEnumerator Play()
    {

        yield return new WaitForSeconds(delayTime); //delay the Animation

        GameSystem.instance.changeModeAnimation();
        foreach (PlayableDirector dir in Directors)
        {
            DirectorPlay(dir);
            yield return new WaitUntil(() => isDirectorCompleted(dir)); //結束動畫
        }
        //結束動畫，進入劇情UI環節
        mainCamera.SetActive(true); //開啟主相機
        ownCameraClose(); //關閉副相機
        GameSystem.instance.changeModeStory();

        foreach (TextAsset file in storyContents)
        {

            UISystem.instance.getStoryPanel().GetComponent<storyUI>().setStoryFile(file); //顯示劇情
            //print(file);
            yield return new WaitForSecondsRealtime(2);
            UISystem.instance.getStoryPanel().GetComponent<storyUI>().setSkip(); //顯示skip
            yield return new WaitUntil(() => Input.GetKey(KeyCode.Z)); //結束劇情
            UISystem.instance.getStoryPanel().GetComponent<storyUI>().setInit(); //初始化

        }
        GameSystem.instance.changeModeFollowPlayer();

    }

    protected bool storyContentEmpty() {
        return storyContents.Count == 0;
    }
    protected bool DirectorsEmpty()
    {
        return Directors.Count == 0;
    }
    protected void DirectorPlay(PlayableDirector dir)
    {
        
        mainCamera.SetActive(false);
        dir.Play();

    }
    protected bool isDirectorCompleted(PlayableDirector temp)
    {
        return temp.state != PlayState.Playing;
    }
    protected void ownCameraOpen() {
        ownCamera?.SetActive(true);
    }
    protected void ownCameraClose() {
        ownCamera?.SetActive(false);
    }
}
