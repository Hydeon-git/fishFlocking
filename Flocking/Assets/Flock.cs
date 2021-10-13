using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Flock : MonoBehaviour
{
    public FlockManager flockManager;
    int fishNum = 0;
    float speed;
    Vector3 direction;

    void Start()
    {
        speed = Random.Range(flockManager.minSpeed, flockManager.maxSpeed);        
    }
    void Update()
    {
        //Rules();
        InvokeRepeating("Rules",1 , 2);
        transform.rotation = Quaternion.Slerp(transform.rotation,
                             Quaternion.LookRotation(direction),
                             flockManager.rotationSpeed * Time.deltaTime);
        transform.Translate(0, 0, Time.deltaTime * speed);        
    }
    void Rules()
    {        
        // Flocking Rules    
        Vector3 cohesion = Vector3.zero;
        Vector3 align = Vector3.zero;
        Vector3 separation = Vector3.zero;
        

        foreach (GameObject go in flockManager.allFish)
        {
            if (go != this.gameObject)
            {
                float distance = Vector3.Distance(go.transform.position, transform.position);

                if (distance <= flockManager.neighbourDistance)
                {
                    cohesion += go.transform.position;
                    align += go.GetComponent<Flock>().direction;
                    separation -= (transform.position - go.transform.position) /
                        (distance * distance);
                    direction = (cohesion + align + separation).normalized * speed;
                    fishNum++;
                }
            }
        }
        Debug.Log(fishNum);
        if (fishNum > 0)
        {
            cohesion = (cohesion / fishNum - transform.position).normalized * speed;
            align /= fishNum;
            speed = Mathf.Clamp(align.magnitude, flockManager.minSpeed, flockManager.maxSpeed);
        }
        
    }
}
