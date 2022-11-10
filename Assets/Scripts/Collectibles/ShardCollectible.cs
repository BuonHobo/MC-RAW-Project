using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShardCollectible : MonoBehaviour
{
    [SerializeField] float respawn_time;
    private int size_int = 1;
    private static readonly System.Random rnd = new System.Random();

    public void startTimer()
    {
        gameObject.SetActive(false);
        Invoke("backOn", respawn_time);
    }

    void backOn()
    {
        gameObject.SetActive(true);
        gameObject.transform.localScale = (new Vector3(15, 15, 15));
        gameObject.LeanScale(new Vector3(7, 7, 1), 1).setEaseOutExpo();
    }

    void Update()
    {
        if (gameObject.LeanIsTweening())
        {
            return;
        }
        else
        {   
            size_int=rnd.Next(0,3);
            gameObject.LeanScale(new Vector3(7+size_int,7+size_int,1),2);
        }   
    }
}
