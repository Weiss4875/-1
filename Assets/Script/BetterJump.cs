using UnityEngine;

public class BetterJump : MonoBehaviour
{
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    private Rigidbody2D _rigidbody2D;
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_rigidbody2D.linearVelocity.y < 0)
        {
            _rigidbody2D.gravityScale = fallMultiplier;
        }
        else if (_rigidbody2D.linearVelocity.y > 0 && !Input.GetButton("Jump"))
        {
            _rigidbody2D.gravityScale = lowJumpMultiplier;
        }
        else
        {
            _rigidbody2D.gravityScale = 1f;
        }
    }
}
