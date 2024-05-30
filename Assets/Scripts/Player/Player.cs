using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // ��ɵ��� ������ ���� ��´�

    public PlayerController controller;
    public PlayerCondition condition;
    public PalyerPickUp pickUp;

    public ItemData itemData;
    public Action PickUpItem;

    public Transform dropPosition;
    private void Awake()
    {
        PlayerManager.Instance.Player = this;
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();
        pickUp = GetComponent<PalyerPickUp>();
    }
}
