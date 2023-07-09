using System.Collections.Generic;
using UnityEngine;

public class UIItemManager : MonoBehaviour
{
    
    public static UIItemManager instance = null;

    public List<GameObject> activeUIObjects;
    public List<GameObject> defaultUIObjects;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        
        if (ItemManager.instance != null)
            UpdateItemImage();
    }

    public void CollectItem(InteractableItem item)
    {
        ItemManager.instance.CollectItem(item);
        UpdateItemImage();
    }

    public void UseItem(InteractableItem item)
    {
        ItemManager.instance.UseItem(item);
        UpdateItemImage();
    }

    private void UpdateItemImage()
    {
        for (int i = 0; i < ItemManager.instance.items.Count; i++)
        {
            var item = ItemManager.instance.items[i];

            var activeObject = activeUIObjects[i];
            var defaultObject = defaultUIObjects[i];

            bool isItemCollected = ItemManager.instance.IsItemCollected(item);
            bool isItemUsed = ItemManager.instance.IsItemUsed(item);

            activeObject.SetActive(isItemCollected && !isItemUsed);
            defaultObject.SetActive(!isItemCollected || isItemUsed);
        }
    }


}
