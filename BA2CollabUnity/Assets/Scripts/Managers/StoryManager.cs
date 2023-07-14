using JetBrains.Annotations;
using UnityEngine;
using static Environment;
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
        DegreeItem, TextPuzzleFirst, MedallionItem, GiftCardItem,BlockCollectItem1, NightStandItem, ThreeDPuzzleSecond, ThreeDPuzzleItemSecond,
        MoonShineLanternItem,
        TheatreArticleItem, TextPuzzleSecond, ThreeDPuzzleThird, ThreeDPuzzleItemThird,
        JewelryBoxItem, ThreeDPuzzleFourth, ThreeDPuzzleItemFourth,
        PosterItem, WallPosterItem,
        GlassesItem, GlassesPuzzle,
        LockBoxItem, LockPuzzle,
        ProjectorItem, ShelvesPuzzle,
        SecrectDrawerItem, ThreeDPuzzleFifth, ThreeDPuzzleItemFifth,
        FlowerPuzzle, ThreeDPuzzleSixth, ThreeDPuzzleItemSixth,
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
    [CanBeNull] public BlockManager blockManager;


    //items
    public Item3DPuzzle item3DPuzzleFam;
    public Item3DPuzzle item3DPuzzleHouse;
    public Item3DPuzzle item3DPuzzleTheat;
    public Item3DPuzzle item3DPuzzleProtest;
    public Item3DPuzzle item3DPuzzleNewFam;
    public Item3DPuzzle item3DPuzzleHos;

    //blocksToCollect
    public BlockCollectItem blockCollectItem1, blockCollectItem2, blockCollectItem3;

    private Transform tempParent;
    public Transform ItemHolder;

    private void Start()
    {
        CurrentState = GameState.Start;
    }

    private void Update()
    {
        SetCurrentGameState();
    }


    private void NefariousParentAbuseActivation(GameObject var)
    {
        tempParent = var.transform.parent.transform;
        var.transform.parent = ItemHolder;
        var.gameObject.SetActive(true);
        var.GetComponent<InteractableItem>().SetInteractable(true);
        var.transform.parent = tempParent;
    }



    private void SetCurrentGameState()
    {
        switch (CurrentState)
        {
            case GameState.Start:
                if (Player.instance != null && Environment.instance.currentRoom == CurrentRoom.Bedroom)
                {
                    puckNoteItem.SetInteractable(true);
                    CurrentState = GameState.PuckNoteItem;
                }
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
                    UIItemManager.instance.CollectItem(puckItem);
                    NefariousParentAbuseActivation(movingBoxItem.gameObject);
                    CurrentState = GameState.MovingBoxItemDropPuck;
                }
                break;
            // case GameState.FamilyPhotoItem:
            //     if (familyPhotoFrameItem.IsComplete())
            //     {
            //         puzzle2DManager.Activate();
            //         CurrentState = GameState.TwoDPuzzle;
            //     }
            //     break;
            // case GameState.TwoDPuzzle:
            //     if (puzzle2DManager.IsComplete())
            //     {
            //         Destroy(familyPhotoFrameItem);
            //         movingBoxItem.SetInteractable(true);
            //         CurrentState = GameState.MovingBoxItemDropPuck;
            //     }
            //      break;
            case GameState.MovingBoxItemDropPuck:
                if (movingBoxItem.boxState == MovingBoxState.PickingUpBox)
                {
                    CheckListManager.instance.AdvanceChecklist(); //putting puck in the box = start packing
                    item3DPuzzleFam.SetInteractable(true);
                    CurrentState = GameState.ThreeDPuzzleItemFirst;
                }
                break;
            case GameState.ThreeDPuzzleItemFirst:
                if (item3DPuzzleFam.IsComplete())
                {
                    blockManager.Activate();
                    CurrentState = GameState.ThreeDPuzzleFirst;
                }
                break;
            case GameState.ThreeDPuzzleFirst:
                if (blockManager.IsComplete())
                {
                    CurrentState = GameState.MovingBoxItemPickedUp;
                    Destroy(item3DPuzzleFam);
                    movingBoxItem.SetInteractable(true);
                }
                break;
            case GameState.MovingBoxItemPickedUp:
                if (movingBoxItem.boxState == MovingBoxState.DroppingBox)
                {
                    movingBoxItem.SetInteractable(true);
                    CurrentState = GameState.MovingBoxItemDropped;
                }
                break;
            case GameState.MovingBoxItemDropped:
                if (movingBoxItem.boxState == MovingBoxState.BoxDropped)
                {
                    CheckListManager.instance.AdvanceChecklist(); //putting the box in the entrance = dropping moving box finished 
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
                    CurrentState = GameState.BlockCollectItem1;
                }
                break;
            case GameState.BlockCollectItem1:
                if (blockCollectItem1.IsComplete())
                {
                    NefariousParentAbuseActivation(item3DPuzzleHouse.gameObject);
                    CurrentState = GameState.ThreeDPuzzleItemSecond;
                }
                break;
            case GameState.ThreeDPuzzleItemSecond:
                if (item3DPuzzleHouse.IsComplete())
                {
                    blockManager.Activate();
                    CurrentState = GameState.ThreeDPuzzleSecond;
                }
                break;
            case GameState.ThreeDPuzzleSecond:
                if (blockManager.IsComplete())
                {
                    CheckListManager.instance.AdvanceChecklist(); //bring cube to the livingroom = finishing first 3d puzzle in livingroom 
                    Destroy(item3DPuzzleHouse);
                    moonShineLanternItem.SetInteractable(true);
                    CurrentState = GameState.MoonShineLanternItem;
                }
                break;
            case GameState.MoonShineLanternItem:
                if (moonShineLanternItem.IsComplete())
                {
                    NefariousParentAbuseActivation(nightStandItem.gameObject);
                    CurrentState = GameState.NightStandItem;
                }
                break;
            case GameState.NightStandItem:
                if (nightStandItem.IsComplete() && blockCollectItem2.IsComplete())
                {
                    //theatreArticleItem.SetInteractable(true);
                    NefariousParentAbuseActivation(item3DPuzzleTheat.gameObject);
                    CurrentState = GameState.ThreeDPuzzleItemThird;
                }
                break;
            // case GameState.TheatreArticleItem:
            //     if (theatreArticleItem.IsComplete())
            //     {
            //         textBoxManager2.Activate();
            //         CurrentState = GameState.TextPuzzleSecond;
            //     }
            //     break;
            // case GameState.TextPuzzleSecond:
            //     if (textBoxManager2.IsComplete())
            //     {
            //         Destroy(textBoxManager2.gameObject);
            //         NefariousParentAbuseActivation(item3DPuzzleTheat.gameObject);
            //         CurrentState = GameState.ThreeDPuzzleItemThird;
            //     }
            //     break;
            case GameState.ThreeDPuzzleItemThird:
                if (item3DPuzzleTheat.IsComplete())
                {

                    blockManager.Activate();
                    CurrentState = GameState.ThreeDPuzzleThird;
                }
                break;
            case GameState.ThreeDPuzzleThird:
                if (blockManager.IsComplete())
                {
                    Destroy(item3DPuzzleTheat);
                    NefariousParentAbuseActivation(jewelryBoxItem.gameObject);
                    CurrentState = GameState.JewelryBoxItem;
                }
                break;
            case GameState.JewelryBoxItem:
                if (jewelryBoxItem.IsComplete())
                {
                    CheckListManager.instance.AdvanceChecklist(); //whos ring is it? = after ionteract with the ring
                    item3DPuzzleProtest.SetInteractable(true);
                    CurrentState = GameState.ThreeDPuzzleItemFourth;
                }
                break;
            case GameState.ThreeDPuzzleItemFourth:
                if (item3DPuzzleProtest.IsComplete())
                {
                    blockManager.Activate();
                    CurrentState = GameState.ThreeDPuzzleFourth;
                }
                break;
            case GameState.ThreeDPuzzleFourth:
                if (blockManager.IsComplete())
                {
                    Destroy(item3DPuzzleProtest);
                    posterItem.SetInteractable(true);
                    CurrentState = GameState.PosterItem;
                }
                break;
            case GameState.PosterItem:
                if (posterItem.IsComplete())
                {
                    UIItemManager.instance.CollectItem(posterItem);
                    NefariousParentAbuseActivation(wallPosterItem.gameObject);
                    CurrentState = GameState.WallPosterItem;
                }
                break;
            case GameState.WallPosterItem:
                if (wallPosterItem.IsComplete())
                {
                    CheckListManager.instance.AdvanceChecklist(); //posters belong in the basement == atfer posterwall 
                    glassesItem.SetInteractable(true);
                    UIItemManager.instance.CollectItem(glassesItem);
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
                    NefariousParentAbuseActivation(lockBoxItem.gameObject);
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
                    UIItemManager.instance.CollectItem(projectorItem);//slide
                    CurrentState = GameState.ProjectorItem;
                }
                break;
            case GameState.ProjectorItem:
                if (projectorItem.IsComplete())
                {
                    NefariousParentAbuseActivation(shelvesManager.gameObject);
                    CurrentState = GameState.ShelvesPuzzle;
                }
                break;
            case GameState.ShelvesPuzzle:
                if (shelvesManager.IsComplete())
                {
                    CheckListManager.instance.AdvanceChecklist(); //What are these letters for? == shelf puzzle done
                    Destroy(shelvesManager);
                    secrectDrawerItem.SetInteractable(true);
                    CurrentState = GameState.SecrectDrawerItem;
                }
                break;
            case GameState.SecrectDrawerItem:
                if (secrectDrawerItem.IsComplete() && blockCollectItem3.IsComplete())
                {
                    NefariousParentAbuseActivation(item3DPuzzleNewFam.gameObject);
                    CurrentState = GameState.ThreeDPuzzleItemFifth;
                }
                break;
            case GameState.ThreeDPuzzleItemFifth:
                if (item3DPuzzleNewFam.IsComplete())
                {
                    blockManager.Activate();
                    CurrentState = GameState.ThreeDPuzzleFifth;
                }
                break;
            case GameState.ThreeDPuzzleFifth:
                if (blockManager.IsComplete())
                {
                    Destroy(item3DPuzzleNewFam);
                    plantManager.Activate();
                    CurrentState = GameState.FlowerPuzzle;
                }
                break;
            case GameState.FlowerPuzzle:
                if (plantManager.IsComplete())
                {
                    CheckListManager.instance.AdvanceChecklist(); //after flowerpuzzle
                    Destroy(plantManager);
                    NefariousParentAbuseActivation(item3DPuzzleHos.gameObject);
                    CurrentState = GameState.ThreeDPuzzleItemSixth;
                }
                break;
            case GameState.ThreeDPuzzleItemSixth:
                if (item3DPuzzleHos.IsComplete())
                {
                    blockManager.Activate();
                    CurrentState = GameState.ThreeDPuzzleSixth;
                }
                break;
            case GameState.ThreeDPuzzleSixth:
                if (blockManager.IsComplete())
                {
                    Destroy(item3DPuzzleHos);
                    movingBoxItem.SetInteractable(true);
                    movingBoxItem.boxState = MovingBoxState.End;
                    CurrentState = GameState.MovingBoxItemEnd;
                }
                break;

        }
    }

}
