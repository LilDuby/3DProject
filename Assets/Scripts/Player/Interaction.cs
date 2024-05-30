using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

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
        if(Time.time - lastCheckTime > checkRate) // ray를 계속 쏘지 않기 위해
        {
            lastCheckTime = Time.time;


            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2)); // ray가 나가는 시작점, 정가운대
            RaycastHit hit; // ray에 부딧힌 오브젝트의 정보를 담아둘 변수

            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask)) // 부딧혔다면 정보를 hit에 반환 인식할 거리는 maxCheckDistance 그 후 LayerMask설정
            {
                if (hit.collider.gameObject != curInteractGameObject) // 이전과 같지 않은 오브젝트를 인식했을 때
                {
                    curInteractGameObject = hit.collider.gameObject;
                    curInteractable = hit.collider.GetComponent<IInteractable>();
                    SetPromptText();
                }
            }
            else // 빈공간에 ray를 쐈을때
            {
                curInteractGameObject = null;
                curInteractable = null;
                promptText.gameObject.SetActive(false);
            }
        }
    }

    private void SetPromptText()
    {
        promptText.gameObject.SetActive(true);
        promptText.text = curInteractable.GetInteractPrompt();
    }

    public void OnInteractPickUp(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started && curInteractable != null && PlayerManager.Instance.Player.itemData == null)
        {
            bool isResource = curInteractable.OnInteract();
            if(isResource)
            {
                curInteractGameObject = null;
                curInteractable = null;
                promptText.gameObject.SetActive(false);
            }
        }
    }

    public void OnInteractThrow(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && PlayerManager.Instance.Player.itemData != null)
        {
            // 아이템 생성
            PlayerManager.Instance.Player.pickUp.ThrowItem(PlayerManager.Instance.Player.itemData);
            // 들고있는 오브젝트 삭제
            PlayerManager.Instance.Player.pickUp.ThrowPickUp();
            PlayerManager.Instance.Player.itemData = null;
        }
    }
}
