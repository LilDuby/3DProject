using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour
{
    public Image image;
    public float flashSpeed;

    private Coroutine coroutine;

    void Start()
    {
        PlayerManager.Instance.Player.condition.onTakeDamage += Flash;
    }

    public void Flash()
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine); // 코루틴이 실행중일 때 Flash 함수 호출 시 기존 코루틴 멈추기
        }

        image.enabled = true;
        image.color = new Color(1f, 100f / 255f, 100f / 255f);
        coroutine = StartCoroutine(FadeAway());
    }

    private IEnumerator FadeAway() // 코루틴 사용시 IEnumerator 사용해야함
    {
        float startAlpha = 0.3f;
        float a = startAlpha;

        while(a > 0)
        {
            a -= (startAlpha / flashSpeed) * Time.deltaTime;
            image.color = new Color(1f, 100f / 255f, 100f / 255f, a);
            yield return null; // 코루틴은 항상 반환이 있어야함
        }

        image.enabled = false;
    }
}
