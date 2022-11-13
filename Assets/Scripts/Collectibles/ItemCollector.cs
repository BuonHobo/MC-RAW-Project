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
            Destroy(collision.gameObject);
            indicator.gameObject.SetActive(true);
        }
    }
}
