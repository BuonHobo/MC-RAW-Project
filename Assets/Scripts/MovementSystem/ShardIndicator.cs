using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShardIndicator : MonoBehaviour
{

    [SerializeField] GameObject shard_icon;
    [SerializeField] float distance = 50;
    [SerializeField] float animation_time;
    ShardController shard_controller;
    GameObject[] icons;
    Vector2[] icons_pos;

    RectTransform[] icons_opacity;
    int active_icons;
    void InstantiateBalls()
    {
        icons = new GameObject[active_icons];
        icons_pos = new Vector2[active_icons];
        icons_opacity = new RectTransform[active_icons];

        float offset = distance * (active_icons - 1) / 2f;

        for (int i = 0; i < active_icons; i++)
        {
            icons[i] = Instantiate(shard_icon, transform);
            icons[i].transform.position += new Vector3(i * distance - offset, 0, 0);
            icons_pos[i] = new(icons[i].transform.position.x, icons[i].transform.position.y);
            icons_opacity[i] = icons[i].GetComponent<RectTransform>();
        }
    }
    public void Init(ShardController ctx)
    {
        shard_controller = ctx;
        active_icons = shard_controller.max_shards;

        InstantiateBalls();
    }

    public void consumeShard()
    {
        UpdateState(true);
    }

    public void restoreShard()
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

        icons_opacity[active_icons].LeanAlpha(target_alpha, animation_time);

        icons_pos[active_icons].y += distance * dirY;
        icons[active_icons].LeanMove(icons_pos[active_icons], animation_time);

        for (int i = 0; i < icons.Length; i++)
        {
            if (i == active_icons) { continue; }

            icons_pos[i].x += distance * 0.5f * dirX;
            icons[i].LeanMove(icons_pos[i], animation_time).setEaseInQuad();

        }

        if (!consuming)
        {
            active_icons++;
        }
    }
}
