using System.Collections;
using UnityEngine;

public class BossRotatingLaser : MonoBehaviour
{
    [Header("References")]
    public GameObject warningPrefab;
    public GameObject laserPrefab;

    [Header("Timing")]
    public float initialDelay = 30f;
    public float warningDuration = 5f;
    public float pauseBeforeFire = 2f;
    public float laserDuration = 4f;
    public float cooldown = 10f;

    [Header("Rotation")]
    public float initialSpeed = 180f;
    public float finalSpeed = 0f;

    private GameObject rotationCenter;

    void Start()
    {
        rotationCenter = new GameObject("LaserRotationCenter");
        rotationCenter.transform.position = transform.position;
        StartCoroutine(LaserLoop());
    }

    IEnumerator LaserLoop()
    {
        yield return new WaitForSeconds(initialDelay); 
        while (true)
        {
            yield return StartCoroutine(FireRotatingLaser());
            yield return new WaitForSeconds(cooldown);
        }
    }

    IEnumerator FireRotatingLaser()
    {
        rotationCenter.transform.position = transform.position;
        rotationCenter.transform.rotation = Quaternion.identity;

        // 生成 8 道警示條
        GameObject[] warnings = new GameObject[8];
        for (int i = 0; i < 8; i++)
        {
            float angle = i * 45f;
            Quaternion rot = Quaternion.Euler(0, 0, angle);
            warnings[i] = Instantiate(warningPrefab, transform.position, rot);
            warnings[i].transform.SetParent(rotationCenter.transform);
        }

        // 越轉越慢
        float elapsed = 0f;
        while (elapsed < warningDuration)
        {
            float t = elapsed / warningDuration;
            float currentSpeed = Mathf.Lerp(initialSpeed, finalSpeed, t);
            rotationCenter.transform.Rotate(0, 0, currentSpeed * Time.deltaTime);
            elapsed += Time.deltaTime;
            yield return null;
        }

     
        yield return new WaitForSeconds(pauseBeforeFire);

       
        foreach (var w in warnings)
            Destroy(w);

     
        GameObject[] lasers = new GameObject[8];
        for (int i = 0; i < 8; i++)
        {
            float angle = i * 45f;
            Quaternion rot = rotationCenter.transform.rotation
                             * Quaternion.Euler(0, 0, angle);
            lasers[i] = Instantiate(laserPrefab, transform.position, rot);
            lasers[i].transform.SetParent(rotationCenter.transform);
        }

        yield return new WaitForSeconds(laserDuration);

        foreach (var l in lasers)
            Destroy(l);

        rotationCenter.transform.rotation = Quaternion.identity;
    }
}