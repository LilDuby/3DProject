using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float jumpPower;
    private Vector2 curMovementInput;
    public LayerMask groundLayerMask;

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook; // 회전범위
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity;
    private Vector2 mouseDelta;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        // 커서 숨기기
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate() // 물리 연산은 그냥 Update보다 FixedUpdate
    {
        Move();
    }

    private void LateUpdate()
    {
        CameraLook();
    }

    void Move() // 실질적으로 이동을 시켜주는 함수
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x; // forward (앞w 뒤s) right (좌a 우d)방향값 설정
        // Vector2 에서 받은 y값을 Vector3의 z값에 넣어주면 Input Syetem을 3D에 사용 가능하다
        // 2D 조작을 3D에서 시점을 위에서 보고있는 상태로 똑같이 조작 한다고 생각
        // 2D에서 위 아래로 움직이는 것이 3D에서는 앞 뒤, 2D에서 위아래는 y 3D에서 앞 뒤는 z, 따라서 입력받은 Vector2의 y를 vector3의 z에 넣어주면 3D에서 이동 기능 구현됨
        dir *= moveSpeed;
        dir.y = _rigidbody.velocity.y; // 평소의 y값으로 유지 ( 점프x일때 ), 3D에서는 y값이 위 아래

        _rigidbody.velocity = dir; // rigidbody에 값을 넣어 질량과 관성을 무시하고 움직이게 만든다, 저항이 없어 속도가 동일
    }

    void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook); // 최솟값보다 작아지면 최솟값 반환 최댓값보다 커지면 최댓값 반환
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0); // 마우스 이동과 시야의 움직임이 반대라서 -를 붙인다 // 카메라의 x축을 돌려 시야의 위 아래 조정

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0); // 캐릭터 y축을 돌려 시야의 좌 우 조정
    }

    public void OnMove(InputAction.CallbackContext context) // 현재 상태를 받아온다 // Player Input의 Events에 등록할 함수
    {
        if(context.phase == InputActionPhase.Performed) // 키가 눌려있는 상태일때
        {
            curMovementInput = context.ReadValue<Vector2>(); // context의 값을 받아온다
        }
        else if(context.phase == InputActionPhase.Canceled) // 눌린 키가 떨어졌을때
        {
            curMovementInput = Vector2.zero; // 아무것도 안들어간다
        }
    }

    public void OnLook(InputAction.CallbackContext context) // Player Input의 Events에 등록할 함수
    {
        mouseDelta = context.ReadValue<Vector2>(); // 마우스는 phase가 없기 때문에 값을 받기만 하면 됨
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started && IsGrounded())
        {
            _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
        }
    }

    bool IsGrounded() // 플레이어가 바닥에 닿았는지 확인
    {
        Ray[] rays = new Ray[4] // 플레이어를 기준으로 책상 다리처럼 4개를 만들어줄 예정
        {
            // 각 Ray의 시작점을 정해줌 그러고 방향 정해줌
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down)
        };

        for(int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask)) // 0.1f 길이의 Ray를 쏴서 groundLayerMask에 해당하는 애들만 검출
            {
                return true;
            }
        }

        return false;
    }
}
