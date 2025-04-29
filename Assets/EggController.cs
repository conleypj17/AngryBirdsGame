using UnityEngine;
using UnityEngine.SceneManagement;

public class EggController : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (ShouldRestart(collision.gameObject))
        {
            RestartLevel();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (ShouldRestart(other.gameObject))
        {
            RestartLevel();
        }
    }

    private bool ShouldRestart(GameObject obj)
    {
        // Restart if the object has BirdController or BoxController or PigController script
        return obj.GetComponent<RedBirdController>() != null ||
               obj.GetComponent<BoxController>() != null ||
               obj.GetComponent<PigController>() != null;
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}