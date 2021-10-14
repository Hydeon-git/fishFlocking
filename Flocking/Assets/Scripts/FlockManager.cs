using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
    public Flock fishPrefab;
    public int numFish = 20;
    public Vector3 spawnLimit;
    public Flock[] allFish { get; set; }

	// Behaviour parameters for the whole system
	[Header("Fish Parameters")]
	[Range(0.0f, 10.0f)]
	public float minSpeed;
	[Range(0.0f, 10.0f)]
	public float maxSpeed;	

	[Header("Cohesion Parameters")]
	[Range(0.0f, 10.0f)]
	public float neighbourDistance;
	[Range(0.0f, 10.0f)]
	public float alignDistance;
	[Range(0.0f, 10.0f)]
	public float boundDistance;

	[Header("Weight Parameters")]
	[Range(0.0f, 10.0f)]
	public float cohesionWeight;
	[Range(0.0f, 10.0f)]
	public float alignWeight;
	[Range(0.0f, 10.0f)]
	public float boundWeight;


	private void Start()
    {
		spawnLimit = new Vector3(5, 5, 5);
		allFish = new Flock[numFish];

		for (int i = 0; i < numFish; i++)
		{
			Vector3 pos = this.transform.position + new Vector3(
				Random.Range(-spawnLimit.x, spawnLimit.x),
				Random.Range(-spawnLimit.y, spawnLimit.y),
				Random.Range(-spawnLimit.z, spawnLimit.z));


			var rotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
			allFish[i] = Instantiate(fishPrefab, pos, rotation);
			allFish[i].GetComponent<Flock>().flockManager = this;
		}
	}
	private void Update()
	{
        for (int i = 0; i < allFish.Length; i++)
        {
			allFish[i].MoveFishes();
		}
		
	}
}
