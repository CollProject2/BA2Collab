using UnityEngine;

public class TextBlock : MonoBehaviour
{
    public int id;
    public bool solved;
    private bool isDragging = false;
    public Transform startPosition;
    private Vector3 offset;

    private void Awake()
    {
        solved = false;
    }

    private void OnMouseDown()
    {
        //prevent clicking before the puzzle is loaded
        if (!TextBoxManager.instance.isInteractable) return;

        //set the new Letter as the current letter
        TextBoxManager.instance.SetCurrentTextBlock(this);
        isDragging = true;
        Vector3 mousePos = Input.mousePosition;
        // Set z to the distance between the camera and your UI plane
        mousePos.z = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);

        offset = gameObject.transform.position - mouseWorldPos;
    }

    void OnMouseUp()
    {
        isDragging = false;

        // Check if the block is placed;
        if (!solved)
            // Return the block to its starting position.
            transform.position = startPosition.position;
        
    }

    public void SetPosition(Transform pos)
    {
        transform.position = pos.position;
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mousePos = Input.mousePosition;
            // Set z to the distance between the camera and your UI plane
            mousePos.z = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);

            Vector3 targetPos = mouseWorldPos + offset;
            if(!solved)
                gameObject.transform.position = targetPos;
        }
    }

}
