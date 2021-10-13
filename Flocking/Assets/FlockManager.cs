using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
	public GameObject fishPrefab;
	public int numFish = 20;
	public GameObject[] allFish;

	[Range(1.0f, 10.0f)]
	public float neighbourDistance;

	void Start()
	{		
		int x, y, z;
		int dx, dy, dz;
		allFish = new GameObject[numFish];
		for (int i = 0; i < numFish; i++)
		{
			Vector3 pos = this.transform.position + new Vector3((x = Random.Range(-10, 10)), (y = Random.Range(-10, 10)), (z = Random.Range(-10, 10)));
			Vector3 randomize = new Vector3((dx = Random.Range(0, 360)), (dy = Random.Range(0, 360)), (dz = Random.Range(0, 360)));
			allFish[i] = (GameObject)Instantiate(fishPrefab, pos, Quaternion.LookRotation(randomize));
			allFish[i].GetComponent<Flock>().flockManager = this;
		}
	}
	void Update()
    {
        
    }
}

