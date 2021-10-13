using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockManager flockManager;    
    float speed, globalSpeed, distance;
    int fishNum;

    void Start()
    {
        speed = Random.Range(0, 5);
        globalSpeed = 1.0f;
    }
    void Update()
    {
        transform.Translate(0, 0, Time.deltaTime * speed);
        Algorithms();
    }
    void Algorithms()
    {        
        // Flocking Rules
        Vector3 cohesion = Vector3.zero;
        Vector3 align = Vector3.zero;
        Vector3 separation = Vector3.zero;

        foreach (GameObject go in flockManager.allFish)
        {
            if (go != this.gameObject)
            {
                float distance = Vector3.Distance(go.transform.position,
                                                  transform.position);
                if (distance <= flockManager.neighbourDistance)
                {
                    cohesion += go.transform.position;
                    fishNum++;
                }
            }
        }
        if (fishNum > 0)
        {
            cohesion = (cohesion / fishNum - transform.position).normalized * speed;
        }
            
    }
}
