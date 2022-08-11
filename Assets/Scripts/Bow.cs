using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Bow : XRGrabInteractable
{
    private Animator animator = null;
    private Pull pull = null;
   // private ReleaseArrow releaseArrow = null;

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        pull = GetComponentInChildren<Pull>();
       // releaseArrow = GetComponentInChildren<ReleaseArrow>();
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if(updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
        {
            if(isSelected)
            {
                BowAnimation(pull.PullAmount);
            }
        }
    }

    private void BowAnimation(float value)
    {
        animator.SetFloat("Blend", value);
    }

/*    protected override void OnEnable()
    {
        base.OnEnable();
        selectEntered.AddListener(releaseArrow.SetReady);
        selectExited.AddListener(releaseArrow.SetReady);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        selectEntered.RemoveListener(releaseArrow.SetReady);
        selectExited.RemoveListener(releaseArrow.SetReady);
    }*/
}