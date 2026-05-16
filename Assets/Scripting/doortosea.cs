using UnityEngine;
using UnityEngine.SceneManagement;

public class doortosea : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("sea");
        }
    }
}
