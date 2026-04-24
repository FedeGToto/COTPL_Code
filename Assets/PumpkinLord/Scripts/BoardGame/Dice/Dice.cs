using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public bool IsRolling => rb.linearVelocity.magnitude > 0.01f && rb.angularVelocity.magnitude > 0.01f;

    [SerializeField] private Vector3 startPosition;
    [SerializeField] private float maxLaunchForce = 500;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Shoot()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        Quaternion randomRotation = Random.rotation;
        transform.SetPositionAndRotation(startPosition, randomRotation);

        rb.AddForce(Vector3.up * maxLaunchForce, ForceMode.Impulse);

        float dirX = Random.Range(-maxLaunchForce, maxLaunchForce);
        float dirY = Random.Range(-maxLaunchForce, maxLaunchForce);
        float dirZ = Random.Range(-maxLaunchForce, maxLaunchForce);
        rb.AddTorque(dirX, dirY, dirZ, ForceMode.Impulse);
    }
}
