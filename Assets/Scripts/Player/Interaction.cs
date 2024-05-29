using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public float checkRate = 0.05f;
    private float lastCheckTime;
    public float maxCheckDistance;
    public LayerMask layerMask;

    public GameObject curInteractGameObject;
    private IInteractable curInteractable;

    public TextMeshProUGUI promptText;
    private Camera camera;

    void Start()
    {
        camera = Camera.main;
    }


    void Update()
    {
        if(Time.time - lastCheckTime > checkRate) // ray�� ��� ���� �ʱ� ����
        {
            lastCheckTime = Time.time;


            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2)); // ray�� ������ ������, �������
            RaycastHit hit; // ray�� �ε��� ������Ʈ�� ������ ��Ƶ� ����

            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask)) // �ε����ٸ� ������ hit�� ��ȯ �ν��� �Ÿ��� maxCheckDistance �� �� LayerMask����
            {
                if (hit.collider.gameObject != curInteractGameObject) // ������ ���� ���� ������Ʈ�� �ν����� ��
                {
                    curInteractGameObject = hit.collider.gameObject;
                    curInteractable = hit.collider.GetComponent<IInteractable>();
                    SetPromptText();
                }
            }
            else // ������� ray�� ������
            {
                // curInteractGameObject = null;
                // curInteractable = null;
                promptText.gameObject.SetActive(false);
            }
        }
    }

    private void SetPromptText()
    {
        promptText.gameObject.SetActive(true);
        promptText.text = curInteractable.GetInteractPrompt();
    }
}
