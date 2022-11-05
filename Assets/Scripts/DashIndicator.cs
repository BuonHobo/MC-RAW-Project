using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DashIndicator : MonoBehaviour
{

    [SerializeField] GameObject dash_icon;
    [SerializeField] float distance = 50;
    DashController dash_controller;
    GameObject[] icons;
    Vector2[] icons_pos;

    RectTransform[] icons_opacity;
    int active_icons;
    float dash_duration;

    void InstantiateBalls()
    {
        icons = new GameObject[active_icons];
        icons_pos = new Vector2[active_icons];
        icons_opacity = new RectTransform[active_icons];

        float offset = distance * (active_icons - 1) / 2f;

        for (int i = 0; i < active_icons; i++)
        {
            icons[i] = Instantiate(dash_icon, transform);
            icons[i].transform.position += new Vector3(i * distance - offset, 0, 0);
            icons_pos[i] = new(icons[i].transform.position.x, icons[i].transform.position.y);
            icons_opacity[i] = icons[i].GetComponent<RectTransform>();
        }
    }
    public void Init(DashController ctx)
    {
        dash_controller = ctx;
        dash_duration = dash_controller.dash_duration;
        active_icons = dash_controller.dashes;

        InstantiateBalls();
    }

    public void consumeDash()
    {
        UpdateState(true);
    }

    public void restoreDash()
    {
        UpdateState(false);
    }

    // Update is called once per frame
    void UpdateState(bool consuming)
    {
        float dirX = -1;
        float target_alpha = 1;

        if (consuming)
        {
            active_icons--;
            dirX = 1;
            target_alpha = 0;
        }
        float dirY = -dirX;

        icons_opacity[active_icons].LeanAlpha(target_alpha, dash_duration);

        icons_pos[active_icons].y += distance * dirY;
        icons[active_icons].LeanMove(icons_pos[active_icons], dash_duration);

        for (int i = 0; i < icons.Length; i++)
        {
            if (i == active_icons) { continue; }

            icons_pos[i].x += distance * 0.5f * dirX;
            icons[i].LeanMove(icons_pos[i], dash_duration).setEaseInQuad();

        }

        if (!consuming)
        {
            active_icons++;
        }
    }
}
