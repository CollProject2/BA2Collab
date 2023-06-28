using System.Collections.Generic;
using UnityEngine;

public class ItemUIManager : MonoBehaviour
{
    public List<GameObject> itemsUI;
    public static ItemUIManager Instance;  // Singleton instance

    private Dictionary<int, GameObject> items = new Dictionary<int, GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        InitializeItemDictionary();
    }

    public void InitializeItemDictionary()
    {
        for (int i = 0; i < itemsUI.Count; i++)
        {
            items.Add(i, itemsUI[i]);
        }
    }

    public void ToggleItem(int key)
    {
        // Check if the key exists in the dictionary
        if (items.ContainsKey(key))
            // Toggle the item's active state
            items[key].SetActive(!items[key].activeInHierarchy);
        
    }
}
