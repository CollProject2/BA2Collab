using UnityEngine;

public class WordDetector : MonoBehaviour
{
    public int id;
    public Transform endPosition;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TextBlock"))
        {
            Debug.Log(other.gameObject.GetComponent<TextBlock>().id);
            Debug.Log(id);
            if (other.gameObject.GetComponent<TextBlock>().id == id)
            {
                other.gameObject.GetComponent<TextBlock>().solved = true;
                other.gameObject.GetComponent<MeshRenderer>().enabled=false;
                TextBoxManager.instance.CallCheck();
                Destroy(gameObject);
            }
        }
    }
}
