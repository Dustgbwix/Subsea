using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private bool inWater = false;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float Swim = 5f;
    [SerializeField] private float jump = 5f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (inWater == true)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, Swim, rb.linearVelocity.z);
            }
            else
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            }
            if (Input.GetKey(KeyCode.LeftControl))
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, -Swim, rb.linearVelocity.z);
            }
            else
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                moveSpeed = 9f;
            }
            else
            {
                moveSpeed = 5f;
            }
        }
        if (inWater == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(Vector3.up * jump, ForceMode.Impulse);
            }
            if (Input.GetKey(KeyCode.LeftControl))
            {
                moveSpeed = 2f;
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                moveSpeed = 9f;
            }
            else
            {
                moveSpeed = 5f;
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            }
        }
        float X = Input.GetAxis("Horizontal");
        float Z = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(X, 0f, Z);
        rb.linearVelocity = new Vector3(movement.x * moveSpeed, rb.linearVelocity.y, movement.z * moveSpeed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("water"))
        {
            inWater = true;
            rb.useGravity = false;
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("water"))
        {
            rb.useGravity = true;
            inWater = false;
        }
    }

}
