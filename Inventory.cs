using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    // hyper-simple singleton pattern (static = shared by all instances of this class)
    public static Inventory instance;

    // Set instance equal to this particular Inventory component
    // YOU SHOULD ONLY HAVE ONE INVENTORY AT ALL TIMES
    void Awake() {

        if (instance != null) {
            Debug.LogWarning("More than one Inventory found!");
            return;
        }

        instance = this;
    }

    // delegate
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    // limit space
    public int space = 12;
    
    public List<Item> items = new List<Item>();

    public bool Add(Item item) {
        if (!item.isDefaultItem) {
            
            if (items.Count >= space) {
                Debug.Log("Not enough room to add item.");
                return false;;
            }

            items.Add(item);
            
            if (onItemChangedCallback != null) {
                onItemChangedCallback.Invoke();
            }
            
        }

        return true;

    }

    public void Remove(Item item) {
        items.Remove(item);

        if (onItemChangedCallback != null) {
            onItemChangedCallback.Invoke();
        }
    }

}
