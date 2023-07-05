using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
[CommandInfo("puzzle","moveMedallionPuzzleAway","moves puzzle out of screen")]
public class MoveMedallionPuzzleAway : Command
{
    public MedallionItem medallionItem;
    public GiftCardItem giftCardItem;
    public override void OnEnter()
    {
        medallionItem.MoveMedallionAway();
        giftCardItem.SetInteractable(true);
        Continue();
    }
}
