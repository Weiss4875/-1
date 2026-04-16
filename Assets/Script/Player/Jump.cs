using UnityEngine;

public class Jump : MonoBehaviour
{
    public float JumpVelocity = 5f;
    private Rigidbody2D _rigidbody2D;
    private bool JumpRequest = false;
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            JumpRequest = true;
        }
    }
    private void FixedUpdate()
    {
        if (JumpRequest)
        {
            _rigidbody2D.AddForce(Vector2.up * JumpVelocity, ForceMode2D.Impulse);
            JumpRequest = false ;
        }
    }
}
