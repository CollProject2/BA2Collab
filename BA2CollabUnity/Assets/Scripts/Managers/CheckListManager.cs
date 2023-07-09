using System.Collections.Generic;
using UnityEngine;

public class CheckListManager : MonoBehaviour
{
    public List<GameObject> checkList;
    public static CheckListManager instance;
    private int currentCheck;
    //putting puck in the box = start packing
    //putting the box in the entrance = dropping moving box finished 
    //bring cube to the livingroom = finishing first 3d puzzle in livingroom 
    //whos ring is it? = after ionteract with the ring
    //posters belong in the basement == atfer posterwall 
    //What are these letters for? == shelf puzzle done
    //visit the garden == after zoom in?
    //look for sound = after flowerpuzzle 
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        currentCheck = 0;
    }

    private void Check()
    {
        for (int i = 0; i < checkList.Count; i++)
        {
            if (i-1 <= currentCheck)
                checkList[i].SetActive(true);
        }
    }

    public void AdvanceChecklist()
    {
        currentCheck++;
        Check();
    }

}
