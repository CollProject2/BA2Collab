using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance = null;
    public List<InteractableItem> items;

    public Dictionary<GameObject, bool> itemStatus = new();
    public Dictionary<GameObject, bool> itemUsage = new();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        // Initialize the item status and usage dictionaries
        foreach (var item in items)
        {
            itemStatus[item.gameObject] = false;
            itemUsage[item.gameObject] = false;
        }
    }
    private void LogDictionaryValues()
    {
        foreach (var keyValuePair in itemStatus)
        {
            Debug.Log($"Key: {keyValuePair.Key}, Value: {keyValuePair.Value}");
        }
    }

    public void CollectItem(InteractableItem item)
    {
        if (itemStatus.ContainsKey(item.gameObject))
        {
            itemStatus[item.gameObject] = true;
        }
       // LogDictionaryValues();
    }

    public bool IsItemCollected(InteractableItem item)
    {
        if (itemStatus.ContainsKey(item.gameObject))
        {
            return itemStatus[item.gameObject];
        }

        return false;
    }

    public void UseItem(InteractableItem item)
    {
        if (itemUsage.ContainsKey(item.gameObject) && itemStatus[item.gameObject])
        {
            itemUsage[item.gameObject] = true;
        }
    }

    public bool IsItemUsed(InteractableItem item)
    {
        if (itemUsage.ContainsKey(item.gameObject))
        {
            return itemUsage[item.gameObject];
        }

        return false;
    }
}
