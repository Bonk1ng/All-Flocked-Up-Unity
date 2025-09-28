using UnityEngine;
using UnityEngine.UI;

public class AccessoryPanelCanvas : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        closeButton.onClick.AddListener(CloseAccessoryPanel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CloseAccessoryPanel()
    {
        Destroy(this.gameObject);
    }
}
