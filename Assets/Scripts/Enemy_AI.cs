using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour {
    public float UP_AND_DOWN;
    public float LEFT;
    public float RIGHT;
    public float speed;
    public float death_size;
    public float death_speed;
    public Sprite Explosion;
    public GameObject Player;
    public GameObject Sight;
    public Transform target;
    public GameObject Enemy_Missles;
    public int Shoot_Speed;
    public int rotation_speed;
    public GameObject Ammo;

    private SpriteRenderer SR;
    private CapsuleCollider2D Coll;
    private bool up_or_down;
    private int n;
    private int b;
    private int life;
    private bool dead;
    private bool in_radius;
    private int k = 0;
    private int l = 0;
    private bool found;
    private bool at_start;
    private Vector3 start_position;
    private Quaternion start_rotation;

    // Use this for initialization
    void Start () {
        in_radius = false;
        up_or_down = true;
        at_start = false;
        life = 3;
        SR = gameObject.GetComponent<SpriteRenderer>();
        Coll = gameObject.GetComponent<CapsuleCollider2D>();
        start_position = transform.position;
        start_rotation = transform.rotation;
    }
	
	// Update is called once per frame
	void Update () {
        in_radius = Sight.GetComponentInChildren<Sight>().in_radius;
        if(up_or_down == true && dead == false && in_radius == false && found==false)
        {
            transform.Translate(0, speed*Time.deltaTime, 0);
            n++;
            if (n == UP_AND_DOWN)
            {
                up_or_down = false;
                n = 0;
            }
        }
        if(up_or_down==false && dead == false && in_radius == false && found==false)
        {
            transform.Rotate(0, 0, 1);
            b++;
            if (b == 180)
            {
                b = 0;
                up_or_down = true;
            }
        }
        if(in_radius == true)
        {
            found = true;
            n = 0;
            b = 0;
            var look_at_player = Quaternion.LookRotation(transform.position - Player.transform.position, Vector3.forward);
            look_at_player.x = 0;
            look_at_player.y = 0;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, look_at_player, rotation_speed*Time.deltaTime);
            l++;
            if (l == Shoot_Speed)
            {
                Shoot();
                l = 0;
            }

        }
        if (found == true && in_radius == false)
        {
            if (at_start == false)
            {
                var look_at_start = Quaternion.LookRotation(transform.position-start_position, Vector3.forward);
                look_at_start.x = 0;
                look_at_start.y = 0;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, look_at_start, rotation_speed*Time.deltaTime);
                transform.position = Vector3.MoveTowards(transform.position, start_position, speed*Time.deltaTime);
            }

            if (transform.position == start_position)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, start_rotation, rotation_speed*Time.deltaTime);
                at_start = true;
                if(transform.rotation==start_rotation)
                {
                    found = false;
                    at_start = false;
                }
            }
        }
        if (life == 0)
        {
            Coll.size = new Vector2(4, 4);
            transform.gameObject.tag = "Explosion";
            SR.sprite = Explosion;
            GetComponent<Collider2D>().isTrigger = true;
            dead = true;
        }
        if (dead == true)
        {
            transform.localScale+= new Vector3(1/death_size,1/death_size,0);
            k++;
            if(k == death_speed)
            {
                Instantiate(Ammo, transform.localPosition, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D missle)
    {
        if(missle.gameObject.tag == "Missle")
        {
            life--;
        }
        if (missle.gameObject.tag == "Explosion")
        {
            life--;
        }
    }
    private void Shoot()
    {
        Instantiate(Enemy_Missles, transform.localPosition, transform.localRotation);
    }
}
