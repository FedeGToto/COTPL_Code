using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public bool IsRolling => rb.linearVelocity != Vector3.zero && rb.angularVelocity != Vector3.zero;

    [SerializeField] private Vector3 startPosition;
    [SerializeField] private float maxLanuchForce = 500;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Shoot()
    {
        rb.isKinematic = true;
        float dirX = Random.Range(0, maxLanuchForce);
        float dirY = Random.Range(0, maxLanuchForce);
        float dirZ = Random.Range(0, maxLanuchForce);

        transform.SetPositionAndRotation(startPosition, Quaternion.identity);

        rb.isKinematic = false;
        rb.AddForce(transform.up * maxLanuchForce);
        rb.AddTorque(dirX, dirY, dirZ);
    }
}
