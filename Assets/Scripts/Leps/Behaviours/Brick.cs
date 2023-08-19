using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] new ParticleSystem particleSystem;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            ParticleSystem particle = Instantiate<ParticleSystem>(particleSystem);
            particle.Play();
            Destroy(gameObject, 1);
        }
    }
}
