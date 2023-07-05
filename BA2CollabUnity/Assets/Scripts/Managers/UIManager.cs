using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    //Singelton instance
    public static UIManager instance = null;

    //Properties
    public Dialogues dialogues;
    public PuzzleUI puzzleUI;
    public MainMenuUI MainMenuUI;
    
    private void Awake()
    {
        //Singelton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        
    }
}
