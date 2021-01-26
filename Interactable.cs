using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    bool isFocus = false;
    Transform player;

    bool hasInteracted = false;

    public Transform interactionTransform;

    // virtual method - meant to be overwritten
    public virtual void Interact() {
        // Debug.Log("Interacted with: " + gameObject.name);
    }

    void Update() {
        if (isFocus && !hasInteracted) {
            float distance = Vector3.Distance(player.position, transform.position); // ?
            if (distance <= radius) {
                Interact();
                hasInteracted = true;
            }
        }
    }

    public void OnFocused (Transform playerTransform) {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public void OnDefocused() {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }
    // distance specifying how close player
    // needs to get to object to interact with it;
    // in this case picking up an item

    void OnDrawGizmosSelected() {
        
        if (interactionTransform == null) {
            interactionTransform = transform;
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
