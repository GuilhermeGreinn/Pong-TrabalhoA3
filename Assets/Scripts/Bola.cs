using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bola : MonoBehaviour
{
    public float MultiplicadorDificuldade = 1.3f;
    public float minXSpeed = 0.8f;
    public float maxXSpeed = 1.2f;
    public float minYSpeed = 0.8f;
    public float maxYSpeed = 1.2f;

    public GameObject SFX;
    public AudioClip somColisaoRaquete;
    public AudioClip somColisaoParede;

    private Rigidbody2D rigidbodyBall;
    private AudioSource audioSource;

    void Start()
    {
        rigidbodyBall = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        IniciarMovimento();
    }

    public void IniciarMovimento()
    {
        if (rigidbodyBall != null)
        {
            rigidbodyBall.velocity = new Vector2(
                Random.Range(minXSpeed, maxXSpeed) * (Random.value > 0.5f ? -1 : 1),
                Random.Range(minYSpeed, maxYSpeed) * (Random.value > 0.5f ? -1 : 1)
            );
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "limite")
        {
            if (other.transform.position.y > transform.position.y && rigidbodyBall.velocity.y > 0)
            {
                rigidbodyBall.velocity = new Vector2(rigidbodyBall.velocity.x, -rigidbodyBall.velocity.y);
                PlayCollisionSound(somColisaoParede);
            }
            else if (other.transform.position.y < transform.position.y && rigidbodyBall.velocity.y < 0)
            {
                rigidbodyBall.velocity = new Vector2(rigidbodyBall.velocity.x, -rigidbodyBall.velocity.y);
                PlayCollisionSound(somColisaoParede);
            }
        }

        if (other.tag == "raquete")
        {
            if (other.transform.position.x > transform.position.x && rigidbodyBall.velocity.x > 0)
            {
                rigidbodyBall.velocity = new Vector2(-rigidbodyBall.velocity.x, rigidbodyBall.velocity.y);
                PlayCollisionSound(somColisaoRaquete);
            }

            if (other.transform.position.x < transform.position.x && rigidbodyBall.velocity.x < 0)
            {
                rigidbodyBall.velocity = new Vector2(-rigidbodyBall.velocity.x, rigidbodyBall.velocity.y);
                PlayCollisionSound(somColisaoRaquete);
            }
        }

        if (other.tag == "ponto1")
        {
            GameManager.score1++;
            GameManager.AtualizarPontuacao();
            ResetBola();
        }
        else if (other.tag == "ponto2")
        {
            GameManager.score2++;
            GameManager.AtualizarPontuacao();
            ResetBola();
        }
    }

    void ResetBola()
    {
        transform.position = Vector3.zero;
        IniciarMovimento();
    }

    void PlayCollisionSound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
