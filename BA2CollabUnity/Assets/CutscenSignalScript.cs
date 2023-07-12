using UnityEngine;

public class CutscenSignalScript : MonoBehaviour
{
    public void OnSignal()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.yoursEverMusic, Vector3.zero);
    }

    public void endGame()
    {
        Application.Quit();
    }
}
