using UnityEngine;
using UnityEngine.SceneManagement;

public class EggController : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;

    private bool isBreaking = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (ShouldRestart(collision.gameObject))
        {
            BreakEgg();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (ShouldRestart(other.gameObject))
        {
            BreakEgg();
        }
    }

    private void BreakEgg()
    {
        if (isBreaking) return;
        isBreaking = true;

        if (explosionPrefab != null)
        {
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            // Try to play the particle system manually
            ParticleSystem ps = explosion.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                ps.Play();
            }
        }

        Destroy(gameObject); // destroy egg object

        RestartLevel();
    }

    private bool ShouldRestart(GameObject obj)
    {
        return obj.GetComponent<RedBirdController>() != null ||
               obj.GetComponent<BoxController>() != null ||
               obj.GetComponent<PigController>() != null;
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}