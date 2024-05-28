using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    private Vector2 curMovementInput;

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

    void CameraLook() // ���� ĳ���� ���ҽ��� ���� ������ �׳� ī�޶� ������
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
}
