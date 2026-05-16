using UnityEngine;
using UnityEngine.SceneManagement;

public class doortopod : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("pod");
        }
    }
}
