using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PalyerPickUp : MonoBehaviour
{
    public PickUp curPickUp;
    public Transform PickUpParent;

    public Transform dropPosition;

    private PlayerController controller;
    private PlayerCondition condition;

    void Start()
    {
        controller = PlayerManager.Instance.Player.controller;
        condition = PlayerManager.Instance.Player.condition;
        dropPosition = PlayerManager.Instance.Player.dropPosition;
    }


    public void PickUpNew(ItemData data)
    {
        ThrowPickUp();
        curPickUp = Instantiate(data.PickUpPrefeb, PickUpParent).GetComponent<PickUp>();
    }

    public void ThrowPickUp()
    {
        if(curPickUp != null)
        {
            Destroy(curPickUp.gameObject);
            curPickUp = null;
        }
    }

    public void ThrowItem(ItemData data)
    {
        Instantiate(data.dropPrefab, dropPosition.position, Quaternion.Euler(Vector3.forward));
    }

    public void OnUse(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && curPickUp != null)
        {
            if (PlayerManager.Instance.Player.itemData.type == ItemType.CanUse)
            {
                switch (PlayerManager.Instance.Player.itemData.consumables.type)
                {
                    case ConsumableType.Health:
                        condition.Heal(PlayerManager.Instance.Player.itemData.consumables.value);
                        ThrowPickUp();
                        PlayerManager.Instance.Player.itemData = null;
                        break;
                }
            }
        }
    }
}
