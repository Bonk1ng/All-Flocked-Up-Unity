using UnityEngine;

public class PerchableObject_Tree : MonoBehaviour, I_Perchable
{
    public GameObject playerRef;
    [SerializeField] private bool isPerching;
    [SerializeField] private GameObject[] topHideSpots;
    [SerializeField] private GameObject[] branchPerchSpots;
    [SerializeField] private Vector3 playerOffset  = new Vector3(0,0,0);
    [SerializeField] private SphereCollider perchSphere;
    [SerializeField] private SphereCollider hideColliders;
    [SerializeField] private UI_PerchPrompt currentPrompt;
    [SerializeField] private UI_PerchPrompt promptPrefab;
    [SerializeField] private bool isPromptShown;

    void Update()
    {
        if (isPerching)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StopPerch();
                playerRef.GetComponent<Rigidbody>().linearVelocity = new Vector3(1, 1, 0);
            }
            UpdatePerch();
        }
        else return;
    }

    public void StartPerch()
    {
        isPerching = true;
        playerRef.transform.position = topHideSpots[0].transform.position + playerOffset;
    }

    public void StopPerch()
    {
        isPerching = false;
        ToggleMeshCollidersOn();
    }

    public void UpdatePerch()
    {
        playerRef.transform.position = topHideSpots[0].transform.position+ playerOffset;
    }

    public void MovePosition()
    {

    }

    protected void ShowPrompt()
    {
        if(currentPrompt == null)
        {
            currentPrompt = Instantiate<UI_PerchPrompt>(promptPrefab);
            isPromptShown = true;
        }
    }

    protected void HidePrompt()
    {
        if(currentPrompt != null)
        {
            Destroy(currentPrompt);
            isPromptShown=false;
        }
    }

    protected void ToggleMeshCollidersOn()
    {

        hideColliders.GetComponent<SphereCollider>().isTrigger = true;
        
    }

    protected void ToggleMeshCollidersOff()
    {

        hideColliders.GetComponent<SphereCollider>().isTrigger = false;
        
    }

   void OnTriggerEnter(Collider perchSphere)
    {
        if (perchSphere.gameObject.CompareTag("Player"))
        {
            if (playerRef == null)
            {
                //ToggleMeshCollidersOff();
                playerRef = perchSphere.gameObject;
                //StartPerch();
            }
        }
    }
    void OnCollisionEnter(Collision hideColliders)
    {
        if (hideColliders.gameObject.CompareTag("Player") && playerRef!=null)
        {
            Debug.Log("Hit the Collider");

                ToggleMeshCollidersOff();
                StartPerch();
            
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (playerRef != null)
            {
                playerRef = null;
            }
        }
    }
}
