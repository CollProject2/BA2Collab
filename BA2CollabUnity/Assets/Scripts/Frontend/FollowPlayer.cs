using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public float yOffset = 0.5f;
    
    void Update()
    {
        transform.LookAt(new Vector3(Player.instance.transform.position.x,Player.instance.transform.position.y+yOffset,Player.instance.transform.position.z));
    }
}
