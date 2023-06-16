using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuUI : MonoBehaviour
{
    // the UI panel appears instantly now, in the future we will have a sequence
    // zoom out, close curtains, open related buttons....

    //Properties
    public bool isDisplayed { get; private set; }
    [SerializeField] private GameObject pauseMenuPanel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isDisplayed)
            {
                DisplayPauseMenuUI();
            }
            else
            {
                HidePauseMenuUI();
            }
        }
    }

    private void Start()
    {
        HidePauseMenuUI();
    }

    //Hide, Resume Game
    private void HidePauseMenuUI()
    {
        Player.instance.SetCanMove(true);
        // run player or set time scale to 1
        pauseMenuPanel.SetActive(false);
        isDisplayed = false;
    }

    //Display, Pause Game
    private void DisplayPauseMenuUI()
    {
        // stop player or set time scale to 0
        Player.instance.SetCanMove(false);
        pauseMenuPanel.SetActive(true);
        isDisplayed = true;
    }

    // exit game
    public void ExitGameButton()
    {
        Application.Quit();
    }

    public void MainMenuButton()
    {
        //load scene ? kinda works like restart game
        SceneManager.LoadScene(0);
    }
}
