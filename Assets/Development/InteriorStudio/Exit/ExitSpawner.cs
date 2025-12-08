using UnityEngine;

public class ExitSpawner : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject promptPrefab;
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private Vector3 startLocation;
    private bool isPromptShown;
    private bool isTeleporting;

    private void Start()
    {

    }

    private void Update()
    {
        if (isPromptShown)
        {
            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                if (!isTeleporting)
                {
                    GoToStartLocation();
                }
                else return;
            }
        }
        else return;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject;
            if (player != null)
            {
                ShowPrompt();
            }
        }
    }

    private void ShowPrompt()
    {
        //spawn prompt
        isPromptShown = true;
    }

    private void HidePrompt()
    {
        isPromptShown = false;
    }

    private void GoToStartLocation()
    {
        isTeleporting = true;
        player.transform.position = startLocation;
    }
}
