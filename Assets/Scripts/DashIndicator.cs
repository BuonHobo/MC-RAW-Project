using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashIndicator : MonoBehaviour
{

    [SerializeField] GameObject player;
    [SerializeField] GameObject dash_icon;
    DashController dash_controller;
    [SerializeField] float distance = 50;
    GameObject[] icons;
    int active_icons;
    // Start is called before the first frame update
    void Start()
    {
        dash_controller = player.GetComponent<DashController>();
        active_icons = dash_controller.dashes;
        float offset = distance * (active_icons - 1) / 2f;
        icons = new GameObject[active_icons];
        for (int i = 0; i < active_icons; i++)
        {
            icons[i] = Instantiate(dash_icon, transform);
            icons[i].transform.position += new Vector3(i * distance - offset, 0, 0);
            Debug.Log("Instantiating " + i);
        }
    }

    public void consumeDash()
    {
        active_icons--;
        icons[active_icons].SetActive(false);

        for (int i = 0; i < icons.Length; i++)
        {

            icons[i].transform.position += new Vector3(distance / 2, 0, 0);

            Debug.Log("Translated");
        }
    }

    public void restoreDash()
    {
        icons[active_icons].SetActive(true);
        active_icons++;

        for (int i = 0; i < icons.Length; i++)
        {

            icons[i].transform.position -= new Vector3(distance / 2, 0, 0);

            Debug.Log("Translated");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
