using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] public AudioSource deathSoundEffect;
    public bool alive{get;private set;} =true;
    private SpriteRenderer sr;
    private void Start()
    {
        sr=GetComponent<SpriteRenderer>();
        rb=GetComponent<Rigidbody2D>();
        anim=GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap") && alive)
        {
            rb.constraints=RigidbodyConstraints2D.None;
            deathSoundEffect.Play();
            alive=false;
            sr.color=Color.grey;
            anim.enabled=false;
            Invoke("Die",1);
            //Die();
        }
    }

    private void Die()
    {
        anim.enabled=true;
        anim.SetTrigger("death");
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
