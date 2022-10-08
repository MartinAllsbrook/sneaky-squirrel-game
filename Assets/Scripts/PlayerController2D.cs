using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    public static PlayerController2D Instance;
    
    [SerializeField] private CharacterController2D playerCharacterController2D;
    [SerializeField] private Animator playerAnimator;
    
    [SerializeField] private float speedMultiplier;
    
    private float _horizontalMove = 0;
    private float _verticalMove = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Update()
    {
        _horizontalMove = Input.GetAxis("Horizontal") * speedMultiplier;
        _verticalMove = Input.GetAxis("Vertical") * speedMultiplier;
        if (Mathf.Abs(_horizontalMove) > Mathf.Abs(_verticalMove))
        {
            playerAnimator.SetFloat("Horizontal Velocity", _horizontalMove);
            playerAnimator.SetFloat("Vertical Velocity", 0);
        }
        else if (Mathf.Abs(_horizontalMove) < Mathf.Abs(_verticalMove))
        {
            playerAnimator.SetFloat("Horizontal Velocity", 0);
            playerAnimator.SetFloat("Vertical Velocity", _verticalMove);
        }
        else if (Math.Abs(_horizontalMove) == 0 && Mathf.Abs(_verticalMove) == 0)
        {
            playerAnimator.SetFloat("Horizontal Velocity", 0);
            playerAnimator.SetFloat("Vertical Velocity", 0);
        }
    }

    private void FixedUpdate()
    {
        playerCharacterController2D.Move(_horizontalMove, _verticalMove);
    }
}
