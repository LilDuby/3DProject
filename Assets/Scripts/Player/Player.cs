using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 기능들의 정보를 여기 담는다

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
