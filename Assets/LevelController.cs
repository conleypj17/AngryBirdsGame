using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private static int nextLevelIndex = 1;
    private PigController[] enemies;
    private void OnEnable()
    {
        enemies = FindObjectsOfType<PigController>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (PigController enemy in enemies)
        {
            if (enemy != null)
            {
                return;
            }
        }

        Debug.Log("You killed all enemies");

        //load the next level
        nextLevelIndex++;
        string nextLevelName = "Level" + nextLevelIndex;
        SceneManager.LoadScene(nextLevelName);
    }
}
