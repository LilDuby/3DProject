using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public enum ItemType
{
    JustPickUp,
    CanUse,
    Resource
}

public enum ConsumableType
{
    Health,
    Buff
}

[Serializable]
public class ItemDataConsumable
{
    public ConsumableType type;
    public float value;
    public float time;
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
    public ItemType type;
    public GameObject dropPrefab;

    [Header("Consumable")]
    public ItemDataConsumable consumables;

    [Header("PickUp")]
    public GameObject PickUpPrefeb;
}
