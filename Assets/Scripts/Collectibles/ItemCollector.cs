using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    //[SerializeField] private Text cherriesText;

    //[SerializeField] private AudioSource collectionSoundEffect;

    public bool collected { get; private set; } = false;
    private bool canCollect=true;

    [SerializeField] private FruitIndicator indicator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectible") && !collected && canCollect)
        {
            //collectionSoundEffect.Play();
            var anim = collision.gameObject.GetComponent<Animator>();
            anim.SetBool("Animate", false);
            anim.SetTrigger("Collect");
            Destroy(collision.gameObject, 0.5f);
            collected=indicator.addFrutto(collision.gameObject);

            canCollect=false;
            Invoke("restoreCollect",0.1f);
        }
    }
    private void restoreCollect(){
        canCollect=true;
    }
}
