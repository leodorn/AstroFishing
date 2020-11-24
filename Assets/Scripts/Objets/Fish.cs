using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Fish : MonoBehaviour
{
    public FishData fishData;
    public FishMarketData fishMarketData;
    [HideInInspector] public float price;
    [HideInInspector] public List<Vector2> movementPointList;    

    [HideInInspector] public Vector2 pointToGo;

    [HideInInspector] public float speed;
    [HideInInspector] public FishingRode fishingRode;
    [HideInInspector] public float weight;
    [HideInInspector] public Animator animator;
    [HideInInspector] public bool isStruggling { get; set; }
    [HideInInspector] public SpriteRenderer spriteRenderer;

    [HideInInspector] public FishState state;
    [HideInInspector] public Rigidbody2D rigidBodyFish;
    private void Awake()
    {
        speed = fishData.initialSpeed;
        rigidBodyFish = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        movementPointList = new List<Vector2>();
        pointToGo = new Vector2(0, 0);       
        SetState(new FishFree(this,false));
        price = fishMarketData.price;
    }

    private void Update()
    {
        state.Update();
    }

    

    void Start()
    {
        
        RandomSize();
        state.CreateMovementPoint();
 
    }

    void RandomSize()
    {
        weight = Random.Range(fishData.minWeight, fishData.maxWeight);
        transform.localScale = transform.localScale * weight;
        price *= weight;
    }

    public void Jump()
    {
        SetState(new FishJump(this));
        state.Jump();
    }

   

  
    public void StopStruggling()
    {
        state.StopStruggling();
    }
   
    

  

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fishData.rangeToSeeDecoy);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, fishData.rangeToSlowNearDecoy);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, fishData.rangeToPickDecoy);

    }


    public int getPriceDependOnWeight()
    {
        if((int)price == 0)
        {
            return 1;
        }
        return (int)price;
    }

    public void BeFree()
    {
        state.BeFree();
       
    }

    

    public void SetState(FishState state)
    {
        StopAllCoroutines();
        this.state = state;
    }



    




}
