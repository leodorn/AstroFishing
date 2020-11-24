using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointeur : MonoBehaviour
{
    // Start is called before the first frame update
    List<Transform> pointList;
    [SerializeField]
    Transform endPointLeft, endPointRight, startPoint;
    [SerializeField]
    float speed;
    [SerializeField]
    Transform barCircle;
    Transform actualPoint;
    bool goToRight;

    private void Awake()
    {
        pointList = new List<Transform>();
        for(int i=0;i<barCircle.childCount;i++)
        {
            pointList.Add(barCircle.GetChild(i));
        }
        actualPoint = startPoint;
    }
    void Start()
    {
        transform.position = startPoint.position;
        MakeRotationAround();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Vector2.Distance(transform.position,actualPoint.position)<=0.001)
        {
            actualPoint = GetNextPoint();
        }
        MakeRotationAround();
        transform.position = Vector2.MoveTowards(transform.position, actualPoint.position, speed * Time.deltaTime);
    }

    void MakeRotationAround()
    {
        
        Vector2 diff = (Vector2)transform.position - (Vector2)barCircle.position;
        diff.Normalize();


        float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, rotZ);

    }

    Transform GetNextPoint()
    {
        if(actualPoint == endPointLeft)
        {
            goToRight = true;
        }
        else if( actualPoint == endPointRight)
        {
            goToRight = false;
        }
        if(goToRight)
        {
            return pointList[pointList.IndexOf(actualPoint) + 1];
        }
        else
        {
            return pointList[pointList.IndexOf(actualPoint) - 1];
        }
    }
}
