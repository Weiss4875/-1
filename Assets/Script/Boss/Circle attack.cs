using System.Collections;
using UnityEngine;

public class BossCircleAttack : MonoBehaviour
{
    [Header("References")]
    public GameObject warningPrefab;
    public GameObject attackPrefab;
    public Transform player;

    [Header("Timing")]
    public float interval = 5f;
    public float warningTime = 0.5f;
    public float attackDuration = 1f; // 手動填你的爆炸動畫長度

    void Start()
    {
        StartCoroutine(CircleLoop());
    }

    IEnumerator CircleLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            yield return StartCoroutine(FireCircle());
        }
    }

    IEnumerator FireCircle()
    {
        Vector3 spawnPos = player.position;

        GameObject warning = Instantiate(warningPrefab, spawnPos, Quaternion.identity);
        yield return new WaitForSeconds(warningTime);
        Destroy(warning);

        GameObject attack = Instantiate(attackPrefab, spawnPos, Quaternion.identity);
        yield return new WaitForSeconds(attackDuration);
        Destroy(attack);
    }
}