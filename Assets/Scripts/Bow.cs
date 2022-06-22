using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Bow : XRGrabInteractable
{
    private Pull pull = null;

    protected override void Awake()
    {
        base.Awake();
        pull = GetComponentInChildren<Pull>();
    }
}