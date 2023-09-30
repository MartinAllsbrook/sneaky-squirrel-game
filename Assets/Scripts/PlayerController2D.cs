using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController2D : MonoBehaviour
{
    public static PlayerController2D Instance;

    public UnityEvent CatnipEaten;
    
    [SerializeField] private CharacterController2D playerCharacterController2D;
    [SerializeField] private Animator playerAnimator;
    
    [SerializeField] private float speedMultiplier;

    private float _horizontalMove;
    private float _verticalMove;

    private void Awake()
    {
        if (Instance == null) Instance = this;

        if (CatnipEaten == null) CatnipEaten = new UnityEvent();
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
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Catnip"))
        {
            Destroy(col.gameObject);
            UIController.Instance.AddScore();
            CatnipEaten.Invoke();
            UIController.Instance.ChangeHealth(10);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Missile"))
        {
            Debug.Log("HIT");
            UIController.Instance.ChangeHealth(-50);
        }
    }
}
