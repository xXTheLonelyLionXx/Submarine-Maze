using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour {
    // Use this for initialization
    void Start()
    {
        transform.parent = GameObject.FindGameObjectWithTag("Player").transform;
    }
	// Update is called once per frame
	void Update () {
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            transform.up = collision.transform.localPosition - transform.position;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
        if(other.gameObject.tag == "Explosion")
        {
            Destroy(gameObject);
        }
    }
}
