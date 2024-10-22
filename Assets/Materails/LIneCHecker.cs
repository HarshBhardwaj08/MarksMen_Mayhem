using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LIneCHecker : MonoBehaviour
{
    // Two GameObjects to check if they are parallel or intersecting
    public GameObject gameObject1;
    public GameObject gameObject2;

    void Start()
    {
        Vector3 direction1 = gameObject1.transform.forward; // Forward direction of GameObject 1
        Vector3 direction2 = gameObject2.transform.forward; // Forward direction of GameObject 2

        // Check if parallel
        if (AreObjectsParallel(direction1, direction2))
        {
            Debug.Log("The objects are parallel.");
        }
        else
        {
            Debug.Log("The objects are not parallel.");
        }

        // Check for intersection (you'll need a method to determine if their paths intersect)
        if (DoObjectsIntersect(gameObject1.transform.position, direction1, gameObject2.transform.position, direction2))
        {
            Debug.Log("The objects' lines intersect.");
        }
        else
        {
            Debug.Log("The objects' lines do not intersect.");
        }
    }

    // Function to check if two directions are parallel using cross product
    bool AreObjectsParallel(Vector3 dir1, Vector3 dir2)
    {
        Vector3 crossProduct = Vector3.Cross(dir1, dir2);
        return crossProduct.magnitude < Mathf.Epsilon; // Check if the cross product is near zero
    }

    // Function to check if two lines intersect (using basic vector math)
    bool DoObjectsIntersect(Vector3 point1, Vector3 dir1, Vector3 point2, Vector3 dir2)
    {
        Vector3 lineBetweenPoints = point2 - point1;
        Vector3 crossDir = Vector3.Cross(dir1, dir2);

        // If lines are parallel (cross product of directions is zero), they don't intersect
        if (crossDir.magnitude < Mathf.Epsilon)
            return false;

        // Otherwise, calculate if the lines meet at some point (intersection)
        // t is the parameter for the line1, u is for line2
        float t = Vector3.Dot(Vector3.Cross(lineBetweenPoints, dir2), crossDir) / crossDir.sqrMagnitude;
        float u = Vector3.Dot(Vector3.Cross(lineBetweenPoints, dir1), crossDir) / crossDir.sqrMagnitude;

        // If t and u are both positive or within the range you're interested in, they intersect
        return (t >= 0 && u >= 0);
    }
}
