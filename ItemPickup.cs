using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item;
    public Animator animator;
    public AudioSource sfx;

    public override void Interact() {
        base.Interact();
        StartCoroutine(PickUp());
    }

    IEnumerator PickUp() {
        animator.SetTrigger("Attack_1");
        yield return new WaitForSeconds(1.25f);
        bool wasPickedUp = Inventory.instance.Add(item);
        if (wasPickedUp) {
            sfx.Play();

            Destroy(gameObject);
        }
    }
}
