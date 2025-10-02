using NUnit.Framework;
using UnityEngine;

public class NestBase : MonoBehaviour
{
    [Header("BaseClassRefs")]
    private GameObject playerRef;
    [SerializeField] private float activeRadius;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRef = FindFirstObjectByType<PlayerGroundMovement>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    protected void ShowNest()
    {

    }

    protected void HideNest()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        ShowNest();
    }

    private void OnTriggerExit(Collider other)
    {
        HideNest();
    }


}
