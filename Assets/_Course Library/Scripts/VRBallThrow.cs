// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
//
// public class VRBallThrow : MonoBehaviour
// {
//     private Rigidbody rb;
//     private Vector3 lastPosition;
//
//     void Start()
//     {
//         rb = GetComponent<Rigidbody>();
//     }
//
//     void Update()
//     {
//         // Track the position of the ball (or controller) every frame
//         lastPosition = transform.position;
//     }
//
//     public void ReleaseBall()
//     {
//         Vector3 throwVelocity = (transform.position - lastPosition) / Time.deltaTime;
//         rb.AddForce(throwVelocity, ForceMode.VelocityChange);
//     }
// }
using UnityEngine;
using System.Collections.Generic;

public class VRBallThrow : MonoBehaviour
{
    private Rigidbody rb;
    private LinkedList<Vector3> positions;
    private const int maxPositions = 10;
    private const float updateInterval = 0.05f;
    private bool isReleased = false;
    private float pathProgress = 0.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        positions = new LinkedList<Vector3>();
        InvokeRepeating(nameof(TrackPosition), 0, updateInterval);
    }

    void TrackPosition()
    {
        if (!isReleased)
        {
            positions.AddLast(transform.position);

            if (positions.Count > maxPositions)
            {
                positions.RemoveFirst();
            }
        }
    }

    public void ReleaseBall()
    {
        isReleased = true;
        rb.isKinematic = true; // Disable physics control
    }

    void Update()
    {
        if (isReleased)
        {
            if (pathProgress < positions.Count - 1)
            {
                // Manually iterate through the linked list to access elements by index
                LinkedListNode<Vector3> currentNode = positions.First;
                for (int i = 0; i < (int)pathProgress; i++)
                {
                    currentNode = currentNode.Next;
                }
                Vector3 startPosition = currentNode.Value;
                Vector3 endPosition = currentNode.Next.Value;

                // Calculate the interpolated position
                float t = pathProgress - (int)pathProgress;
                transform.position = Vector3.Lerp(startPosition, endPosition, t);

                // Increment the path progress
                pathProgress += Time.deltaTime / updateInterval;
            }
            else
            {
                // Stop moving the ball and re-enable physics
                isReleased = false;
                rb.isKinematic = false;
            }
        }
    }
}

