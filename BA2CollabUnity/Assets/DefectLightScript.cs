using System.Collections;
using UnityEngine;

public class DefectLightScript : MonoBehaviour
{
    public GameObject lightObject; // Reference to the light
    public float minFastInterval = 0.05f;
    public float maxFastInterval = 0.2f;
    public float minSlowInterval = 2f;
    public float maxSlowInterval = 5f;
    public int minFastBlinks = 2; // minimum fast blinks
    public int maxFastBlinks = 7; // maximum fast blinks

    // Start is called before the first frame update
    void Start()
    {
        // Start the Coroutine
        StartCoroutine(FlashingLight());
    }

    IEnumerator FlashingLight()
    {
        while (true)
        {
            // Fast blinking part
            int fastBlinks = Random.Range(minFastBlinks, maxFastBlinks + 1);
            for (int i = 0; i < fastBlinks; i++)
            {
                lightObject.SetActive(!lightObject.activeSelf);
                yield return new WaitForSeconds(Random.Range(minFastInterval, maxFastInterval));
            }

            // Stable part
            lightObject.SetActive(true);
            yield return new WaitForSeconds(Random.Range(minSlowInterval, maxSlowInterval));
        }
    }
}
