using System.Collections;
using UnityEngine;

public class BossLaserAttack : MonoBehaviour
{
    public GameObject warningPrefab;
    public GameObject laserPrefab;
    public Transform player;

    public float warningTime = 2f;
    public float laserDuration = 1f;

    [Header("³s®g³]©w")]
    public int burstCount = 8;
    public float burstStagger = 0.3f; 
    public float cooldown = 20f;

    void Start() => StartCoroutine(LaserLoop());

    IEnumerator LaserLoop()
    {
        while (true)
        {
            for (int i = 0; i < burstCount; i++)
            {
                StartCoroutine(FireLaser(i * burstStagger));
            }

       
            float totalDuration = burstStagger * (burstCount - 1)
                                  + warningTime + laserDuration;
            yield return new WaitForSeconds(totalDuration + cooldown);
        }
    }

    IEnumerator FireLaser(float delay)
    {
        if (delay > 0f)
            yield return new WaitForSeconds(delay);

   
        Vector3 spawnPos = player.position;
        Quaternion downRotation = Quaternion.Euler(0, 0, 0);

        GameObject warning = Instantiate(warningPrefab, spawnPos, downRotation);
        yield return new WaitForSeconds(warningTime);
        Destroy(warning);

        GameObject laser = Instantiate(laserPrefab, spawnPos, downRotation);
        yield return new WaitForSeconds(laserDuration);
        Destroy(laser);
    }
}