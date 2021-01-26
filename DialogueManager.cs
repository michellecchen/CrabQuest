using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public Animator animator;

    public AudioSource audioSource;
    public AudioClip[] audioClips;

    private Queue<string> sentences; // FIFO to load dialogue

    // Start is called before the first frame update
    void Start()
    {
        // audioSource = GetComponent<AudioSource>();
        sentences = new Queue<string>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void Update() {
        if (Input.GetKeyDown("k")) {
            DisplayNextSentence();
        }
    }

    public void StartDialogue(MyDialogue dialogue) {
        
        animator.SetBool("IsOpen", true);

        nameText.text = dialogue.name;
        sentences.Clear();
        
        foreach(string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        
        if (sentences.Count == 0) {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        
        StopAllCoroutines(); // in case user starts new sentence b4 prev. finished animating
        StartCoroutine(TypeSentence(sentence));
    }

    //coroutine
    IEnumerator TypeSentence(string sentence) {
        dialogueText.text="";
        
        foreach(char letter in sentence.ToCharArray()) {
            dialogueText.text += letter;
            // audioSource.Play();
            PlayRandom();
            yield return new WaitForSeconds(0.08f);
        }
    }

    void PlayRandom() {
        audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
        audioSource.Play();
    }

    void EndDialogue() {
        animator.SetBool("IsOpen", false);
    }
    
}
