using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float PadPower;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Rigidbody _rigidbody))
        {
            _rigidbody.AddForce(Vector2.up * PadPower, ForceMode.Impulse);
        }
    }
}
