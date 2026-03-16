using UnityEngine;

public class Observer : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private Transform player;

    [SerializeField] private GameEnding gameEnding;
    private bool playerInRange;


    private void Awake()
    {
        gameEnding = FindFirstObjectByType<GameEnding>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    private void Update()
    {

        if(player == null)
        {
            Debug.LogWarning(transform.name + " is missing a reference to the player.");
            return;
        }
        if (playerInRange)
        {
            Vector3 direction = player.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    Debug.Log("Player detected!");
                    gameEnding.CaughtPlayer();
                }
            }
            
        }
    }

}
