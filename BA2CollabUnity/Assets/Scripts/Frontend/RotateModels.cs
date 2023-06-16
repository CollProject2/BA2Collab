using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateModels : MonoBehaviour
{
    public float speed;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up , speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, -speed * Time.deltaTime);
        }
    }
}
