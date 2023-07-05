using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("puzzle","moveGiftCardAway","moves puzzle out of screen")]

public class MoveGiftCardAway : Command
{
    public GiftCardItem giftCardItem;

    public override void OnEnter()
    {
        giftCardItem.MoveItemAway();
        Continue();
    }

    
}
