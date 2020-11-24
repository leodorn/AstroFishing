using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingRode : MonoBehaviour
{

    public FishingRodeData fishingRodeData;
    float touchWaterPoint;
    [HideInInspector]
    public Fish fish;
    Rigidbody2D rigidBodyFishingRode;
    // Start is called before the first frame update

    private void Awake()
    {
        rigidBodyFishingRode = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        touchWaterPoint = 999;
        transform.position = new Vector3(transform.position.x, transform.position.y, 1);
    }

    // Update is called once per frame
    void Update()
    {
        CreateLine(Player.instance.spawnDecoyTransform.position);
    }

    private void FixedUpdate()
    {
        if (touchWaterPoint != 999)
        {

            if (Mathf.Abs(touchWaterPoint - transform.position.y) >= fishingRodeData.depthLimit)
            {
                rigidBodyFishingRode.velocity =
                    new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
            }
            else
            {
                rigidBodyFishingRode.velocity = new Vector2(0, fishingRodeData.forceToGoDownInWater);
            }
        }
       
        Collider2D[] listCollider = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        foreach (Collider2D collider in listCollider)
        {
            if (collider.CompareTag("End"))
            {
                rigidBodyFishingRode.velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
            }
        }
    }

    void CreateLine(Vector2 positionToCreateLine)
    {
        LineRenderer lineRenderer = transform.GetChild(0).GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, positionToCreateLine);
        lineRenderer.SetPosition(1, transform.position);
        lineRenderer.startWidth = 0.03f;
        lineRenderer.endWidth = 0.03f;
    }
    
    public void MoveTowards(Transform transformTarget,float speed)
    {
        transform.position = Vector2.MoveTowards(transform.position, transformTarget.position, speed * Time.deltaTime);
    }

    public void AddForce(Vector2 directionToApplyToDecoy,float forceToApplyToDecoy, float forceToLaunchFishingRode)
    {
        rigidBodyFishingRode.AddForce(directionToApplyToDecoy * forceToApplyToDecoy * forceToLaunchFishingRode);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Water"))
        {
            Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();
            rigidBody.velocity = new Vector2(0, fishingRodeData.forceToGoDownInWater);
            rigidBody.gravityScale = 0;
            touchWaterPoint = collision.transform.position.y;
        }
    }


    public void GetBackFish()
    {
        Player.instance.SetStateAction(new PlayerExplore(Player.instance));
        if (fish != null)
        {
            if (Player.instance.fishCaughtList.Count < 3 && !(Player.instance.GetStateLocation() is PlayerInBoat))
            {
                //GameObject pointToStock = GameObject.Find("PointToStockFishNoBoat");
                //StockFish(pointToStock.transform);
                Player.instance.fishCaughtList.Add(fish);
                fish.SetState(new FishDead(fish, Player.instance));
                Player.instance.SetStateAction(new PlayerCarryFish(Player.instance,fish));
            }
            else if(Player.instance.GetStateLocation() is PlayerInBoat && 
                Player.instance.boat.GetComponent<Boat>().stockInBoatForFish > Player.instance.boat.GetComponent<Boat>().fishCaughtList.Count)
            {
                StockFish(Player.instance.boat.GetComponent<Boat>().fishStockPlaceTransform);
                Player.instance.boat.GetComponent<Boat>().fishCaughtList.Add(fish);
                FishManager.instance.DestroyFish(fish);
            }
            else
            {
                Debug.Log("Y'a plus de place ! ");
                fish.Jump();
            }
         
        }
        Player.instance.cameraGame.AttachCameraAndSetPosition(Player.instance.transform);
        Destroy(gameObject);
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
        
    }

    void StockFish(Transform place)
    {
        GameObject deadFish;
        if(Player.instance.GetStateLocation() is PlayerInBoat)
        {
            deadFish = Instantiate(fishingRodeData.deadFishPrefab,
            new Vector2(place.position.x,
            (float)(place.position.y + 0.25 * Player.instance.boat.fishCaughtList.Count)),
            Quaternion.identity); 
        }
        else
        {
            deadFish = Instantiate(fishingRodeData.deadFishPrefab,
            new Vector2(place.position.x,
            (float)(place.position.y + 0.25 * Player.instance.fishCaughtList.Count)),
            Quaternion.identity);
        }
        
        deadFish.GetComponent<SpriteRenderer>().sprite = fish.fishData.deadSprite;

        deadFish.transform.localScale = fish.transform.lossyScale;
        deadFish.transform.SetParent(place.transform);
    }

    public bool InWaterOrNot()
    {
        if(touchWaterPoint == 999)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
