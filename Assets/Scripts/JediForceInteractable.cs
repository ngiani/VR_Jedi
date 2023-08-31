using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class JediForceInteractable : MonoBehaviour
{
    public HandEvent Selected = new HandEvent();
    public UnityEvent UnSelected;

    [SerializeField] private RayInteractor leftHandInteractor;
    [SerializeField] private RayInteractor rightHandInteractor;

    [SerializeField] private Outliner outliner;

    private bool leftHandInHover;
    private bool rightHandInHover;

    private bool selected;
    public bool IsSelected => selected;

    public enum HandType { LEFT, RIGHT}

    public class HandEvent : UnityEvent<HandType>
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
        leftHandInteractor.WhenStateChanged += OnLeftHandInteractorStateChanged;
        rightHandInteractor.WhenStateChanged += OnRightHandInteractorStateChanged;
    }



    private void OnRightHandInteractorStateChanged(InteractorStateChangeArgs state)
    {
        if (state.NewState == InteractorState.Hover)
        {
            rightHandInHover = true;

            outliner.ShowOutline();

            selected = true;

            Selected?.Invoke(HandType.RIGHT);

        }

        else if (state.NewState == InteractorState.Disabled || state.NewState == InteractorState.Normal)
        {

            rightHandInHover = false;


            if (!leftHandInHover)
            {
                selected = false;
                outliner.HideOutline();
                UnSelected?.Invoke();
            }
                
        }
           
    }

    private void OnLeftHandInteractorStateChanged(InteractorStateChangeArgs state)
    {
        if (state.NewState == InteractorState.Hover)
        {
            leftHandInHover = true;

            outliner.ShowOutline();

            selected = true;

            Selected?.Invoke(HandType.LEFT);
        }
           
        else if (state.NewState == InteractorState.Disabled || state.NewState == InteractorState.Normal)
        {
            leftHandInHover = false;

            if (!rightHandInHover)
            {
                outliner.HideOutline();
                selected = false;
                UnSelected?.Invoke();
            }
                
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("IS CUBE SELECTED ? " + selected);
    }
}
