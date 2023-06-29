using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warning : MonoBehaviour
{
    public string warningName;
    private void OnTriggerEnter(Collider other)
    {
        UIManager.instance.dialogues.StartDialogue(warningName);
        Destroy(this);
    }
}
