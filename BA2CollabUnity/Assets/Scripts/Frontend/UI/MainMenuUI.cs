using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    // the UI panel appears instantly now, in the future we will have a sequence
    // zoom in, open curtains, open related buttons....

    //Properties
    public bool isDisplayed { get; private set; }
    public bool journalIsOpen;

    [Header("Objects")]
    public GameObject mainMenuPanel;
    [SerializeField] private GameObject title;
    [SerializeField] private GameObject flower;
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject curtainL;
    [SerializeField] private GameObject curtainR;
    [SerializeField] private GameObject mandala;
    [SerializeField] private GameObject journalScreen;
    [SerializeField] private GameObject pauseScreen;

    [Header("Positions")]

    [SerializeField] private Transform titleActivePos;
    [SerializeField] private Transform pauseActivePos;
    [SerializeField] private Transform pauseInitPos;
    [SerializeField] private Transform flowerActivePos;
    [SerializeField] private Transform TEMPUIAwayPos;

    [Header("Durations")]
    
    [SerializeField] private float titleMoveDuration = 4;
    [SerializeField] private float flowerMoveDuration = 3;
    [SerializeField] private float startButtonMoveDuration = 2;
    [SerializeField] private float curtainOpenDuration = 2;

    [Header("Scale")]
    [SerializeField] private float targetScaleX;

    [Header("Curves")]
    [SerializeField] private AnimationCurve titleMoveCurve;
    [SerializeField] private AnimationCurve menuAwayCurve;
    [SerializeField] private AnimationCurve buttonCurve;

    [Header("Values")]
    public float mandalaTurnSpeed = 15f;


    
    public float offSetY = 2;
    public float offSetZ = 2;
    public float offSetX = 2;

    
    public bool canPause = false;
    private bool canStart = false;

    private void Start()
    {
        // starts without input
        MainMenuSequence();
        journalIsOpen = false;
    }

    private void Update()
    {
        TurnMandala();
        OpenJournalScreen();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenuSequence();
        }
    }

    void TurnMandala()
    {
        mandala.transform.Rotate(Vector3.forward, mandalaTurnSpeed * Time.deltaTime);
    }

    

    // the sequence of events when we start the game and re load the scene
    void MainMenuSequence()
    {
        FadeToTransparent(4);
        MoveTitleAndFlowerDown();
    }

    public void PauseMenuSequence()
    {
        if (journalIsOpen) return;
        if (canPause)
        {
            canPause = false;
            Player.instance.SetCanMove(false);
            LightManager.instance.TurnOffPlayerLights();
            pauseScreen.transform.DOMove(pauseActivePos.position, 2).SetEase(buttonCurve);
        }
        else
        {
            canPause = true;
            Player.instance.SetCanMove(false);
            LightManager.instance.TurnOnPlayerLights();
            pauseScreen.transform.DOMove(pauseInitPos.position, 2).SetEase(buttonCurve);
        }
        
    }
    public void OpenJournalScreen()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (canPause && !journalIsOpen)
            {
                canPause = false;
                journalIsOpen = true;
                Player.instance.SetCanMove(false);
                journalScreen.transform.DOMove(UIManager.instance.puzzleUI.blockPuzzleActivePos.position, 2);
                LightManager.instance.TurnOffPlayerLights();
            }
            else if (!canPause && journalIsOpen)
            {
                canPause = true;
                journalIsOpen = false;
                Player.instance.SetCanMove(true);
                journalScreen.transform.DOMove(UIManager.instance.puzzleUI.blockPuzzleInstantiatePos.position, 2);
                LightManager.instance.TurnOnPlayerLights();
            }
        }
    }

    public void FadeToTransparent(float duration)
    {
        // Tweens the alpha value to zero
        mainMenuPanel.GetComponent<Image>().DOColor(new Color(0, 0, 0, 0), duration);
    }

    public void FadeToBlack(float duration)
    {
        // Tweens the alpha value to 1
        mainMenuPanel.GetComponent<Image>().DOColor(new Color(0, 0, 0, 1), duration);
    }

    void MoveTitleAndFlowerDown()
    {
        title.transform.DOMove(titleActivePos.position, titleMoveDuration).SetEase(buttonCurve);
        flower.transform.DOMove(flowerActivePos.position, flowerMoveDuration).SetEase(titleMoveCurve).OnComplete(MoveMenuButtonsIn);
        AudioManager.instance.PlayOneShot(FMODEvents.instance.titleDown,transform.position);
    }

    void MoveMenuButtonsIn()
    {
        startButton.transform.DOLocalRotate(new Vector3(0,0,-40), startButtonMoveDuration,RotateMode.LocalAxisAdd).SetEase(buttonCurve).OnComplete(() => canStart = true);
    }

    void OpenCurtains()
    {
        curtainR.transform.DOLocalMoveX(0.8f, 6);
        curtainL.transform.DOLocalMoveX(-0.8f, 6);
        curtainL.transform.DOLocalRotate(new Vector3(0, 0, 35), curtainOpenDuration,RotateMode.LocalAxisAdd);
        curtainR.transform.DOLocalRotate(new Vector3(0, 0, -35), curtainOpenDuration, RotateMode.LocalAxisAdd);
        AudioManager.instance.PlayOneShot(FMODEvents.instance.curtainOpening,transform.position);
    }
    
    void CloseCurtains()
    {
        curtainL.transform.DOScaleX(82, curtainOpenDuration).SetEase(Ease.Linear);
        curtainR.transform.DOScaleX(82, curtainOpenDuration).SetEase(Ease.Linear);
        AudioManager.instance.PlayOneShot(FMODEvents.instance.curtainOpening,transform.position);
    }

    void MoveButtonsAndTitleAway()
    {
        startButton.transform.DOMove(TEMPUIAwayPos.position, startButtonMoveDuration).SetEase(menuAwayCurve);
        title.transform.DOMove(TEMPUIAwayPos.position, startButtonMoveDuration).SetEase(menuAwayCurve);
        flower.transform.DOMove(TEMPUIAwayPos.position, startButtonMoveDuration).SetEase(menuAwayCurve);
        AudioManager.instance.PlayOneShot(FMODEvents.instance.titleUp,transform.position);
    }

    //Buttons
    public void StartGameButton()
    {
        if (canStart == false) return; 
        mainMenuPanel.SetActive(false);
        mandala.GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 0), 1);
        CameraManager.instance.CameraZoomSequence();
        OpenCurtains();
        
        MoveButtonsAndTitleAway(); // TEMPorary
        AudioManager.instance.PlayOneShot(FMODEvents.instance.startButtonClick,transform.position);
        isDisplayed = false;
    }
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
