using UnityEngine;

public class PigController : MonoBehaviour
{
    [SerializeField] private GameObject cloudParticlePrefab;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        RedBirdController bird = collision.collider.GetComponent<RedBirdController>();
        if (bird != null)
        {
            Instantiate(cloudParticlePrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            return;
        }

        PigController enemy = collision.collider.GetComponent<PigController>();
        if (enemy != null)
        {
            return;
        }

        if (collision.contacts[0].normal.y < -0.5)
        {
            Instantiate(cloudParticlePrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }
}
