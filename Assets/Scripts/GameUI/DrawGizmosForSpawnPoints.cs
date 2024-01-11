using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGizmosForSpawnPoints : MonoBehaviour
{
    void OnDrawGizmos()
    {
        // Draw a red wire sphere at the transform's position
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1);
    }
}
