using UnityEngine;

public class RotateModels : MonoBehaviour
{
    public float speed;

    // Update is called once per frame
    void Update()
    {
        if (!Player.instance.canMove) return;
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up, speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, -speed * Time.deltaTime);
        }
    }
}
