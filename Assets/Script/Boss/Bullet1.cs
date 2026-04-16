using UnityEngine;

public class Bullet1 : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 moveDir;

    public void SetDirection(Vector2 dir)
    {
        moveDir = dir.normalized;
    }

    void Update()
    {
        transform.Translate(moveDir * speed * Time.deltaTime, Space.World);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}