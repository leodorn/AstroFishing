using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorStruggle : MonoBehaviour
{
    float minForceOfTheFish, maxForceOfTheFish,timeToChangeForce;
    float speed, forceGivenByPlayer;
    [SerializeField]
    Transform startPoint, endPoint,greenZone;
    bool inGoodZone,coroutineCatchStarted, coroutineFreeStarted;
    [HideInInspector]
    public Fish fish;


    private void Awake()
    {
        forceGivenByPlayer = 0;
        speed = 0;
    }

    public void SetMinAndMaxForceAndTime(float minForceOfTheFish,float maxForceOfTheFish,float timeToChangeForce,float difficultyToCatch)
    {
        this.minForceOfTheFish = minForceOfTheFish;
        this.maxForceOfTheFish = maxForceOfTheFish;
        this.timeToChangeForce = timeToChangeForce;
        greenZone.localScale = new Vector3(difficultyToCatch, greenZone.localScale.y, greenZone.localScale.z);
    }

    private void Update()
    {
        if(CheckCollider("GoodZone",0.3f))
        {

            if (!inGoodZone)
            {
                inGoodZone = true;
                if (!coroutineCatchStarted)
                {
                    StartCoroutine(StayInGoodZoneTimer(0));

                }

            }
        }
        else
        {
            inGoodZone = false;
            if(!coroutineFreeStarted)
            {
                StartCoroutine(StayInWrongZoneTimer(0));
            }
        }
    }
           
        
    

    bool CheckCollider(string tag,float radius)
    {
        Collider2D[] listCollider = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D collider in listCollider)
        {
            if(collider.CompareTag(tag))
            {
                return true;
            }
        }
        return false;
    }

    IEnumerator StayInWrongZoneTimer(int timeInWrongZone)
    {
        coroutineFreeStarted = true;
        yield return new WaitForSeconds(1f);
        if (!inGoodZone)
        {
            if (timeInWrongZone == fish.fishData.timeToBeFree-1)
            {
                Debug.Log("Il s'est libéré !");
                Player.instance.SetStateAction(new PlayerFishing(Player.instance));
                fish.BeFree();
                GameManager.instance.SetBarCircleActive(false);

            }
            else
            {
                StartCoroutine(StayInWrongZoneTimer(timeInWrongZone + 1));
            }
        }
        else
        {
            coroutineFreeStarted = false;
        }
    }

    IEnumerator StayInGoodZoneTimer(int timeInGoodZone)
    {
        coroutineCatchStarted = true;
        yield return new WaitForSeconds(1f);
        if(inGoodZone)
        {
            if(timeInGoodZone == fish.fishData.timeToBeCatch-1)
            {
                Debug.Log("Capturé !");
                Player.instance.SetStateAction(new PlayerFishing(Player.instance));
                fish.StopStruggling();
                GameManager.instance.SetBarCircleActive(false);


            }
            else
            {
                StartCoroutine(StayInGoodZoneTimer(timeInGoodZone + 1));
            }
        } 
        else
        {
            coroutineCatchStarted = false;
        }
    }

    public void LaunchCursor()
    {
        StopAllCoroutines();
        transform.position =  new Vector2(startPoint.position.x +0.1f, startPoint.position.y);
        StartCoroutine(ChangeForce());
        StartCoroutine(ReduceForce());
        Player.instance.SetStateAction(new PlayerStruggle(Player.instance));
        speed = 0;
        forceGivenByPlayer = 0;
        inGoodZone = false;
        coroutineFreeStarted = false;
        coroutineCatchStarted = false;
    }

    private void FixedUpdate()
    {
        
        float force = speed + forceGivenByPlayer;
        //Debug.Log("coucou" + force + "speed "+ speed + "forceGiven" + forceGivenByPlayer);
        //Debug.Log("EndPosition " + Vector2.Distance(transform.position, endPoint.position));
        //Debug.Log("StartPosition " + Vector2.Distance(transform.position, startPoint.position));
        if (force <0)
        {
            if(Vector2.Distance(transform.position, startPoint.position) > 0.1)
            {
                transform.Translate(Vector2.right * force * Time.deltaTime);
            }
            
        }
        else if(force>0 )
        {
            if (Vector2.Distance(transform.position, endPoint.position) > 0.1)
            {
                transform.Translate(Vector2.right * force * Time.deltaTime);
            }
            else
            {
                forceGivenByPlayer /= 2;
            }
        }
        
    }

    
    void SetSpeed(float x)
    {
        this.speed = -x;
    }

    public void SetForceGiven(float force)
    {
        this.forceGivenByPlayer = force;
    }

    public float GetForceGivenByPlayer()
    {
        return forceGivenByPlayer;
    }
    IEnumerator ChangeForce()
    {
        SetSpeed(Random.Range(minForceOfTheFish, maxForceOfTheFish));
        yield return new WaitForSeconds(timeToChangeForce);
        StartCoroutine(ChangeForce());
    }

    IEnumerator ReduceForce()
    {
        forceGivenByPlayer -= 0.3f;
        if(forceGivenByPlayer <0)
        {
            forceGivenByPlayer = 0;
        }
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(ReduceForce());
    }

    

}
