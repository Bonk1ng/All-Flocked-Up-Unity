using System.Collections;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
enum E_ConsumableType {Health, Stamina,Poop,Custom }
enum E_ReactionType { Health, Stamina,Poop,Speed,Flight,FlySpeed,Custom }
public class ConsumableBase : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Mesh consumableMesh;
    [SerializeField] private GameObject playerRef;
    [Header("States")]
    [SerializeField] private E_ConsumableType consumableType;
    [SerializeField] private bool hasReaction;
    [SerializeField] private E_ReactionType reactionType;
    [Header("Health")]
    [SerializeField] private bool isHealth;
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] private int healthToRegen;
    [SerializeField] private int healthToLose;
    [Header("Stamina")]
    [SerializeField] private bool isStamina;
    [SerializeField]private float staminaToRegen;
    [SerializeField] private float staminaToLose;
    [Header("Poop")]
    [SerializeField] private bool isPoop;
    [SerializeField] private int poopToRegen;
    [SerializeField] private int poopToLose;
    [Header("Custom")]
    [SerializeField] private bool isCustom;
    [Header("ReactionModifiers")]
    [SerializeField] private int healthModifier;
    [SerializeField] private float staminaModifier;
    [SerializeField] private int poopModifier;
    [SerializeField] private float groundSpeedModifier;
    [SerializeField] private bool canFly;
    [SerializeField] private bool canPoop;
    [SerializeField] private float flightSpeedModifier;
    [Header("Particles")]
    [SerializeField] private ParticleSystem consumableParticles;


    private void Awake()
    {
        consumableMesh = GetComponent<MeshFilter>().sharedMesh;
        consumableParticles = GetComponent<ParticleSystem>();
    }

    void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
            
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ConsumableEffect(consumableType);
            Destroy(gameObject);
        }
    }

    private void ConsumableEffect(E_ConsumableType consumableType)
    {
        switch (consumableType)
        {
            case E_ConsumableType.Health:
                isHealth = true;
                playerHealth = playerRef.GetComponent<PlayerHealth>();
                HealEffect(healthToRegen, healthModifier);
                CheckForReaction();
                break;
            case E_ConsumableType.Stamina:
                isStamina = true;
                StaminaEffect(staminaToRegen,staminaModifier);
                CheckForReaction();
                break;
            case E_ConsumableType.Poop:
                isPoop = true;
                PoopEffect(poopToRegen,poopModifier,canPoop);
                CheckForReaction();
                break;
            case E_ConsumableType.Custom:
                isCustom = true;
                CustomEffect(healthToRegen,healthModifier,staminaToRegen,staminaModifier,poopToRegen,poopModifier,groundSpeedModifier,flightSpeedModifier,canFly,canPoop);
                CheckForReaction();
                break;
        }
    }

    private void ReactionEffect(E_ReactionType reactionType)
    {
        switch (reactionType)
        {
            
            case E_ReactionType.Health:
                HealEffect(-healthToLose, -healthModifier);
                break;
            case E_ReactionType.Stamina:
                StaminaEffect(staminaToLose,-staminaModifier);
                break;
            case E_ReactionType.Poop:
                PoopEffect(poopToLose,poopModifier,canPoop);
                break;
            case E_ReactionType.Custom:
                CustomEffect(-healthToLose, -healthModifier,-staminaToLose,-staminaModifier,-poopToLose,-poopModifier,-groundSpeedModifier,-flightSpeedModifier,canFly,canPoop);
                break;
            case E_ReactionType.Speed:
                SpeedEffect(-groundSpeedModifier);
                break;
            case E_ReactionType.Flight:
                FlightEffect(!canFly);
                break;
            case E_ReactionType.FlySpeed:
                FlySpeedEffect(-flightSpeedModifier);
                break;
           

        }
    }

    private void CheckForReaction()
    {
        if (hasReaction)
        {
            ReactionEffect(reactionType);
        }
    }
    private void HealEffect(int health, int modifier)
    {
        playerHealth = playerRef.GetComponent<PlayerHealth>();
        var currenthealth = playerHealth.currentHealth;
        currenthealth += health;
        if (modifier > 0)
        {
            currenthealth *= modifier;
            currenthealth = playerHealth.currentHealth;
            return;
        }
        else
            currenthealth = playerHealth.currentHealth;
    }
    private void StaminaEffect(float stamina, float modifier)
    {
        if (modifier > 0)
        {

        }
        else
            Debug.Log("");
    }
    private void PoopEffect(int poop,int poopMod, bool canPoop)
    {
        if (poopMod > 0)
        {

        }
        else
            Debug.Log("");
    }
    private void CustomEffect(int health, int healthMod, float stamina, float stamMod,int poop, float poopMod,float groundSpeedMod,float flySpeedMod, bool canFly,
                        bool canPoop)
    {
        if (healthMod > 0)
        {

        }
        if (stamMod > 0)
        {

        }
        if (poopMod > 0)
        {

        }
        if (groundSpeedMod > 0)
        {

        }
        if (flySpeedMod > 0)
        {

        }
        if (!canFly)
        {

        }
        if (!canPoop)
        {

        }
        else
            Debug.Log("");
    }
    private void SpeedEffect(float speed)
    {

    }
    private void FlightEffect(bool canFly)
    {

    }
    private void FlySpeedEffect(float flySpeed)
    {

    }
    
}
