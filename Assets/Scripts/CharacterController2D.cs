using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private float dampingFactor;
    private Rigidbody2D _characterRigidbody2D;
    private void Awake()
    {
        if (_characterRigidbody2D == null) _characterRigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }

    public void Move(float xVelocity, float yVelocity)
    {
        var currentVelocity = _characterRigidbody2D.velocity;
        var xVelocityDiff = xVelocity - currentVelocity.x;
        var yVelocityDiff = yVelocity - currentVelocity.y;
        if (Mathf.Abs(xVelocityDiff) > 0.05 || Mathf.Abs(yVelocityDiff) > 0.05)
        {
            _characterRigidbody2D.velocity = new Vector2(currentVelocity.x + xVelocityDiff/dampingFactor, currentVelocity.y + yVelocityDiff/dampingFactor);
        }
        else if(xVelocity == 0 && yVelocity == 0)
        {
            _characterRigidbody2D.velocity = new Vector2(0f, 0f);
        }
        // _characterRigidbody2D.velocity = new Vector2(xVelocity, yVelocity);
        // Debug.Log("XV: " + xVelocity);
    }
}
