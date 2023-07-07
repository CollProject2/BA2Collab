using UnityEngine;

public class Leaf : MonoBehaviour
{
    public bool removed = false;
    private Vector3 screenPoint;
    private Vector3 offset;
    private Rigidbody rb;
    public float rotationLimit = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Limit rotation speed
        if (rb.angularVelocity.magnitude > rotationLimit)
            rb.angularVelocity = rb.angularVelocity.normalized * rotationLimit;
    }

    void OnMouseDown()
    {
        if (!PlantManager.instance.isIntaractable) return;
        // Translate the GameObject's position to the screen's point
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        // Calculate the offset
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag()
    {
        if (!PlantManager.instance.isIntaractable) return;
        // Every frame that the mouse button is held down over the object, adjust its position to follow the mouse cursor
        Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
        gameObject.transform.position = cursorPosition;
    }

    void OnMouseUp()
    {
        if (!PlantManager.instance.isIntaractable) return;
        // When the mouse button is released, trigger the leaf's falling physics
        GetComponent<Rigidbody>().isKinematic = false;
        removed = true;
        PlantManager.instance.CallCheck();
        Destroy(this);
    }

}
