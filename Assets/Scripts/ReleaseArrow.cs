using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class ReleaseArrow : XRSocketInteractor
{
    public Pull Pull { get; private set; }
    public Bow Bow { get; private set; }
    //public bool IsReady = false;


    protected override void Awake()
    {
        base.Awake();
        Pull = GetComponentInChildren<Pull>();
        Bow = GetComponentInParent<Bow>();

    }

    /*  protected virtual void OnHoverEnter(XRBaseInteractable interactable)
       {
           Debug.Log("called");
           UpdateAttach();
       }*/

    protected override void OnEnable()
    {
        base.OnEnable();
        Pull.selectExited.AddListener(FireArrow);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        Pull.selectExited.RemoveListener(FireArrow);
    }

    /*    public void DropArrow(SelectExitEventArgs args)
        {
            if (hasSelection)
            {
                interactionManager.SelectExit(this, firstInteractableSelected);
            }
        }*/

    public override void ProcessInteractor(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractor(updatePhase);
       /* if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
        {*/


            if (Bow.isSelected)
            {
                UpdateAttach();
            }
        //}

    }

    public void UpdateAttach()
    {
        attachTransform.position =Pull.PullPosition;
        //Debug.Log(attachTransform.position);
    }

   /* public void SetReady(BaseInteractionEventArgs args)
    {
        IsReady = args.interactable.isSelected;
        Debug.Log(IsReady);
    }*/


    public void FireArrow(SelectExitEventArgs args)
    {
        if (hasSelection)
        {
            interactionManager.SelectExit(this, firstInteractableSelected);
        }
    }


    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        return CanHover(interactable) && interactable is Arrow && Bow.isSelected;
    }


    //public override bool CanSelect(IXRSelectInteractable interactable)
    //{
    //    return CanHover(interactable) && QuickSelect(interactable);
    //}

    //private bool QuickSelect(IXRInteractable interactable)
    //{
    //    return !hasSelection || IsSelecting(interactable);
    //}

    private bool CanHover(IXRSelectInteractable interactable)
    {
        if (interactable is IXRHoverInteractable hoverInteractable)
            return CanHover(hoverInteractable);

        return false;
    }
}
