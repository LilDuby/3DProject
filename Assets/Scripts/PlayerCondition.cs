using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    public UICondition uiCondition;

    Condition health { get { return uiCondition.health; } }


    void Update()
    {
        health.Plus(health.passiveValue * Time.deltaTime);
    }
}
