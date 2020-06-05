using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class storyTriggerType : storyTypeBase
{
    public string TriggerTag;
    public Vector3 TriggerSize;
    public Vector3 triggerCenterPos;
    public string getTriggerTag()
    {
        return TriggerTag;
    }
    public bool enterTheTrigger()
    {
        Collider[] enterZone = Physics.OverlapBox(triggerCenterPos, TriggerSize, transform.rotation, LayerMask.GetMask("Player"));
        if (enterZone.Length > 0)
        {
            return true;
        }
        else {
            return false;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(triggerCenterPos , TriggerSize);
    }

}
