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

    [Header("Objects")]
    public GameObject mainMenuPanel;
    [SerializeField] private GameObject title;
    [SerializeField] private GameObject flower;
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject exitButton;
    [SerializeField] private GameObject curtainL;
    [SerializeField] private GameObject curtainR;
    [SerializeField] private GameObject mandala;

    [Header("Positions")]
    [SerializeField] private Transform camEndPos;
    [SerializeField] private Transform camBeginPos;
    [SerializeField] private Transform titleActivePos;
    [SerializeField] private Transform flowerActivePos;
    [SerializeField] private Transform startButtonActivePos;
    [SerializeField] private Transform exitButtonActivePos;
    [SerializeField] private Transform TEMPUIAwayPos;

    [Header("Durations")]
    [SerializeField] private float cameraZoomDuration = 2;
    [SerializeField] private float titleMoveDuration = 4;
    [SerializeField] private float flowerMoveDuration = 3;
    [SerializeField] private float startButtonMoveDuration = 2;
    [SerializeField] private float exitButtonMoveDuration = 2;
    [SerializeField] private float curtainOpenDuration = 2;

    [Header("Scale")]
    [SerializeField] private float targetScaleX;

    [Header("Curves")]
    [SerializeField] private AnimationCurve titleMoveCurve;
    [SerializeField] private AnimationCurve menuAwayCurve;
    [SerializeField] private AnimationCurve buttonCurve;

    [Header("Values")]
    public float mandalaTurnSpeed = 15f;


    public GameObject tempFocus;
    public float offSetY = 2;
    public float offSetZ = 2;
    public float offSetX = 2;

    public bool look = false;
    private bool canPause = false;
    private bool canStart = false;

    private void Start()
    {
        // starts without input
        MainMenuSequence();
    }

    private void Update()
    {
        if (look)
        {
            Camera.main.transform.LookAt(tempFocus.transform);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenuSequence();
        }

        TurnMandala();
    }

    void TurnMandala()
    {
        mandala.transform.Rotate(Vector3.forward, mandalaTurnSpeed * Time.deltaTime);
    }

    
    private void CameraZoomSequence()
    {
        Camera.main.transform.DOMove(camEndPos.transform.position, cameraZoomDuration);
        Camera.main.transform.DORotate(new Vector3(2.28f, 0, 0), cameraZoomDuration).OnComplete(OnCameraZoomEnd);
    }
    private void CameraZoomOutSequence()
    {
        Camera.main.transform.DOMove(camBeginPos.transform.position, cameraZoomDuration);
        Camera.main.transform.DORotate(new Vector3(-1.97f, 0, 0), cameraZoomDuration).OnComplete(() => UIManager.instance.dialogues.dialogueBox.MoveToPassivePos());
    }

    private void OnCameraZoomEnd()
    {
        look = true;
        canPause = true;
        Player.instance.SetCanMove(true);
        LightManager.instance.TurnOffFrontStageLights();
        LightManager.instance.TurnOnPlayerLights();
        UIManager.instance.dialogues.dialogueBox.MoveToActivePos();
    }

    // the sequence of events when we start the game and re load the scene
    void MainMenuSequence()
    {
        FadeToTransparent(4);
        MoveTitleAndFlowerDown();
    }

    void PauseMenuSequence()
    {
        if (canPause == false) return;
        canPause = false;
        look = false;
        CameraZoomOutSequence();
        Player.instance.SetCanMove(false);
        LightManager.instance.TurnOffPlayerLights();
        LightManager.instance.TurnOnFrontStageLights();
        MoveMenuButtonsIn();
        CloseCurtains();
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
        title.transform.DOMove(titleActivePos.position, titleMoveDuration).SetEase(titleMoveCurve).OnComplete(MoveMenuButtonsIn);
        flower.transform.DOMove(flowerActivePos.position, flowerMoveDuration).SetEase(titleMoveCurve).OnComplete(MoveMenuButtonsIn);
        AudioManager.instance.PlayOneShot(FMODEvents.instance.titleDown,transform.position);
    }

    void MoveMenuButtonsIn()
    {
        startButton.transform.DOMove(startButtonActivePos.position, startButtonMoveDuration).SetEase(buttonCurve);
        exitButton.transform.DOMove(exitButtonActivePos.position, exitButtonMoveDuration).SetEase(buttonCurve)
            .OnComplete(() => canStart = true);
    }

    void OpenCurtains()
    {
        curtainL.transform.DOScaleX(targetScaleX, curtainOpenDuration).SetEase(Ease.Linear);
        curtainR.transform.DOScaleX(targetScaleX, curtainOpenDuration).SetEase(Ease.Linear);
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
        exitButton.transform.DOMove(TEMPUIAwayPos.position, startButtonMoveDuration).SetEase(menuAwayCurve);
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
        CameraZoomSequence();
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
