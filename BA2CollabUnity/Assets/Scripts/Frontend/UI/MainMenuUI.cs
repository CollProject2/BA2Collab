using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    // the UI panel appears instantly now, in the future we will have a sequence
    // zoom in, open curtains, open related buttons....

    //Properties
    public bool isDisplayed { get; private set; }

    [Header("Objects")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject title;
    [SerializeField] private GameObject flower;
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject exitButton;
    [SerializeField] private GameObject curtainL;
    [SerializeField] private GameObject curtainR;
    [SerializeField] private GameObject mandala;

    [Header("Positions")]
    [SerializeField] private Transform camEndPos;
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

    private void Start()
    {
        // starts without input
        DisplayMainMenuUI();
        MainMenuSequence();
    }

    private void Update()
    {
        if (look)
        {
            Camera.main.transform.LookAt(tempFocus.transform);
        }

        TurnMandala();
    }

    void TurnMandala()
    {
        mandala.transform.Rotate(Vector3.forward, mandalaTurnSpeed * Time.deltaTime);
    }

    void DisplayMainMenuUI()
    {
        isDisplayed = true;
        mainMenuPanel.SetActive(true);
    }
    private void CameraZoomSequence()
    {
        Camera.main.transform.DOMove(camEndPos.transform.position, cameraZoomDuration);
        Camera.main.transform.DORotate(new Vector3(2.28f, 0, 0), cameraZoomDuration).OnComplete(OnCameraZoomEnd);
    }

    private void OnCameraZoomEnd()
    {
        look = true;
        Player.instance.SetCanMove(true);
        LightManager.instance.TurnOffFrontStageLights();
        LightManager.instance.TurnOnPlayerLights();
        UIManager.instance.dialogues.dialogueBox.MoveToActivePos();
    }

    // the sequence of events when we start the game and re load the scene
    void MainMenuSequence()
    {
        FadeToTransparent();
        MoveTitleAndFlowerDown();
    }

    void FadeToTransparent()
    {
        // Tweens the alpha value to zero
        mainMenuPanel.GetComponent<Image>().DOColor(new Color(0, 0, 0, 0), 4);
    }

    void MoveTitleAndFlowerDown()
    {
        title.transform.DOMove(titleActivePos.position, titleMoveDuration).SetEase(titleMoveCurve).OnComplete(MoveMenuButtonsIn);
        flower.transform.DOMove(flowerActivePos.position, flowerMoveDuration).SetEase(titleMoveCurve).OnComplete(MoveMenuButtonsIn);
    }

    void MoveMenuButtonsIn()
    {
        startButton.transform.DOMove(startButtonActivePos.position, startButtonMoveDuration).SetEase(buttonCurve);
        exitButton.transform.DOMove(exitButtonActivePos.position, exitButtonMoveDuration).SetEase(buttonCurve);
    }

    void OpenCurtains()
    {
        curtainL.transform.DOScaleX(targetScaleX, curtainOpenDuration).SetEase(Ease.Linear);
        curtainR.transform.DOScaleX(targetScaleX, curtainOpenDuration).SetEase(Ease.Linear);
    }

    void MoveButtonsAndTitleAway()
    {
        startButton.transform.DOMove(TEMPUIAwayPos.position, startButtonMoveDuration).SetEase(menuAwayCurve);
        exitButton.transform.DOMove(TEMPUIAwayPos.position, startButtonMoveDuration).SetEase(menuAwayCurve);
        title.transform.DOMove(TEMPUIAwayPos.position, startButtonMoveDuration).SetEase(menuAwayCurve);
        flower.transform.DOMove(TEMPUIAwayPos.position, startButtonMoveDuration).SetEase(menuAwayCurve);

    }

    //Buttons
    public void StartGameButton()
    {
        mainMenuPanel.SetActive(false);
        mandala.GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 0), 1);
        CameraZoomSequence();
        OpenCurtains();
        MoveButtonsAndTitleAway(); // TEMPorary
        
        isDisplayed = false;
    }
    public void ExitGameButton()
    {
        Application.Quit();
    }
}
