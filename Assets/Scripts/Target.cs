using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, IArrowHittable
{
    public float forceAmount = 1.0f;
    //public Material otherMaterial = null;

    public void Hit(Arrow arrow)
    {
        //ApplyMaterial();
        ApplyForce(arrow.transform.forward);
    }

    //private void ApplyMaterial()
    //{
      //  MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
     //   meshRenderer.material = otherMaterial;
    //}

    private void ApplyForce(Vector3 direction)
    {
        Rigidbody rigidBody = GetComponent<Rigidbody>();
        rigidBody.AddForce(direction * forceAmount);
    }
}
