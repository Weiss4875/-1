using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject ammo1;

    public float fireInterval = 0.2f;
    public int bulletCount = 16;

    void Start()
    {
        StartCoroutine(Attack1_ShootRandom());
    }

    IEnumerator Attack1_ShootRandom()
    {
        while (true)
        {
            float angle = Random.Range(0f, 360f);
            Vector2 dir = Quaternion.Euler(0, 0, angle) * Vector2.up;

            GameObject bullet = Instantiate(ammo1, transform.position, Quaternion.identity);
            bullet.GetComponent<Bullet1>().SetDirection(dir);

            yield return new WaitForSeconds(fireInterval);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Attack1_ShootRandom());
        }
    }
}