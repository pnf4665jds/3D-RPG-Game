using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPoint : MonoBehaviour
{
    public Shield Shield;

    private void OnDestroy()
    {
        Shield.EnergyPointNum--;
    }
}
