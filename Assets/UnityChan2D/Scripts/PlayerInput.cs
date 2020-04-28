using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : IPlayerInput
{
    [SerializeField]private float _x;
    [SerializeField]private bool _jump;

    public void Inputting()
    {
        _x = Input.GetAxis("Horizontal");
        _jump = Input.GetButtonDown("Jump");
    }

    public float X
    {
        get { return this._x; }
        private set { this._x = value; }
    }

    public bool Jump
    {
        get { return this._jump; }
        private set { this._jump = value; }
    }
}
