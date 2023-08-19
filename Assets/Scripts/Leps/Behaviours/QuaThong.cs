using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuaThong : MonoBehaviour
{
    private Animator animator;
    private bool isExplore;
    [SerializeField] AudioClip audioClip;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        isExplore = false;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isExplore", isExplore);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (audioClip != null) audioSource.PlayOneShot(audioClip);
            isExplore = true;
            Destroy(gameObject,1);
        }
    }
}
