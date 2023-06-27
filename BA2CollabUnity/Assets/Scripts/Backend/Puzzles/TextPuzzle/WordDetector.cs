using UnityEngine;

public class WordDetector : MonoBehaviour
{
    public int id;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TextBlock"))
        {
            other.transform.position = transform.position;
            if (other.gameObject.GetComponent<TextBlock>().id == id)
            {
                other.gameObject.GetComponent<TextBlock>().solved = true;
                other.gameObject.GetComponent<TextBlock>().isInteractable = false;
                Destroy(this);
                TextBoxManager.instance.CallCheck();
            }

        }
    }
}
