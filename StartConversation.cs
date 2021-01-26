using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartConversation : MonoBehaviour
{

    public Button startBtn;
    public Animator animator;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            animator.SetBool("IsOpen", true);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player") {
            animator.SetBool("IsOpen", false);
        }
    }
}
