using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    //[SerializeField] private Text cherriesText;

    //[SerializeField] private AudioSource collectionSoundEffect;

    public bool collected { get; private set; } = false;

    [SerializeField] private FruitIndicator indicator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var collectible= collision.gameObject.GetComponent<CollectibleFruit>();
        

        if (collision.gameObject.CompareTag("Collectible") && !collected && !collectible.wasCollected)
        {
            //collectionSoundEffect.Play();
            var anim = collision.gameObject.GetComponent<Animator>();
            anim.SetBool("Animate", false);
            anim.SetTrigger("Collect");

            collectible.wasCollected=true;
            Destroy(collision.gameObject, 0.5f);
            collected=indicator.addFrutto(collision.gameObject);
        }
    }
}
