using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class storyCircleTriggerType : storyTypeBase
{
    public string TriggerTag;
    public float TriggerSize;
    public Vector3 triggerCenterPos;


    private void Start()
    {
        ownCameraClose();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        StartCoroutine(EmergencyStart(TriggerTag));
    }

    private IEnumerator EmergencyStart(string tag)
    {
        yield return new WaitUntil(() => enterTheTrigger(tag));
        StartCoroutine(Play());

    }
    public string getTriggerTag()
    {
        return TriggerTag;
    }
    public bool enterTheTrigger(string tag)
    {
        Collider[] enterZone = Physics.OverlapSphere(triggerCenterPos, TriggerSize, LayerMask.GetMask(tag));
        if (enterZone.Length > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(triggerCenterPos, TriggerSize);
    }
}
