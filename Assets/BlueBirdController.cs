using UnityEngine;
using System.Collections;

public class BlueBirdController : MonoBehaviour
{
    [SerializeField] private float launchPower = 500f;
    [SerializeField] private float splitForce = 300f;
    [SerializeField] private GameObject splitBirdPrefab;

    private Rigidbody2D rb;
    private LineRenderer lr;

    private Vector2 startPosition;
    private Vector3 initialPosition;

    private bool isDragging = false;
    private bool wasLaunched = false;
    private bool hasSplit = false;
    private float timeSittingAround = 0f;
    private bool sceneResetQueued = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();

        if (lr == null)
        {
            lr = gameObject.AddComponent<LineRenderer>();
            lr.positionCount = 2;
            lr.startWidth = 0.05f;
            lr.endWidth = 0.05f;
            lr.material = new Material(Shader.Find("Sprites/Default"));
            lr.startColor = Color.white;
            lr.endColor = Color.white;
            lr.enabled = false;
        }

        initialPosition = transform.position;
        startPosition = rb.position;
        rb.isKinematic = true;
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0;
            rb.position = mouseWorldPos;

            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, initialPosition);
        }

        if (wasLaunched && rb.linearVelocity.magnitude <= 0.1f)
        {
            timeSittingAround += Time.deltaTime;
        }

        if (!sceneResetQueued && (
            transform.position.y > 40 || transform.position.y < -40 ||
            transform.position.x > 65 || transform.position.x < -20 ||
            timeSittingAround > 3))
        {
            sceneResetQueued = true;
            StartCoroutine(ReloadSceneAfterDelay(0.2f));
        }

        if (wasLaunched && !hasSplit && Input.GetKeyDown(KeyCode.Space))
        {
            Split();
        }
    }

    private void OnMouseDown()
    {
        isDragging = true;
        lr.enabled = true;
    }

    private void OnMouseUp()
    {
        isDragging = false;
        rb.isKinematic = false;
        Vector2 launchDirection = startPosition - rb.position;
        rb.AddForce(launchDirection * launchPower);
        rb.gravityScale = 1;
        wasLaunched = true;
        lr.enabled = false;
    }

    void Split()
    {
        if (hasSplit) return;
        hasSplit = true;

        Vector2 velocity = rb.linearVelocity; // Changed from linearVelocity to velocity

        for (int i = -1; i <= 1; i += 2)
        {
            Vector3 spawnPos = transform.position + new Vector3(i * 0.5f, 0.5f, 0);
            GameObject clone = Instantiate(splitBirdPrefab, spawnPos, Quaternion.identity);

            // Make sure the clone is in the same layer and has the same tag if needed
            clone.layer = gameObject.layer;
            clone.tag = gameObject.tag;

            Rigidbody2D cloneRb = clone.GetComponent<Rigidbody2D>();
            if (cloneRb != null)
            {
                cloneRb.linearVelocity = velocity;
                cloneRb.gravityScale = 1;
                cloneRb.AddForce(new Vector2(i * 0.5f, 1f) * splitForce, ForceMode2D.Impulse);
                cloneRb.freezeRotation = true;

            }
        }

        Destroy(gameObject); // Destroy immediately instead of waiting for frame end
    }

    private IEnumerator DestroyAfterFrame()
    {
        yield return new WaitForEndOfFrame();
        Destroy(gameObject);
    }

    private IEnumerator ReloadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
