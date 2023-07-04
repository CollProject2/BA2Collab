using UnityEngine;

public class WordDetector : MonoBehaviour
{
    public int id;
    public Transform endPosition;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TextBlock"))
        {
            if (other.gameObject.GetComponent<TextBlock>().id == id)
            {
                other.gameObject.GetComponent<TextBlock>().solved = true;
                other.gameObject.GetComponent<TextBlock>().SetPosition(endPosition);
                other.gameObject.GetComponent<TextBlock>().isInteractable = false;
                TextBoxManager.instance.CallCheck();
                Destroy(this);
            }
        }
    }
}
