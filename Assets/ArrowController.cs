using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] float timeStep = 0.1f;
    [SerializeField] int numberOfArrows = 30;
    [SerializeField] float gravity = 9.81f;

    Rigidbody2D birdRigidbody;
    RedBirdController birdController;
    GameObject[] arrows;

    void Start()
    {
        birdController = FindObjectOfType<RedBirdController>();
        birdRigidbody = birdController.GetComponent<Rigidbody2D>();
        arrows = new GameObject[numberOfArrows];

        // Instantiate arrows at the start, but deactivate them
        for (int i = 0; i < numberOfArrows; i++)
        {
            arrows[i] = Instantiate(arrowPrefab);
            arrows[i].SetActive(false);
        }
    }

    void Update()
    {
        if (birdRigidbody.isKinematic) // Only show arrows while the bird is being dragged
        {
            ShowTrajectory();
        }
        else
        {
            HideArrows(); // Hide arrows once the bird is launched
        }
    }

    void ShowTrajectory()
    {
        Vector2 startPosition = birdRigidbody.position;
        Vector2 launchDirection = birdController.GetLaunchDirection(); // Get the launch direction
        float launchPower = birdController.GetLaunchPower(); // Get the launch power
        Vector2 velocity = launchDirection * launchPower; // Calculate the velocity

        Vector2 previousPoint = startPosition;

        for (int i = 0; i < numberOfArrows; i++)
        {
            float time = i * timeStep; // Time step for each arrow
            Vector2 position = startPosition + velocity * time + 0.5f * new Vector2(0, -gravity) * time * time; // Calculate position

            arrows[i].transform.position = position; // Set position of the arrow

            // Calculate the direction of the trajectory for this arrow
            Vector2 direction = position - previousPoint;

            // Calculate the angle of the arrow relative to the horizontal axis
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Apply the correct rotation to the arrow
            arrows[i].transform.rotation = Quaternion.Euler(0, 0, angle);

            arrows[i].SetActive(true); // Activate the arrow

            previousPoint = position; // Update previous point for next arrow
        }
    }

    void HideArrows()
    {
        // Hide all arrows once the bird is launched
        for (int i = 0; i < arrows.Length; i++)
        {
            arrows[i].SetActive(false);
        }
    }
}