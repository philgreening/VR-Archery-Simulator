using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Arrow : XRGrabInteractable
{
    public float speed = 2000.0f;

    //[SerializeField] private Transform tip;
    //    public LayerMask layerMask = ~Physics.IgnoreRaycastLayer;
    //  public LayerMask layerMask = ~0;
   // [SerializeField] private LayerMask layerMask = ~0;


    //private new Collider collider = null;
    private new Rigidbody rigidBody;

    private Vector3 prevLoc = Vector3.zero;
    private bool fired = false;

    private RaycastHit hit;

    private ArrowCast cast;

    protected override void Awake()
    {
        base.Awake();
     //   collider = GetComponent<Collider>();
        rigidBody = GetComponent<Rigidbody>();

        cast = GetComponent<ArrowCast>();

    }

    /*    protected override void OnSelectEntering(SelectEnterEventArgs args)
        {
            if (args.interactorObject is XRDirectInteractor)
                Clear();

            base.OnSelectEntering(args);
        }
    */
    /*  private void Clear()
      {
          SetFire(false);
          TogglePhysics(true);
      }*/

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        Debug.Log(args.interactorObject);

        if (args.interactorObject is ReleaseArrow notch)
            Fire(notch);
    }

    private void Fire(ReleaseArrow notch)
    {
        // if (notch.IsReady)
        //{
        // SetFire(true);
        fired = true;
           // UpdateLastLoc();
        ApplyForce(notch.Pull);
        StartCoroutine(Launch());
        Debug.Log("fire called");
        //}
    }

  /*  private void SetFire(bool value)
    {
        //collider.isTrigger = value;
        fired = value;
    }*/

  /*  private void UpdateLastLoc()
    {
        prevLoc = tip.position;
    }*/

    private void ApplyForce(Pull pull)
    {
        float power = pull.PullAmount;
        Vector3 force = transform.forward * (power * speed);
        rigidBody.AddForce(force);
    }

    /*    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
        {
            base.ProcessInteractable(updatePhase);

            if (fired)
            {
                if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
                {
                    if (CheckCollision())
                    {
                        fired = false;
                        UpdateLastLoc();
                    }
                }

                if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Fixed)
                {
                    SetDirection();
                    Start
                }
            }
        }*/

    private IEnumerator Launch()
    {
        while (!cast.CheckCollision(out hit))
        {
            SetDirection();
            yield return null;
        }
        StopPhysics();
        ChildArrow(hit);
        CheckForObject(hit);
        //fired = !fired;
       // UpdateLastLoc();
    }


    private void SetDirection()
    {
        if (rigidBody.velocity.z > 0.5f)
        {
            transform.forward = rigidBody.velocity;
        }
    }

/*    private bool CheckCollision(out RaycastHit hit)
    {
        *//*      if (Physics.Linecast(prevLoc, tip.position, out hit, layerMask))
              {
                  TogglePhysics(false);
                  ChildArrow(hit);
                  CheckForObject(hit);
              }

              return hit.collider != null;*//*
        if (prevLoc == Vector3.zero)
            prevLoc = tip.position;

        bool collided = Physics.Linecast(prevLoc, tip.position, out hit, layerMask);
        prevLoc = collided ? prevLoc : tip.position;
        Debug.Log(collided);

        return collided;
    }*/

    private void StopPhysics()
    { 
        rigidBody.isKinematic = true;
        rigidBody.useGravity = false;
    }

    private void ChildArrow(RaycastHit hit)
    {
        Transform newParent = hit.transform;
        transform.SetParent(newParent);
    }

    private void CheckForObject(RaycastHit hit)
    {
        /*  GameObject hitObject = hit.transform.gameObject;
          IArrowHittable hitable = hitObject ? hitObject.GetComponent<IArrowHittable>() : null;

          if (hitable != null)
              hitable.Hit(this);*/
        if (hit.transform.TryGetComponent(out IArrowHittable hittable))
            hittable.Hit(this);
    }

/*    public override bool IsSelectableBy(IXRSelectInteractor interactor)
    {
        return base.IsSelectableBy(interactor) && !fired;
    }
*/
}
