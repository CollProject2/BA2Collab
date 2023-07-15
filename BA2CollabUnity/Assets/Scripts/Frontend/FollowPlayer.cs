using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public float yOffset = 0.5f;

    
    // for the lights that follow the player
    void Update()
    {
        if (LightManager.instance.canLookatPlayer)
        {
            transform.LookAt(new Vector3(Player.instance.transform.position.x,Player.instance.transform.position.y+yOffset,Player.instance.transform.position.z));

        }
    }
}
