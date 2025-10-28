using UnityEngine;


[System.Serializable]
public class Item
{
    public string name;
    public string itemType;

    public Item(string name, string itemType)
    {
        this.name = name;
        this.itemType = itemType;
    }
    
}

