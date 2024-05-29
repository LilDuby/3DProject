using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // ��ɵ��� ������ ���� ��´�

    public PlayerController controller;
    public PlayerCondition condition;

    public ItemData itemData;
    public Action addItem;
    private void Awake()
    {
        PlayerManager.Instance.Player = this;
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();
    }
}