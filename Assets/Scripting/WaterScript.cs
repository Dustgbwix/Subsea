using UnityEngine;

public class WaterScript : MonoBehaviour
{
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("water"))
        {
            rb.useGravity = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("water"))
        {
            rb.useGravity = true;
        }
    }
}
