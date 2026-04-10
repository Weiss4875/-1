using UnityEngine;
public class JumpBox : MonoBehaviour
{
    [Range(0, 10)] 
    public float jumpVelocity = 5f;
    public LayerMask mask;
    public float boxHeight;

    private Vector2 playerSize;
    private Vector2 boxSize; 

    private bool JumpRequest = false; 
    private bool grounded = false; 

    private Rigidbody2D _rigidbody2D;
    void Start()
    {   
        _rigidbody2D = GetComponent<Rigidbody2D>();
        playerSize = GetComponent<SpriteRenderer>().bounds.size; 
        boxSize = new Vector2(playerSize.x * 0.8f, boxHeight);
    }
    void Update()
    { 
        if (Input.GetButtonDown("Jump") && grounded)
        {
            JumpRequest = true;
        } 
    }
    private void FixedUpdate()
    {
        if (JumpRequest) 
        {
            _rigidbody2D.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
            JumpRequest = false; grounded = false;
        }
          else
        {
            Vector2 boxCenter = (Vector2)transform.position + (Vector2.down * playerSize.y * 0.5f);
        if (Physics2D.OverlapBox(boxCenter, boxSize, 0, mask)) 
            {
                grounded = true; 
            }
            else 
            {
                grounded = false;
            }
    }
}
    private void OnDrawGizmos() 
    {
        if (grounded)
        {
            Gizmos.color = Color.red;
        } else 
        {
            Gizmos.color = Color.green; 
        }
        Vector2 boxCenter = (Vector2)transform.position + (Vector2.down * playerSize.y * 0.07f);
        Gizmos.DrawWireCube(boxCenter, boxSize); 
    }
}