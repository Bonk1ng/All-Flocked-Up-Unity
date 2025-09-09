using UnityEngine;

public class EXPSeed : MonoBehaviour
{
    [SerializeField] private GameObject playerRef;
    [SerializeField] private int value;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerRef = other.gameObject;
            EXPConsume();
            Destroy(gameObject);

        }
    }

    private void EXPConsume()
    {
        //Add stamina when completed
    }
}
