using System.Collections.Generic;
using UnityEngine;

public class ShelvesManager : MonoBehaviour
{
    public string associatedMemory;
    public bool isInteractable;
    public List<Letter> isRight = new();
    public static ShelvesManager instance = null;
    public Letter currentLetter;
    public List<GameObject> particles;
    private bool doOnce;
    private bool isComplete;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        isInteractable = false;
        doOnce = true;
    }

    private void Update()
    {
        if (Environment.instance.currentRoom == Environment.CurrentRoom.LivingDiningRoom && isInteractable)
        {
            SetParticleSystem(true);
            if (currentLetter != null)
                CheckInput(); 
        }
        else
        {
            currentLetter = null;
        }
    }
    public bool IsComplete()
    {
        return isComplete;
    }

    //set this as the current block
    public void SetCurrentLetter(Letter letter)
    {
        if(currentLetter == null)
            Player.instance.SetCanMove(false); 
        //do ui stuff
        
        currentLetter = letter;
    }

    public void CallCheck()
    {
        if (ShelvesAreSolved())
        {
            isInteractable = false;
            doOnce = true;
            SetParticleSystem(isInteractable);
            Player.instance.SetCanMove(true);
            //recall memory
            Player.instance.RecallMemory(associatedMemory);
            isComplete = true;
            StoryManager.instance.AdvanceGameState();
        }

    }

    public void SetParticleSystem(bool enabled)
    {
        if (doOnce)
            foreach (var particle in particles) { particle.SetActive(enabled); } 
        doOnce = false;
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
    public void DestroyitemScript()
    {
        Destroy(this);
    }
    internal void Activate()
    {
        isInteractable = true;
    }
}
