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
        // ��Ȳ�� ���� curValue�� �������ָ�ȴ�
    }

    float GetPercentage() // fillAmount�� �����ϱ� ���� �� ��ȯ
    {
        // fillAmount�� 0 ~ 1�� ���� ������ ����ü�¿� �ִ�ü���� ������ �ִ밡 1�� ���� ���´�
        return curValue / maxValue;
    }

    public void Plus(float value) // ü���� ȸ���� �� ȣ��, ȸ���� ä�·� = value
    {
        curValue = Mathf.Min(curValue + value, maxValue);
        // Mathf.Min�� ���� ���� ���� ��ȯ
        // �ִ� ü���� �Ѿ ȸ���� �Ұ����ϰ� ������ش�
    }

    public void Subtract(float value) // �������� ���� �� ȣ��, ���� ������ = value
    {
        curValue = Mathf.Max(curValue - value, 0);
        // Mathf.Max�� ���� ū ���� ��ȯ
        // �������� �Ծ��� �� ü���� 0���� �۾����� �ʵ��� ������ش�
    }
}
