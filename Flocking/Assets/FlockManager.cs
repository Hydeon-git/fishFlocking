using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
	public GameObject fishPrefab;
	public int numFish = 20;
	public GameObject[] allFish;
	public Vector3 spawnLimit;

	[Range(0.0f, 5.0f)]
	public float minSpeed;
	[Range(0.0f, 10.0f)]
	public float maxSpeed;
	[Range(1.0f, 10.0f)]
	public float neighbourDistance;
	[Range(0.0f, 5.0f)]
	public float rotationSpeed;

	void Start()
	{		
		spawnLimit = new Vector3(5, 5, 5);
		allFish = new GameObject[numFish];

		for (int i = 0; i < numFish; i++)
		{
			Vector3 pos = this.transform.position + new Vector3(
				Random.Range(-spawnLimit.x, spawnLimit.x),
				Random.Range(-spawnLimit.y, spawnLimit.y),
				Random.Range(-spawnLimit.z, spawnLimit.z));


			Vector3 randomize = new Vector3(Random.Range(-360, 360), Random.Range(-360, 360), Random.Range(-360, 360));
			allFish[i] = (GameObject)Instantiate(fishPrefab, pos, Quaternion.LookRotation(randomize));
			allFish[i].GetComponent<Flock>().flockManager = this;
		}
	}
	void Update()
    {
        
    }
}

