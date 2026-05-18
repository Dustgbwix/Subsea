using UnityEngine;
using UnityEngine.SceneManagement;  

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private bool inWater = false;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float Swim = 5f;
    [SerializeField] private float jump = 5f;
    [SerializeField] private float HighScore = 0f;
    [SerializeField] private float Score = 0f;
    float xRotation = 0f;
    [SerializeField] private float MouseSensitivity = 100f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        float HighScore = PlayerPrefs.GetFloat("HighScore", 0f);
        Debug.Log("High Score is: " + HighScore);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Cursor.visible = true;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (inWater == true)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, Swim, rb.linearVelocity.z);
            }
            else if (Input.GetKey(KeyCode.LeftControl))
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
        float mouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;
        float X = Input.GetAxis("Horizontal");
        float Z = Input.GetAxis("Vertical");
        Vector3 movement = transform.right * X + transform.forward * Z;
        transform.Rotate(0f, mouseX * MouseSensitivity * Time.deltaTime, 0f);
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        rb.linearVelocity = new Vector3(movement.x * moveSpeed, rb.linearVelocity.y, movement.z * moveSpeed);
    }
    private void OnCollisionEnter(Collision others)
    {
        if (others.gameObject.CompareTag("enemy"))
        {
            Time.timeScale = 0f;
            Debug.Log("you died! Final Score was: " + Score);
        }
        if (others.gameObject.CompareTag("saver"))
        {
            if (Score > HighScore)
            {
                HighScore = Score;
                PlayerPrefs.SetFloat("HighScore", Score);
                PlayerPrefs.Save();
            }
        }
        if (others.gameObject.CompareTag("collector"))
        {
            Debug.Log("Score is: " + Score);
            Score += 1f;
            Destroy(others.gameObject);
        }
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
