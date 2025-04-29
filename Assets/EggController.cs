using UnityEngine;
using UnityEngine.SceneManagement; // Needed to reload the scene

public class EggController : MonoBehaviour
{
    // Called when the egg collides with another collider (with Rigidbody2D involved)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        RestartLevel();
    }

    // Optionally, if you are using triggers instead of collisions, use this:
    private void OnTriggerEnter2D(Collider2D other)
    {
        RestartLevel();
    }

    private void RestartLevel()
    {
        // Reloads the current active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}