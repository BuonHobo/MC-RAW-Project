using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FruitIndicator : MonoBehaviour
{
    [SerializeField] GameObject[] fruitIcons;
    [SerializeField] float distance;
    [SerializeField] Color inactive_color;
    private GameObject[] icons;
    private bool[] fruttipresi;
    private int frutti = 0;
    private int ottenuti = 0;
    // Start is called before the first frame update
    void Start()
    {
        frutti=fruitIcons.Length;
        fruttipresi=new bool[frutti];
        icons= new GameObject[frutti];

        for (int i=0; i<frutti;i++){
            fruttipresi[i]=false;

            icons[i]=Instantiate(fruitIcons[i],transform);
            icons[i].name=icons[i].name.Split('(')[0];
            icons[i].transform.position+= new Vector3(i*distance,0,0);
            icons[i].GetComponent<Image>().color=inactive_color;
        }
    }

    public bool addFrutto(GameObject frutto){

        ottenuti++;

        for (int i=0;i<frutti;i++){
            if (fruttipresi[i]) continue;

            if (icons[i].name==frutto.name){
                icons[i].GetComponent<Image>().color=Color.white;
                fruttipresi[i]=true;
            }
        }

        return frutti==ottenuti;
    }

}
