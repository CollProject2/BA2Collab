using UnityEngine;
using static MovingBoxItem;

public class StoryManager : MonoBehaviour
{
    public static StoryManager instance = null;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public enum GameState
    {
        Start,
        PuckNoteItem, PuckItem, FamilyPhotoItem, TwoDPuzzle,
        MovingBoxItemDropPuck, ThreeDPuzzleFirst, ThreeDPuzzleItemFirst, MovingBoxItemPickedUp, MovingBoxItemDropped,
        DegreeItem, TextPuzzleFirst, MedallionItem, GiftCardItem, NightStandItem, ThreeDPuzzleSecond, ThreeDPuzzleItemSecond,
        MoonShineLanternItem, EntranceTriggerLantern, ThreeDPuzzleBlockFirst,
        TheatreArticleItem, TextPuzzleSecond, ThreeDPuzzleThird, ThreeDPuzzleItemThird,
        JewelryBoxItem, ThreeDPuzzleFourth, ThreeDPuzzleItemFourth,
        PosterItem, WallPosterItem,
        GlassesItem, GlassesPuzzle,
        LockBoxItem, LockPuzzle,
        ProjectorItem, ShelvesPuzzle,
        SecrectDrawerItem, ThreeDPuzzleBlockSecond, ThreeDPuzzleFifth, ThreeDPuzzleItemFifth,
        FlowerPuzzle, ThreeDPuzzleSixth, ThreeDPuzzleItemSixth, ThreeDPuzzleBlockThird,
        MovingBoxItemEnd
    }

    public GameState CurrentState { get; private set; }

    // Items
    public PuckNoteItem puckNoteItem;
    public PuckItem puckItem;
    public FamilyPhotoFrameItem familyPhotoFrameItem;
    public MovingBoxItem movingBoxItem;
    public DegreeItem degreeItem;
    public MedallionItem medallionItem;
    public GiftCardItem giftCardItem;
    public NightStandItem nightStandItem;
    public MoonShineLanternItem moonShineLanternItem;
    public EntranceTriggerLantern entranceTriggerLantern;
    public TheatreArticleItem theatreArticleItem;
    public JeweleryBoxItem jewelryBoxItem;
    public PosterItem posterItem;
    public WallPosterItem wallPosterItem;
    public GlassesItem glassesItem;
    public LockBoxItem lockBoxItem;
    public ProjectorItem projectorItem;
    public SecretDrawerItem secrectDrawerItem;

    // To check the completion status of puzzles
    public Puzzle2DManager puzzle2DManager;
    public GlassesManager glassesManager;
    public LockManager lockManager;
    public ShelvesManager shelvesManager;
    public PlantManager plantManager;

    // TextPuzzles
    public TextBoxManager textBoxManager1; // degree
    public TextBoxManager textBoxManager2; // theatre

    // 3DPuzzles
    public BlockManager blockManager1;
    public BlockManager blockManager2;
    public BlockManager blockManager3;
    public BlockManager blockManager4;
    public BlockManager blockManager5;
    public BlockManager blockManager6;

    //items
    public Item3DPuzzle item3DPuzzleFam;
    public Item3DPuzzle item3DPuzzleHouse;
    public Item3DPuzzle item3DPuzzleTheat;
    public Item3DPuzzle item3DPuzzleProtest;
    public Item3DPuzzle item3DPuzzleNewFam;
    public Item3DPuzzle item3DPuzzleHos;

    //blocksToCollect
    public BlockCollectItem blockCollectItem1;
    public BlockCollectItem blockCollectItem3;



    private void Start()
    {
        CurrentState = GameState.Start;
    }

    private void Update()
    {
        SetCurrentGameState();
    }

    private void SetCurrentGameState()
    {
        switch (CurrentState)
        {
            case GameState.Start:
                puckNoteItem.SetInteractable(true);
                break;
            case GameState.PuckNoteItem:
                if (puckNoteItem.IsComplete())
                {
                    puckItem.SetInteractable(true);
                    CurrentState = GameState.PuckItem;
                }
                break;
            case GameState.PuckItem:
                if (puckItem.IsComplete())
                {
                    familyPhotoFrameItem.SetInteractable(true);
                    CurrentState = GameState.FamilyPhotoItem;
                }
                break;
            case GameState.FamilyPhotoItem:
                if (familyPhotoFrameItem.IsComplete())
                {
                    puzzle2DManager.Activate();
                    CurrentState = GameState.TwoDPuzzle;
                }
                break;
            case GameState.TwoDPuzzle:
                if (puzzle2DManager.IsComplete())
                {
                    movingBoxItem.SetInteractable(true);
                    CurrentState = GameState.MovingBoxItemDropPuck;
                }
                break;
            case GameState.MovingBoxItemDropPuck:
                if (movingBoxItem.boxState == MovingBoxState.DroppingPuck)
                {
                    item3DPuzzleFam.SetInteractable(true);
                    CurrentState = GameState.ThreeDPuzzleItemFirst;
                }
                break;
            case GameState.ThreeDPuzzleItemFirst:
                if (item3DPuzzleFam.IsComplete())
                {
                    blockManager1.Activate();
                    CurrentState = GameState.ThreeDPuzzleFirst;
                }
                break;
            case GameState.ThreeDPuzzleFirst:
                if (blockManager1.IsComplete())
                {
                    item3DPuzzleFam.DestroyItemScript();
                    blockManager1.DestroyPuzzle();
                    movingBoxItem.SetInteractable(true);
                    CurrentState = GameState.MovingBoxItemPickedUp;
                }
                break;
            case GameState.MovingBoxItemPickedUp:
                if (movingBoxItem.boxState == MovingBoxState.PickingUpBox)
                {
                    movingBoxItem.SetInteractable(true);
                    CurrentState = GameState.MovingBoxItemDropped;
                }
                break;
            case GameState.MovingBoxItemDropped:
                if (movingBoxItem.boxState == MovingBoxState.DroppingBox)
                {
                    degreeItem.SetInteractable(true);
                    CurrentState = GameState.DegreeItem;
                }
                break;
            case GameState.DegreeItem:
                if (degreeItem.IsComplete())
                {
                    textBoxManager1.Activate();
                    CurrentState = GameState.TextPuzzleFirst;
                }
                break;
            case GameState.TextPuzzleFirst:
                if (textBoxManager1.IsComplete())
                {
                    Destroy(textBoxManager1.gameObject);
                    medallionItem.SetInteractable(true);
                    CurrentState = GameState.MedallionItem;
                }
                break;
            case GameState.MedallionItem:
                if (medallionItem.IsComplete())
                {
                    giftCardItem.SetInteractable(true);
                    CurrentState = GameState.GiftCardItem;
                }
                break;
            case GameState.GiftCardItem:
                if (giftCardItem.IsComplete())
                {
                    blockCollectItem1.SetInteractable(true);
                    item3DPuzzleHouse.SetInteractable(true);
                   CurrentState = GameState.ThreeDPuzzleItemSecond;
                }
                break;
            case GameState.ThreeDPuzzleItemSecond:
                if (item3DPuzzleHouse.IsComplete())
                {
                    blockManager2.Activate();
                    CurrentState = GameState.ThreeDPuzzleSecond;
                }
                break;
            case GameState.ThreeDPuzzleSecond:
                if (blockManager2.IsComplete())
                {
                    item3DPuzzleHouse.DestroyItemScript();
                    blockManager2.DestroyPuzzle();
                    moonShineLanternItem.SetInteractable(true);
                    CurrentState = GameState.MoonShineLanternItem;
                }
                break;
            case GameState.MoonShineLanternItem:
                if (moonShineLanternItem.IsComplete())
                {
                    nightStandItem.SetInteractable(true);
                    CurrentState = GameState.NightStandItem;
                }
                break;
            case GameState.NightStandItem:
                if (nightStandItem.IsComplete())
                {
                    theatreArticleItem.SetInteractable(true);
                    CurrentState = GameState.TheatreArticleItem;
                }
                break;
            case GameState.TheatreArticleItem:
                if (theatreArticleItem.IsComplete())
                {
                    textBoxManager2.Activate();
                    CurrentState = GameState.TextPuzzleSecond;
                }
                break;
            case GameState.TextPuzzleSecond:
                if (textBoxManager2.IsComplete())
                {
                    Destroy(textBoxManager2.gameObject);
                    item3DPuzzleTheat.SetInteractable(true);
                    CurrentState = GameState.ThreeDPuzzleItemThird;
                }
                break;
            case GameState.ThreeDPuzzleItemThird:
                if (item3DPuzzleTheat.IsComplete())
                {
                    blockManager3.Activate();
                    CurrentState = GameState.ThreeDPuzzleThird;
                }
                break;
            case GameState.ThreeDPuzzleThird:
                if (blockManager3.IsComplete())
                {
                    item3DPuzzleTheat.DestroyItemScript();
                    blockManager3.DestroyPuzzle();
                    jewelryBoxItem.SetInteractable(true);
                    CurrentState = GameState.JewelryBoxItem;
                }
                break;
            case GameState.JewelryBoxItem:
                if (jewelryBoxItem.IsComplete())
                {
                    item3DPuzzleProtest.SetInteractable(true);
                    CurrentState = GameState.ThreeDPuzzleItemFourth;
                }
                break;
            case GameState.ThreeDPuzzleItemFourth:
                if (item3DPuzzleProtest.IsComplete())
                {
                    blockManager4.Activate();
                    CurrentState = GameState.ThreeDPuzzleFourth;
                }
                break;
            case GameState.ThreeDPuzzleFourth:
                if (blockManager4.IsComplete())
                {
                    item3DPuzzleProtest.DestroyItemScript();
                    blockManager4.DestroyPuzzle();
                    posterItem.SetInteractable(true);
                    CurrentState = GameState.PosterItem;
                }
                break;
            case GameState.PosterItem:
                if (posterItem.IsComplete())
                {
                    wallPosterItem.SetInteractable(true);
                    CurrentState = GameState.WallPosterItem;
                }
                break;
            case GameState.WallPosterItem:
                if (wallPosterItem.IsComplete())
                {
                    glassesItem.SetInteractable(true);
                    CurrentState = GameState.GlassesItem;
                }
                break;
            case GameState.GlassesItem:
                if (glassesItem.IsComplete())
                {
                    glassesManager.Activate();
                    CurrentState = GameState.GlassesPuzzle;
                }
                break;
            case GameState.GlassesPuzzle:
                if (glassesManager.IsComplete())
                {
                    lockBoxItem.SetInteractable(true);
                    CurrentState = GameState.LockBoxItem;
                }
                break;
            case GameState.LockBoxItem:
                if (lockBoxItem.IsComplete())
                {
                    lockManager.Activate();
                    CurrentState = GameState.LockPuzzle;
                }
                break;
            case GameState.LockPuzzle:
                if (lockManager.IsComplete())
                {
                    Destroy(lockManager.gameObject);
                    projectorItem.SetInteractable(true);
                    CurrentState = GameState.ProjectorItem;
                }
                break;
            case GameState.ProjectorItem:
                if (projectorItem.IsComplete())
                {
                    shelvesManager.Activate();
                    CurrentState = GameState.ShelvesPuzzle;
                }
                break;
            case GameState.ShelvesPuzzle:
                if (shelvesManager.IsComplete())
                {
                    shelvesManager.DestroyitemScript();
                    secrectDrawerItem.SetInteractable(true);
                    CurrentState = GameState.SecrectDrawerItem;
                }
                break;
            case GameState.SecrectDrawerItem:
                if (secrectDrawerItem.IsComplete())
                {
                    item3DPuzzleNewFam.SetInteractable(true);
                    CurrentState = GameState.ThreeDPuzzleItemFifth;
                }
                break;
            case GameState.ThreeDPuzzleItemFifth:
                if (item3DPuzzleNewFam.IsComplete())
                {
                    blockManager5.Activate();
                    CurrentState = GameState.ThreeDPuzzleFifth;
                }
                break;
            case GameState.ThreeDPuzzleFifth:
                if (blockManager5.IsComplete())
                {
                    item3DPuzzleNewFam.DestroyItemScript();
                    blockManager5.DestroyPuzzle();
                    plantManager.Activate();
                    CurrentState = GameState.FlowerPuzzle;
                }
                break;
            case GameState.FlowerPuzzle:
                if (plantManager.IsComplete())
                {
                    Destroy(plantManager);
                    item3DPuzzleHos.SetInteractable(true);
                    CurrentState = GameState.ThreeDPuzzleItemSixth;
                }
                break;
            case GameState.ThreeDPuzzleItemSixth:
                if (item3DPuzzleHos.IsComplete())
                {
                    blockManager6.Activate();
                    CurrentState = GameState.ThreeDPuzzleSixth;
                }
                break;
            case GameState.ThreeDPuzzleSixth:
                if (blockManager6.IsComplete())
                {
                    item3DPuzzleHos.DestroyItemScript();
                    blockManager6.DestroyPuzzle();
                    movingBoxItem.SetInteractable(true);
                    CurrentState = GameState.MovingBoxItemEnd;
                }
                break;
            case GameState.MovingBoxItemEnd:
                if (movingBoxItem.boxState == MovingBoxState.End)
                {
                    //ending shit 
                }
                break;
        }
    }

    // Call function after everything once completed (item and puzzle)
    public void AdvanceGameState()
    {
        CurrentState++;
    }
}
