using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Bow : XRGrabInteractable
{
    private Animator animator = null;
    private Pull pull = null;

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        pull = GetComponentInChildren<Pull>();
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
}