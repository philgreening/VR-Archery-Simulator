using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Quiver : XRBaseInteractable
{

    [SerializeField] private GameObject arrowModel = null;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        SpawnArrow(args);
    }

    /*    protected override void OnEnable()
        {
            base.OnEnable();
            selectEntered.AddListener(SpawnArrow);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            selectEntered.RemoveListener(SpawnArrow);
        }
    */
    private void SpawnArrow(SelectEnterEventArgs args)
    {
        // Create arrow, force into interacting hand
        Arrow arrow = CreateArrow(args.interactorObject.transform);
        interactionManager.SelectEnter(args.interactorObject, arrow);
    }

    private Arrow CreateArrow(Transform orientation)
    {
        // Create arrow, and get arrow component
        GameObject arrowObject = Instantiate(arrowModel, orientation.position, orientation.rotation);
        return arrowObject.GetComponent<Arrow>();
    }

}

