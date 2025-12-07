using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerPerchSystem : MonoBehaviour
{
    [SerializeField] private LayerMask perchLayer;
    [SerializeField] private I_Perchable currentPerchPoint;
    [SerializeField] private UI_PerchPrompt prompt;
    [SerializeField] private UI_PerchPrompt promptPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 10f, perchLayer))
        {
            hit.collider.TryGetComponent<I_Perchable>(out currentPerchPoint);
            switch (currentPerchPoint)
            {
                case PerchableObject_Tree:
                    ShowPrompt("Tree");
                    break;
                case PerchableObject_Bush:
                    ShowPrompt("Bush");
                    break;
                case PerchableObject_General:
                    ShowPrompt(hit.collider.name);
                    break;
            }

        }
        else HidePrompt();
    }

    private void ShowPrompt(string obj)
    {
        prompt = Instantiate<UI_PerchPrompt>(promptPrefab);
        prompt.promptText.SetText(obj);
    }

    private void HidePrompt()
    {
        Destroy(prompt);
    }


}
