using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [SerializeField] GameObject arrowPrefab;   // The arrow symbol (>)
    [SerializeField] float timeStep = 0.1f;    // Time step for each arrow
    [SerializeField] int numberOfArrows = 30;  // Number of arrows to show along the path
    [SerializeField] float gravity = 9.81f;    // Gravity value

    Rigidbody2D birdRigidbody; // Reference to the bird's rigidbody
    GameObject[] arrows;       // Array to store the arrows

    void Start()
    {
        birdRigidbody = FindObjectOfType<RedBirdController>().GetComponent<Rigidbody2D>();
        arrows = new GameObject[numberOfArrows];  // Create an array for arrows

        // Instantiate all arrows at the start, but disable them for now
        for (int i = 0; i < numberOfArrows; i++)
        {
            arrows[i] = Instantiate(arrowPrefab);
            arrows[i].SetActive(false); // Hide arrows until needed
        }
    }

    void Update()
    {
        if (birdRigidbody.isKinematic)  // Only show arrows while dragging the bird
        {
            ShowTrajectory();
        }
        else
        {
            HideArrows();  // Hide arrows once the bird is launched
        }
    }

    void ShowTrajectory()
    {
        Vector2 startPosition = birdRigidbody.position;
        Vector2 launchDirection = FindObjectOfType<RedBirdController>().GetLaunchDirection();
        float launchPower = FindObjectOfType<RedBirdController>().GetLaunchPower();
        Vector2 velocity = launchDirection * launchPower;

        Vector2 previousPoint = startPosition;

        for (int i = 0; i < numberOfArrows; i++)
        {
            float time = i * timeStep;
            Vector2 position = startPosition + velocity * time + 0.5f * new Vector2(0, -gravity) * time * time;

            arrows[i].transform.position = position;

            Vector2 direction = position - previousPoint;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            arrows[i].transform.rotation = Quaternion.Euler(0, 0, angle);

            arrows[i].SetActive(true);

            previousPoint = position;
        }
    }

    void HideArrows()
    {
        // Disable all arrows once the bird is launched
        for (int i = 0; i < arrows.Length; i++)
        {
            arrows[i].SetActive(false);
        }
    }
}
