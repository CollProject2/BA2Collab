using System.Collections.Generic;
using UnityEngine;

public class ShelvesManager : InteractableItem
{
    public string associatedMemory;
    public List<Letter> isRight = new();
    public static ShelvesManager instance = null;
    public Letter currentLetter;
    public GameObject adButtons;
    

    protected override void Awake()
    {
        if (instance == null)
            instance = this;
        isInteractable = false;
    }

    protected override void Update()
    {
        if (Environment.instance.currentRoom == Environment.CurrentRoom.LivingDiningRoom && isInteractable)
        {
            interactParticle.enabled = true;
            if (currentLetter != null)
                CheckInput(); 
        }
        else
        {
            currentLetter = null;
        }
    }

    //set this as the current block
    public void SetCurrentLetter(Letter letter)
    {
        if (currentLetter == null)
        {
            Player.instance.SetCanMove(false);
            Player.instance.isSolving = true;
            adButtons.gameObject.SetActive(true);
        }
            
        //do ui stuff
        
        currentLetter = letter;
    }

    public void CallCheck()
    {
        if (ShelvesAreSolved())
        {
            isInteractable = false;
            Player.instance.SetCanMove(true);
            //recall memory
            Player.instance.RecallMemory(associatedMemory);
            interactParticle.enabled = false;
            isComplete = true;
            adButtons.gameObject.SetActive(false);
        }

    }

    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.A))
            RotateLetterAt(currentLetter.id, RotationLR.Left);
        else if (Input.GetKeyDown(KeyCode.D))
            RotateLetterAt(currentLetter.id, RotationLR.Right);
    }

    //rotate the block with the given index in the list
    public void RotateLetterAt(int index, RotationLR direction)
    {
        if (index >= 0 && index < isRight.Count)
            isRight[index].RotateLetter(direction);
    }

    private bool ShelvesAreSolved()
    {
        foreach (Letter v in isRight)
            if (!v.solved) return false;
        return true;
    }

}
