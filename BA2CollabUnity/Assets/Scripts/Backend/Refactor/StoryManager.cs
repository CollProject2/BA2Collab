using UnityEngine;
using static MovingBoxItem;

public class StoryManager : MonoBehaviour
{
    public enum GameState
    {
        Start,
        PuckNoteItem, PuckItem, FamilyPhotoItem, TwoDPuzzle,
        MovingBoxItemPickedUp, ThreeDPuzzleFirst, MovingBoxItemDropped,
        DegreeItem, TextPuzzleFirst,
        MedallionItem, GiftCardItem, NightStandItem, ThreeDPuzzleSecond,
        MoonShineLanternItem, EntranceTriggerLantern, ThreeDPuzzleBlockFirst,
        TheatreArticleItem, TextPuzzleSecond, ThreeDPuzzleThird,
        JewelryBoxItem, ThreeDPuzzleFourth,
        PosterItem, WallPosterItem,
        GlassesItem, GlassesPuzzle,
        LockBoxItem, LockPuzzle,
        ProjectorItem, ShelvesPuzzle,
        SecrectDrawerItem, ThreeDPuzzleBlockSecond, ThreeDPuzzleFifth,
        FlowerPuzzle, ThreeDPuzzleSixth, ThreeDPuzzleBlockThird,
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
                    movingBoxItem.boxState = MovingBoxState.PickedUp;
                    CurrentState = GameState.MovingBoxItemPickedUp;
                }
                break;
            case GameState.MovingBoxItemPickedUp:
                if (movingBoxItem.boxState == MovingBoxState.PickedUp)
                {
                    blockManager1.Activate();
                    CurrentState = GameState.ThreeDPuzzleFirst;
                }
                break;
            case GameState.ThreeDPuzzleFirst:
                if (blockManager1.IsComplete())
                {
                    movingBoxItem.boxState = MovingBoxState.Dropped;
                    CurrentState = GameState.MovingBoxItemDropped;
                }
                break;
            case GameState.MovingBoxItemDropped:
                if (movingBoxItem.boxState == MovingBoxState.Dropped)
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
                    nightStandItem.SetInteractable(true);
                    CurrentState = GameState.NightStandItem;
                }
                break;
            case GameState.NightStandItem:
                if (nightStandItem.IsComplete())
                {
                    nightStandItem.Collect3DPuzzleBlock();
                    CurrentState = GameState.ThreeDPuzzleBlockFirst;
                }
                break;
            case GameState.ThreeDPuzzleBlockFirst:
                if (blockManager2.IsComplete())
                {
                    moonShineLanternItem.SetInteractable(true);
                    CurrentState = GameState.MoonShineLanternItem;
                }
                break;
            case GameState.MoonShineLanternItem:
                if (moonShineLanternItem.IsComplete())
                {
                    entranceTriggerLantern.Collect3DPuzzleBlock();
                    CurrentState = GameState.ThreeDPuzzleBlockSecond;
                }
                break;
            case GameState.ThreeDPuzzleBlockSecond:
                if (entranceTriggerLantern.HasCollectedBlock())
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
                    blockManager4.Activate();
                    CurrentState = GameState.ThreeDPuzzleThird;
                }
                break;
            case GameState.ThreeDPuzzleThird:
                if (blockManager4.IsComplete())
                {
                    jewelryBoxItem.SetInteractable(true);
                    CurrentState = GameState.JewelryBoxItem;
                }
                break;
            case GameState.JewelryBoxItem:
                if (jewelryBoxItem.IsComplete())
                {
                    blockManager5.Activate();
                    CurrentState = GameState.ThreeDPuzzleFourth;
                }
                break;
            case GameState.ThreeDPuzzleFourth:
                if (blockManager5.IsComplete())
                {
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
                    secrectDrawerItem.SetInteractable(true);
                    CurrentState = GameState.SecrectDrawerItem;
                }
                break;
            case GameState.SecrectDrawerItem:
                if (secrectDrawerItem.IsComplete())
                {
                    secrectDrawerItem.Collect3DPuzzleBlock();
                    CurrentState = GameState.ThreeDPuzzleBlockThird;
                }
                break;
            case GameState.ThreeDPuzzleBlockThird:
                if (secrectDrawerItem.HasCollectedBlock())
                {
                    plantManager.Activate();
                    CurrentState = GameState.FlowerPuzzle;
                }
                break;
            case GameState.ThreeDPuzzleFifth:
                if (blockManager5.IsComplete())
                {
                    plantManager.Activate();
                    CurrentState = GameState.FlowerPuzzle;
                }
                break;
            case GameState.FlowerPuzzle:
                if (plantManager.IsComplete())
                {
                    blockManager6.Activate();
                    CurrentState = GameState.ThreeDPuzzleSixth;
                }
                break;
            case GameState.ThreeDPuzzleSixth:
                if (blockManager6.IsComplete())
                {
                    movingBoxItem.boxState = MovingBoxState.End;
                    CurrentState = GameState.MovingBoxItemEnd;
                }
                break;
            case GameState.MovingBoxItemEnd:
                //ending shit 
                break;
        }
    }

    // Call function after everything once completed (item and puzzle)
    public void AdvanceGameState()
    {
        CurrentState++;
    }
}
