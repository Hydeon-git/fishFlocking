using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    // Flock Manager
    public FlockManager flockManager;
    
    // Variables
    public float angle = 270;
    public float damp = 1;
    private float speed;
    private Vector3 currentVelocity;

    // Lists
    private List<Flock> cohesionNeighbours = new List<Flock>();
    private List<Flock> alignNeighbours = new List<Flock>();

    public void MoveFishes()
    {
        //-----------------------------------------------------------------------------------
        // Add fishes to list if we find a neighbour
        cohesionNeighbours.Clear();        
        alignNeighbours.Clear();
        
        for (int i = 0; i < flockManager.numFish; i++)
        {
            Flock currentUnit = flockManager.allFish[i];
            if (currentUnit != this)
            {
                // Fish distance from the center
                float currentNeighbourDistance =
                    Vector3.SqrMagnitude(currentUnit.transform.position - transform.position);

                if (currentNeighbourDistance <= flockManager.neighbourDistance * flockManager.neighbourDistance)
                {
                    cohesionNeighbours.Add(currentUnit);
                }
                if (currentNeighbourDistance <= flockManager.alignDistance * flockManager.alignDistance)
                {
                    alignNeighbours.Add(currentUnit);
                }
            }
        }
        //-----------------------------------------------------------------------------------
        // Calculate Speed
        speed = 0;
        for (int i = 0; i < cohesionNeighbours.Count; i++)
        {
            speed += cohesionNeighbours[i].speed;
        }
        speed /= cohesionNeighbours.Count;
        speed = Mathf.Clamp(speed, flockManager.minSpeed, flockManager.maxSpeed);
        //-----------------------------------------------------------------------------------
        // Final calculation of the cohesion vector with the data obtained
        Vector3 cohesionVector =  Cohesion() * flockManager.cohesionWeight;
        Vector3 alignVector =  Align() * flockManager.alignWeight;
        Vector3 boundVector = Boundary() * flockManager.boundWeight;
        //-----------------------------------------------------------------------------------
        // Move Fishes according to parameters
        Vector3 movementVector = cohesionVector + alignVector + boundVector;
        movementVector = Vector3.SmoothDamp(transform.forward, movementVector, ref currentVelocity, damp);
        movementVector = movementVector.normalized * speed;

        if (movementVector == Vector3.zero)
        {
            movementVector = transform.forward;
        }
        transform.forward = movementVector;
        transform.position += movementVector * Time.deltaTime;
    }
    private Vector3 Cohesion()
    {
        // Calculate Cohesion Vector
        Vector3 cohesion = Vector3.zero;
        int nearlyNeighbours = 0;

        if (cohesionNeighbours.Count == 0) return Vector3.zero;        

        for (int i = 0; i < cohesionNeighbours.Count; i++)
        {
            if (IsNear(cohesionNeighbours[i].transform.position))
            {
                nearlyNeighbours++;
                cohesion += cohesionNeighbours[i].transform.position;
            }
        }
        cohesion = (cohesion / nearlyNeighbours) - transform.position;
        cohesion = cohesion.normalized;
        return cohesion;
    }
    private Vector3 Align()
    {
        // Calculate Cohesion Vector
        Vector3 align = transform.forward;
        int nearlyNeighbours = 0;

        if (alignNeighbours.Count == 0) return transform.forward;

        for (int i = 0; i < alignNeighbours.Count; i++)
        {
            if (IsNear(alignNeighbours[i].transform.position))
            {
                nearlyNeighbours++;
                align += alignNeighbours[i].transform.forward;
            }
        }
        align = (align / nearlyNeighbours);
        align = align.normalized;
        return align;
    }

    private Vector3 Boundary()
    {
        Vector3 fromCenter = flockManager.transform.position - transform.position;
        bool isNearCenter = (fromCenter.magnitude >= flockManager.boundDistance * 0.9f);
        if (isNearCenter)
        {
            return fromCenter.normalized;
        }
        else
        {
            return Vector3.zero;
        }
    }
    private bool IsNear(Vector3 pos)
    {
        return Vector3.Angle(transform.forward, pos - transform.position) <= angle;
    }
}
