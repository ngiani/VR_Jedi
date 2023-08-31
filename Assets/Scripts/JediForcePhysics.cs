using Oculus.Interaction.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(JediForceInteractable))]
[RequireComponent(typeof(Rigidbody))]
public class JediForcePhysics : MonoBehaviour
{
    [SerializeField] OVRHand leftHand;
    [SerializeField] OVRHand rightHand;

    JediForceInteractable interactableObject;

    OVRHand handInUse = null;
    Vector3 previousHandPosition;

    Rigidbody rigidBody;

    float forceStrength = 7.5F;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

        interactableObject = GetComponent<JediForceInteractable>();
        interactableObject.Selected.AddListener(OnInteractableObjSelected);
        interactableObject.UnSelected.AddListener(OnInteractableObjUnSelected);
    }

    private void OnInteractableObjUnSelected()
    {
        handInUse = null;
    }

    private void OnInteractableObjSelected(JediForceInteractable.HandType handType)
    {
        if (handInUse != null)
            return;

        switch (handType)
        {
            case JediForceInteractable.HandType.LEFT:
                handInUse = leftHand;
                break;
            case JediForceInteractable.HandType.RIGHT:
                handInUse = rightHand;
                break;
        }

        previousHandPosition = handInUse.PointerPose.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {

        if (interactableObject.IsSelected)
        {

            Vector3 forceDirection = handInUse.PointerPose.transform.position - previousHandPosition;

            previousHandPosition = handInUse.PointerPose.transform.position;

            if (forceDirection.magnitude > 0.01f)
                rigidBody.AddForce(forceDirection * forceStrength, ForceMode.Impulse);
        }

    }
}
