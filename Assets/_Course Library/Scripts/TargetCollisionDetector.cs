using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCollisionDetector : MonoBehaviour
{
    public static int counter = 0;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            counter++;
            Debug.Log("Collision Detected. Counter: " + counter);

            // Destroy the target object
            Destroy(gameObject);
        }
    }
}
