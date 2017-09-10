using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnpointHolder : NetworkBehaviour {
    public Vector3[] spawnPoints;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        foreach (var item in spawnPoints)
        {
            Gizmos.DrawCube(item, Vector3.one * 0.5f);
        }
    }
}
