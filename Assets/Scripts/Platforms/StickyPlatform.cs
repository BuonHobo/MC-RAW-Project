using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyPlatform : MonoBehaviour
{
    private WaypointFollower wf;
    private void Start()
    {
        wf = GetComponent<WaypointFollower>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(transform);
            collision.gameObject.SendMessage("OnPlatform", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(null);
            collision.gameObject.SendMessage("OnPlatform", false);
            collision.gameObject.GetComponent<Rigidbody2D>().velocity += new Vector2(wf.velocity.x, wf.velocity.y) * Time.deltaTime;
        }
    }
}
