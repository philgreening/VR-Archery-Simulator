using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Pull : XRBaseInteractable
{
    public float PullAmount { get; private set; } = 0.0f;

    [SerializeField] private Transform start = null;
    [SerializeField] private Transform end = null;

    public Vector3 PullPosition => Vector3.Lerp(start.position, end.position, PullAmount);

    //private XRBaseInteractor pullInteractor = null;

 /*   protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        pullInteractor = (XRBaseInteractor)args.interactorObject;
    }
*/
    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        //pullInteractor = null;
        PullAmount = 0.0f;
    }

    private void UpdatePull()
    {
        Vector3 interactorPos = firstInteractorSelecting.transform.position;
        PullAmount = CalculatePull(interactorPos);
    }
    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);
        if(updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
        {
            if (isSelected)
            {
                // pullPosition = pullInteractor.transform.position;
                // PullAmount = CalculatePull(pullPosition);
                UpdatePull();
            }
        }
    }

    private float CalculatePull(Vector3 pullPosition)
    {
        Vector3 pullDirection = pullPosition - start.position;
        Vector3 targetDirection = end.position - start.position;
        float maxLen = targetDirection.magnitude;

        targetDirection.Normalize();

        float pullValue = Vector3.Dot(pullDirection, targetDirection) / maxLen;


        return Mathf.Clamp(pullValue, 0 , 1);
    }
}
