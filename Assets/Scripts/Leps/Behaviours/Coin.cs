using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private Animator animator;
    private bool isExplode;
    [SerializeField] AudioClip audioClip;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        isExplode = false;
    }

    // Update is called once per frame
    void Update()
    {
     animator.SetBool("isExplode", isExplode);   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(audioClip != null) audioSource.PlayOneShot(audioClip);
            isExplode = true;
            Destroy(gameObject, 1);

        }
    }
}
