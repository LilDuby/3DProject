using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    public float curValue;
    public float startValue;
    public float maxValue;
    public float passiveValue;
    public Image uiBar;

    void Start()
    {
        curValue = startValue;
    }

    void Update()
    {
        uiBar.fillAmount = GetPercentage();
        // 상황에 따라 curValue를 조절해주면된다
    }

    float GetPercentage() // fillAmount를 조절하기 위한 값 반환
    {
        // fillAmount는 0 ~ 1의 값을 가진다 현재체력에 최대체력을 나누면 최대가 1인 값이 나온다
        return curValue / maxValue;
    }

    public void Plus(float value) // 체력을 회복할 때 호출, 회복할 채력량 = value
    {
        curValue = Mathf.Min(curValue + value, maxValue);
        // Mathf.Min은 둘중 작은 값을 반환
        // 최대 체력을 넘어서 회복이 불가능하게 만들어준다
    }

    public void Subtract(float value) // 데미지를 입을 때 호출, 입을 데미지 = value
    {
        curValue = Mathf.Max(curValue - value, 0);
        // Mathf.Max는 둘중 큰 값을 반환
        // 데미지를 입었을 때 체력이 0보다 작아지지 않도록 만들어준다
    }
}
