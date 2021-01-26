using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    // public Transform cam;
    Camera cam;
    public LayerMask movementMask;
    PlayerMotor motor;
    public Animator animator;
    public Interactable focus;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update()
    {

        // check if you're hovering over UI
        // exit out if you click on UI - aka don't move player 
        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }

        if (Input.GetMouseButtonDown(0)) {
            
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, movementMask)) {
                
                // isMoving = true;
                // move our player to selected direction
                motor.MoveToPoint(hit.point);
                
                RemoveFocus();
            }

        }

        if (Input.GetMouseButton(1)) {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100)) {
                // Check if we hit an interactable
                // If we did set it as our focus
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null) {
                    SetFocus(interactable);
                }
            }
        }

    }
    
    void SetFocus(Interactable newFocus) {
        
        if (newFocus != focus) {
            if (focus != null) {
                focus.OnDefocused();
            }
            focus = newFocus;
            motor.FollowTarget(newFocus);
        }

        newFocus.OnFocused(transform);
    
        // isMoving = true;
    }

    void RemoveFocus() {
        if (focus != null) {
            focus.OnDefocused();
        }
        focus = null;
        motor.StopFollowingTarget();
    }

}
