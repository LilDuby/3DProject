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
    public float minXLook; // ȸ������
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
        // Ŀ�� �����
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate() // ���� ������ �׳� Update���� FixedUpdate
    {
        Move();
    }

    private void LateUpdate()
    {
        CameraLook();
    }

    void Move() // ���������� �̵��� �����ִ� �Լ�
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x; // forward (��w ��s) right (��a ��d)���Ⱚ ����
        // Vector2 ���� ���� y���� Vector3�� z���� �־��ָ� Input Syetem�� 3D�� ��� �����ϴ�
        // 2D ������ 3D���� ������ ������ �����ִ� ���·� �Ȱ��� ���� �Ѵٰ� ����
        // 2D���� �� �Ʒ��� �����̴� ���� 3D������ �� ��, 2D���� ���Ʒ��� y 3D���� �� �ڴ� z, ���� �Է¹��� Vector2�� y�� vector3�� z�� �־��ָ� 3D���� �̵� ��� ������
        dir *= moveSpeed;
        dir.y = _rigidbody.velocity.y; // ����� y������ ���� ( ����x�϶� ), 3D������ y���� �� �Ʒ�

        _rigidbody.velocity = dir; // rigidbody�� ���� �־� ������ ������ �����ϰ� �����̰� �����, ������ ���� �ӵ��� ����
    }

    void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook); // �ּڰ����� �۾����� �ּڰ� ��ȯ �ִ񰪺��� Ŀ���� �ִ� ��ȯ
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0); // ���콺 �̵��� �þ��� �������� �ݴ�� -�� ���δ� // ī�޶��� x���� ���� �þ��� �� �Ʒ� ����

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0); // ĳ���� y���� ���� �þ��� �� �� ����
    }

    public void OnMove(InputAction.CallbackContext context) // ���� ���¸� �޾ƿ´� // Player Input�� Events�� ����� �Լ�
    {
        if(context.phase == InputActionPhase.Performed) // Ű�� �����ִ� �����϶�
        {
            curMovementInput = context.ReadValue<Vector2>(); // context�� ���� �޾ƿ´�
        }
        else if(context.phase == InputActionPhase.Canceled) // ���� Ű�� ����������
        {
            curMovementInput = Vector2.zero; // �ƹ��͵� �ȵ���
        }
    }

    public void OnLook(InputAction.CallbackContext context) // Player Input�� Events�� ����� �Լ�
    {
        mouseDelta = context.ReadValue<Vector2>(); // ���콺�� phase�� ���� ������ ���� �ޱ⸸ �ϸ� ��
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started && IsGrounded())
        {
            _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
        }
    }

    bool IsGrounded() // �÷��̾ �ٴڿ� ��Ҵ��� Ȯ��
    {
        Ray[] rays = new Ray[4] // �÷��̾ �������� å�� �ٸ�ó�� 4���� ������� ����
        {
            // �� Ray�� �������� ������ �׷��� ���� ������
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down)
        };

        for(int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask)) // 0.1f ������ Ray�� ���� groundLayerMask�� �ش��ϴ� �ֵ鸸 ����
            {
                return true;
            }
        }

        return false;
    }
}
