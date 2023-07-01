using System;
using UnityEngine;

public class EntranceTriggerLantern : MonoBehaviour
{
    public string triggerMemory;

    private void OnTriggerEnter(Collider other)
    {
        ChangeValues();
    }


    private void ChangeValues()
    {
        gameObject.GetComponent<Collider>().enabled = false;
        UIManager.instance.dialogues.StartDialogue(triggerMemory);
        LightManager.instance.OpenLivingRoomEntranceDoorHighLight(false);
        Destroy(this);
        Destroy(gameObject);
        //light to the door end of fungus
    }

    
}
