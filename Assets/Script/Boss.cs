using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject ammo1;
   
    void Start()
    {
        
    }
    public void Attack1_ShootRandom()
    {
        int bulletCount = 16; 
        for (int i = 0; i < bulletCount; i++)
        {
            
            float angle = Random.Range(0f, 360f);
            Vector2 dir = Quaternion.Euler(0, 0, angle) * Vector2.up;

            GameObject bullet = Instantiate(ammo1, transform.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().SetDirection(dir);
        }
    }
    void Update()
    {
        
    }
}
