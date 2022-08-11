using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowCast : MonoBehaviour
{
    [SerializeField] private Transform tip;
    [SerializeField] private LayerMask layerMask = ~0;

    private Vector3 prevLoc = Vector3.zero;

    public bool CheckCollision(out RaycastHit hit)
    {
    
        if (prevLoc == Vector3.zero)
            prevLoc = tip.position;

        bool collided = Physics.Linecast(prevLoc, tip.position, out hit, layerMask);
        prevLoc = collided ? prevLoc : tip.position;
        Debug.Log(collided);

        return collided;
    }


}
