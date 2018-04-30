using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    public float vertical_speed;
    public float horizontal_speed;
    public float rb2d_speed;
    public int Text_Time;
    public GameObject Missles;
    public Transform Submarine_Position;
    public GameObject Health;
    public Sprite Health3;
    public Sprite Health2;
    public Sprite Health1;
    public Text Enemies;
    public Text Message;
    public GameObject Missle_1;
    public GameObject Missle_2;
    public GameObject Missle_3;
    public GameObject Missle_4;
    public GameObject Missle_5;

    private Rigidbody2D rb2d;
    private int life = 3;
    private Image Health_Image;
    private int Enemies_Start;
    private int Enemies_Active;
    private string Enemies_Start_Text;
    private string Enemies_Active_Text;
    private bool Message_In_or_Out;
    private int Fade;
    private int ammo;

    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        Health_Image = Health.GetComponent<Image>();
        Enemies_Start = GameObject.FindGameObjectsWithTag("Enemy").Length;
        Message_In_or_Out = true;
        ammo = 5;
	}
	
	// Update is called once per frame
	void Update () {
        if(ammo > 5)
        {
            ammo = 5;
        }
        if(ammo == 5)
        {
            Missle_1.SetActive(true);
            Missle_2.SetActive(true);
            Missle_3.SetActive(true);
            Missle_4.SetActive(true);
            Missle_5.SetActive(true);
        } else if (ammo == 4)
        {
            Missle_1.SetActive(true);
            Missle_2.SetActive(true);
            Missle_3.SetActive(true);
            Missle_4.SetActive(true);
            Missle_5.SetActive(false);
        } else if (ammo == 3)
        {
            Missle_1.SetActive(true);
            Missle_2.SetActive(true);
            Missle_3.SetActive(true);
            Missle_4.SetActive(false);
            Missle_5.SetActive(false);
        } else if (ammo == 2)
        {
            Missle_1.SetActive(true);
            Missle_2.SetActive(true);
            Missle_3.SetActive(false);
            Missle_4.SetActive(false);
            Missle_5.SetActive(false);
        } else if (ammo == 1)
        {
            Missle_1.SetActive(true);
            Missle_2.SetActive(false);
            Missle_3.SetActive(false);
            Missle_4.SetActive(false);
            Missle_5.SetActive(false);
        } else if (ammo == 0)
        {
            Missle_1.SetActive(false);
            Missle_2.SetActive(false);
            Missle_3.SetActive(false);
            Missle_4.SetActive(false);
            Missle_5.SetActive(false);
        }

        Enemies_Active = GameObject.FindGameObjectsWithTag("Enemy").Length;
        SetEnemyText();
        transform.Rotate(0,0,-Input.GetAxis("Horizontal")/horizontal_speed);
        transform.Translate(0, Input.GetAxis("Vertical")/vertical_speed, 0);

        if (life == 3)
        {
            Health_Image.sprite = Health3;
        } else if (life == 2)
        {
            Health_Image.sprite = Health2;
        }
        else if (life == 1)
        {
            Health_Image.sprite = Health1;
        }

        if(Input.GetKeyDown(KeyCode.Space) && ammo > 0)
        {
            Shoot();
            ammo--;
        }
	}

    private void FixedUpdate()
    {
        Vector2 movement = new Vector2(0, Input.GetAxis("Vertical"));
        rb2d.AddRelativeForce(movement * rb2d_speed);
    }

    private void Shoot()
    {
        Instantiate(Missles, Submarine_Position.position, Submarine_Position.rotation);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Explosion")
        {
            life--;
        }
        if (other.gameObject.tag == "Enemy_Missle")
        {
            life--;
        }
        if(other.gameObject.tag == "Item_Missle")
        {
            ammo += 3;
        }
    }
    private void SetEnemyText()
    {
        if(Enemies_Start-Enemies_Active == Enemies_Start)
        {
            Enemies.text = "All targets elliminated.";
            Message.text = "GATE OPEN";
            if(Message_In_or_Out == true)
            {
                Color Mess_Col = Message.color;
                Mess_Col.a += 0.01f;
                Message.color = Mess_Col;
                Fade++;
                if(Fade == 100)
                {
                    Message_In_or_Out = false;
                    Fade = 0;
                }
            }
            if (Message_In_or_Out == false)
            {
                Color Mess_Col = Message.color;
                Mess_Col.a -= 0.01f;
                Message.color = Mess_Col;
                Fade++;
                if (Fade == 100)
                {
                    Message_In_or_Out = true;
                    Fade = 0;
                }
            }
        }
        if(Enemies_Start-Enemies_Active < Enemies_Start)
        {
            if(Enemies_Start < 10)
            {
                Enemies_Start_Text = "0" + Enemies_Start;
            }
            else
            {
                Enemies_Start_Text = Enemies_Start.ToString();
            }
            if (Enemies_Start - Enemies_Active < 10)
            {
                Enemies_Active_Text = "0" + (Enemies_Start - Enemies_Active);
            }
            else
            {
                Enemies_Active_Text = Enemies_Active.ToString();
            }
            Enemies.text = "Enemies " + Enemies_Active_Text + "/" + Enemies_Start_Text;
        }
    }
}
