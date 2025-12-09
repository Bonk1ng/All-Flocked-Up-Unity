using UnityEngine;

public class EntranceSpawner : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private UI_InteriorPrompt promptPrefab;
    [SerializeField] private UI_InteriorPrompt currentPrompt;
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private Vector3 endLocation;
    public string locationName;
    private bool isPromptShown;
    private bool isTeleporting;

    private void Start()
    {

    }

    private void Update()
    {
       if(isPromptShown)
        {
            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                if (!isTeleporting)
                {
                    GoToEndLocation();
                }
                else return;
            }
        }else return;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject;
            if(player != null)
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

    private void GoToEndLocation()
    {
        isTeleporting = true;
        player.transform.position = endLocation;
        HidePrompt();
    }
}
