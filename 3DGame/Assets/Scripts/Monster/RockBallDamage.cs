using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBallDamage : MonoBehaviour
{
    public Transform Rock;
    public void createRock() {
        Vector3 newPos = new Vector3(this.transform.position.x, this.transform.position.y + 3, this.transform.position.z);
        var rock = Instantiate(Rock, newPos, Quaternion.identity);
        rock.transform.rotation = this.transform.rotation;
    }
}
