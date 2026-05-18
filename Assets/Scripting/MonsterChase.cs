using UnityEngine;

public class MonsterChase : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    [SerializeField] private float MonsterSpeed = 3f;
    void Update()
    {
        transform.LookAt(player);
        transform.Rotate(0f, 90f, 0f);
        transform.position = Vector3.MoveTowards(transform.position, player.position, MonsterSpeed * Time.deltaTime);
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
