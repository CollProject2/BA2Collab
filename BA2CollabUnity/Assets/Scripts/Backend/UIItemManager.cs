using System.Collections.Generic;
using UnityEngine;

public class UIItemManager : MonoBehaviour
{
    private ItemManager itemManager;
    public static UIItemManager instance;

    public List<GameObject> activeUIObjects;
    public List<GameObject> defaultUIObjects;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        itemManager = ItemManager.instance;
        UpdateItemImage();
    }

    public void CollectItem(InteractableItem item)
    {
        itemManager.CollectItem(item);
        UpdateItemImage();
    }

    public void UseItem(InteractableItem item)
    {
        itemManager.UseItem(item);
        UpdateItemImage();
    }

    private void UpdateItemImage()
    {
        for (int i = 0; i < itemManager.items.Count; i++)
        {
            var item = itemManager.items[i];

            var activeObject = activeUIObjects[i];
            var defaultObject = defaultUIObjects[i];

            bool isItemCollected = itemManager.IsItemCollected(item);
            bool isItemUsed = itemManager.IsItemUsed(item);

            activeObject.SetActive(isItemCollected && !isItemUsed);
            defaultObject.SetActive(!isItemCollected || isItemUsed);
        }
    }


}
