using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSpawnManager : NetworkBehaviour {

    bool hasSpawned = false;
    public float spawnClearRadius = 0.5f;
    public int maxSpawnAttempts = 100;
	// Use this for initialization
	void Start () {

        //if we arent on the server just destroy this
        if (!isServer)
        {
            Destroy(this);
            return;
        }

        //find the spawnpoints
        SpawnpointHolder sph = FindObjectOfType<SpawnpointHolder>();

        //if there are no spawnpoints nuke this component
        if(sph == null)
        {
            Destroy(this);
            return;
        }

        //if the list of spawnpoints is empty poof the component
        if(sph.spawnPoints.Length == 0)
        {
            Destroy(this);
            return;
        }

        int count = 0;
        //while we havent spawned
        while (!hasSpawned)
        {
            //count how many tries we've had
            count++;

            //try spawning at a random spawn point
            Vector3 point = sph.spawnPoints[Random.Range(0, sph.spawnPoints.Length)];

            //if the point is clear
            if (Physics.OverlapSphere(point, spawnClearRadius).Length == 0)
            {
                transform.position = point;
                hasSpawned = true;
            }

            //if we've tried to spawn too many times just give up
            if(count > maxSpawnAttempts)
            {
                hasSpawned = true;
            }
        }

        //poof the component to get rid of jank when we are done
        Destroy(this);
	}
}
