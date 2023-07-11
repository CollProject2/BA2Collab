using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFollowerLight : MonoBehaviour
{
    public GameObject target;
    void Update()
    {
        gameObject.transform.LookAt(target.transform);
    }
}
