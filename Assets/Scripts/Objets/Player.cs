using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.Animations;

public class Player : MonoBehaviour
{
    public static Player instance;
    public PlayerData playerData;
    
    PlayerController controller;
    [HideInInspector] public PlayerStateLocation stateLocation;
    [HideInInspector] public PlayerStateAction stateAction;

    //Movement du joueur 
    [HideInInspector] public float movementValue;

    //Gestion du bateau
    public Boat boat;

    //Faut le garder, c'est gérer avec l'animation
    [HideInInspector] public bool goodShoot { get; set; }
    public Transform spawnDecoyTransform;
    [HideInInspector] public FishingRode fishingRode;
    [HideInInspector] public List<Fish> fishCaughtList;
    [HideInInspector] public List<InteractableObject> interactableObjectNearList;

    //Other
    public CameraGame cameraGame;
    public GameObject barLaunchFishingRode;
    [SerializeField] bool resetValue;


    //Struggle
    public CursorStruggle cursorStruggle;

    //Components
    [HideInInspector] public SpriteRenderer spriteRenderer;
    [HideInInspector] public Animator animator;
    [HideInInspector] public Rigidbody2D rigidbody;

    public Transform carryFishTransfrom;





    private void Awake()
    {
        SetComponents();
        
        if (instance == null)
        {
            instance = this;
        }
        SetStateLocation(new PlayerOnGround(this));
        SetStateAction(new PlayerExplore(this));
        fishCaughtList = new List<Fish>();
        interactableObjectNearList = new List<InteractableObject>();
        controller = new PlayerController();
        controller.Player.Move.performed += ctx =>
        {
            stateLocation.MovePerformed(ctx);
        };
        controller.Player.Move.canceled += ctx =>
        {
            stateLocation.MoveCanceled();
        };

        controller.Player.LaunchDecoy.performed += ctx =>
        {

            stateAction.PressForFishing();


        };
        controller.Player.RideUpDecoy.performed += ctx => stateAction.RideUpFishingRodePerformed();

        controller.Player.RideUpDecoy.canceled += ctx => stateAction.RideUpFishingRodeCanceled();

        controller.Player.FightingStruggle.performed += ctx => stateAction.FightingStruggle();

        controller.Player.InteractWithObject.performed += ctx =>
        {
            stateLocation.InteractWithObject();

        };

    }

    void Start()
    {
        cameraGame.AttachCameraAndSetPosition(transform);
        if (resetValue)
        {
            ResetValue();
        }
    }

    public void ResetValue()
    {
        //Money = 0;
    }
    public void SetComponents()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        stateLocation.Update();
        stateAction.Update();
    }

    private void FixedUpdate()
    {
        stateLocation.FixedUpdate();
        stateAction.FixedUpdate();

    }


    //C'est appelé par l'animation fishing
    void LaunchFishingRode()
    {
        stateAction.LaunchFishingRode();
    }



    public void FailFishing()
    {
        stateAction.FailFishing();
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 5);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 3);
        Gizmos.DrawRay(transform.position, transform.TransformDirection(Vector2.left) * 100);
    }

    public void GoInBoat()
    {
        stateLocation.GoInBoat();
    }

    public void GoOutOfBoat(Ground ground)
    {
        stateLocation.GoOutOfBoat(ground);
    }

    private void OnEnable()
    {
        controller.Player.Enable();
    }

    private void OnDisable()
    {
        controller.Player.Disable();
    }

    public void SetStateLocation(PlayerStateLocation stateLocation)
    {
        this.stateLocation = stateLocation;
    }

    public void SetStateAction(PlayerStateAction stateAction)
    {

        this.stateAction = stateAction;
    }



    public PlayerStateLocation GetStateLocation()
    {
        return stateLocation;
    }

    public PlayerStateAction GetStateAction()
    {
        return stateAction;
    }


}
