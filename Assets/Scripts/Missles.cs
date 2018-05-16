using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missles : MonoBehaviour {

    public float speed;
    public Sprite Explosion;
    public float death_size;
    public float death_speed;

    private CapsuleCollider2D Coll;
    private SpriteRenderer SR;
    private bool explodes;
    private int k;

    // Use this for initialization
    void Start () {
        Coll = gameObject.GetComponent<CapsuleCollider2D>();
        SR = gameObject.GetComponent<SpriteRenderer>();
        explodes = false;
    }
	
	// Update is called once per frame
	void Update () {
        if(explodes == false)
        {
            transform.Translate(0, 1 * speed, 0);
        }

        if(explodes == true)
        {
            transform.localScale += new Vector3(1 / death_size, 1 / death_size, 0);
            k++;
            if (k == death_speed)
            {
                Destroy(gameObject);
            }
        }
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        if (tag == "Missle" && other.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
        if (tag == "Enemy_Missle" && other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "Wall")
        {
            Coll.size = new Vector2(4, 4);
            transform.gameObject.tag = "Explosion";
            SR.sprite = Explosion;
            explodes = true;
        }
    }
}
