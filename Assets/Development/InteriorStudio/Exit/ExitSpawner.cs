using UnityEngine;

public class ExitSpawner : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private UI_InteriorPrompt promptPrefab;
    [SerializeField] private UI_InteriorPrompt currentPrompt;
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private Vector3 startLocation;
    private bool isPromptShown;
    private bool isTeleporting;
    public string locationName;

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
        currentPrompt = Instantiate(promptPrefab);
        currentPrompt.prompt = "Travel to " + locationName;
        isPromptShown = true;
    }

    private void HidePrompt()
    {
        Destroy(currentPrompt);
        isPromptShown = false;
    }

    private void GoToStartLocation()
    {
        isTeleporting = true;
        player.transform.position = startLocation;
        HidePrompt();
    }
}
