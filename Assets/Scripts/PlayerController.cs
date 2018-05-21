using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    public float VerticalSpeed;
    public float HorizontalSpeed;
    public float Rb2DSpeed;
    public int TextTime;
    public GameObject Missles;
    public Transform SubmarinePosition;
    public GameObject Health;
    public Sprite Health3;
    public Sprite Health2;
    public Sprite Health1;
    public Text Enemies;
    public Text Timer;
    public Text Message;

    public GameObject[] MissileCount;

    private Rigidbody2D _rb2d;
    private int _life = 3;
    private Image _healthImage;
    private int _enemiesStart;
    private int _enemiesActive;
    private string _enemiesStartText;
    private string _enemiesActiveText;
    private bool _messageInOrOut;
    private int _fade;
    private int _ammo;
    private float _time;
    private float _time_minutes;
    private float _time_seconds;
    private string _minutes;
    private string _seconds;



    // Use this for initialization
    void Start () {
        _rb2d = GetComponent<Rigidbody2D>();
        _healthImage = Health.GetComponent<Image>();
        _enemiesStart = GameObject.FindGameObjectsWithTag("Enemy").Length;
        _messageInOrOut = true;
        _ammo = 5;
        _time = 180 - Mathf.Round(Time.time);
	}
	
	// Update is called once per frame
	void Update () {
        if(_ammo > 5)
        {
            _ammo = 5;
        }

        for(int i = 0; i < MissileCount.Length; i++)
        {
            MissileCount[i].SetActive(i <= _ammo - 1);
        }

        //UI_Texts Update
        _enemiesActive = GameObject.FindGameObjectsWithTag("Enemy").Length;
        SetEnemyText();
        _time = 180 - Mathf.Round(Time.time);
        SetTimeText();

        //Controls
        transform.Rotate(0,0,-Input.GetAxis("Horizontal")/HorizontalSpeed);
        transform.Translate(0, Input.GetAxis("Vertical")/VerticalSpeed, 0);

        if (_life == 3)
        {
            _healthImage.sprite = Health3;
        } else if (_life == 2)
        {
            _healthImage.sprite = Health2;
        }
        else if (_life == 1)
        {
            _healthImage.sprite = Health1;
        }

        if(Input.GetKeyDown(KeyCode.Space) && _ammo > 0)
        {
            Shoot();
            _ammo--;
        }
	}

    private void FixedUpdate()
    {
        //Physics
        Vector2 movement = new Vector2(0, Input.GetAxis("Vertical"));
        _rb2d.AddRelativeForce(movement * Rb2DSpeed);
    }

    //Create Missle once shot
    private void Shoot()
    {
        Instantiate(Missles, SubmarinePosition.position, SubmarinePosition.rotation);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Life lost
        if(other.gameObject.tag == "Explosion")
        {
            _life--;
        }
        if (other.gameObject.tag == "Enemy_Missle")
        {
            _life--;
        }
        //Ammo gained
        if(other.gameObject.tag == "Item_Missle")
        {
            _ammo += 3;
        }
    }

    //UI_Texts
    private void SetEnemyText()
    {
        if(_enemiesStart-_enemiesActive == _enemiesStart)
        {
            Enemies.text = "All targets elliminated.";
            Message.text = "GATE OPEN";
            if(_messageInOrOut == true)
            {
                Color Mess_Col = Message.color;
                Mess_Col.a += 0.01f;
                Message.color = Mess_Col;
                _fade++;
                if(_fade == 100)
                {
                    _messageInOrOut = false;
                    _fade = 0;
                }
            }
            if (_messageInOrOut == false)
            {
                Color Mess_Col = Message.color;
                Mess_Col.a -= 0.01f;
                Message.color = Mess_Col;
                _fade++;
                if (_fade == 100)
                {
                    _messageInOrOut = true;
                    _fade = 0;
                }
            }
        }
        if(_enemiesStart-_enemiesActive < _enemiesStart)
        {
            if(_enemiesStart < 10)
            {
                _enemiesStartText = "0" + _enemiesStart;
            }
            else
            {
                _enemiesStartText = _enemiesStart.ToString();
            }
            if (_enemiesStart - _enemiesActive < 10)
            {
                _enemiesActiveText = "0" + (_enemiesStart - _enemiesActive);
            }
            else
            {
                _enemiesActiveText = _enemiesActive.ToString();
            }
            Enemies.text = "Enemies " + _enemiesActiveText + "/" + _enemiesStartText;
        }
    }
    private void SetTimeText()
    {
        _time_minutes = Mathf.Floor(_time / 60);
        _time_seconds = _time % 60;
        if(_time_minutes < 10)
        {
            _minutes = "0" + _time_minutes;
        }
        else
        {
            _minutes = _time_minutes.ToString();
        }
        if(_time_seconds < 10)
        {
            _seconds = "0" + _time_seconds;
        }
        else
        {
            _seconds = _time_seconds.ToString();
        }
        Timer.text = _minutes + ":" + _seconds;
    }
}
