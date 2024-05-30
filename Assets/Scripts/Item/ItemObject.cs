using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public string GetInteractPrompt();
    public bool OnInteract();
}

public class ItemObject : MonoBehaviour, IInteractable
{

    public ItemData data;

    public string GetInteractPrompt()
    {
        string str = $"{data.displayName}\n{data.description}";
        return str;
    }

    public bool OnInteract()
    {
        if(data.type != ItemType.Resource)
        {
            PlayerManager.Instance.Player.itemData = data;
            PlayerManager.Instance.Player.PickUpItem?.Invoke();
            Destroy(gameObject);
            PlayerManager.Instance.Player.pickUp.PickUpNew(data);
            return true;
        }
        return false;
    }
}
