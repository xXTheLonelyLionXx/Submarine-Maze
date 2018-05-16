using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public GameObject Player;

    private Vector3 _offset;

    // Use this for initialization
    void Start()
    {
        _offset = transform.position - Player.transform.position;
    }

    void Update()
    {
    }

    // Update is called once per frame
    void LateUpdate()
    {

        if (Player == null)
        {
        }
        else
        {
            transform.position = Player.transform.position + _offset;
        }
    }
}
