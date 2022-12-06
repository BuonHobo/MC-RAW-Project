using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Chronometer : MonoBehaviour
{
    public float time { get; private set; } = 0;
    private TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        int minutes = Mathf.FloorToInt(time / 60);
        float seconds = time % 60;
        text.SetText(minutes.ToString("D2") + ":" + seconds.ToString("00.000"));
    }
}
