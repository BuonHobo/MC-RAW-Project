using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    //[SerializeField] private Text cherriesText;

    //[SerializeField] private AudioSource collectionSoundEffect;

    public bool collected { get; private set; } = false;

    [SerializeField] private Image indicator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectible"))
        {
            //collectionSoundEffect.Play();
            var anim=collision.gameObject.GetComponent<Animator>();
            anim.SetBool("Animate",false);
            anim.SetTrigger("Collect");
            Destroy(collision.gameObject,0.5f);
            indicator.gameObject.SetActive(true);
        }
    }
}
