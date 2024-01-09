using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Items/New item")]
public class ItemData : ScriptableObject
{
    public string _name;
    public string description;
    public Sprite visual;
    public GameObject prefab;
    public bool stackable;
    public int maxStack;

    public ItemType itemType;
    public EquipmentType equipmentType;
}

public enum ItemType
{
    Ressource,
    Equipment,
    Consumable,
    Artefact
}

public enum EquipmentType
{
    Head,
    Chest,
    Hands,
    Legs,
    Feet
}
