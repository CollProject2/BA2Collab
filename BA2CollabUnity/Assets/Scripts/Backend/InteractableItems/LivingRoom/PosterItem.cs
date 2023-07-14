using UnityEngine;

public class PosterItem : InteractableItem
{
    public string posterMemory;


    protected override void Collect()
    {
        base.Collect();
        UIManager.instance.dialogues.StartDialogue(posterMemory);
        Player.instance.hasPoster = true;
        Player.instance.isSolving = true;


        MeshRenderer[] renders; 
        renders= GetComponentsInChildren<MeshRenderer>();

        if (renders != null)
        {
            foreach (MeshRenderer rend in renders)
                rend.enabled = false;
        }

        SetIsComplete(true);
    }
}