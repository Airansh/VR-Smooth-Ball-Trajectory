using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public GameObject targetPrefab;
    public Transform spawnPoint;

    public void CreateTarget()
    {
        Instantiate(targetPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
