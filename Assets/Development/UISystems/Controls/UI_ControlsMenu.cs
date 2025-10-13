using UnityEngine;
using UnityEngine.UI;

public class UI_ControlsMenu : UI_PauseMenu
{
    [SerializeField] private Button closeControlsButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        closeControlsButton.onClick.AddListener(OnControlsOpen);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected new void OnControlsOpen()
    {
        base.OnControlsOpen();
    }
}
