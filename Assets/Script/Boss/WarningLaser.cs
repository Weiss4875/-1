using System.Collections;
using UnityEngine;

public class WarningFade : MonoBehaviour
{
    public SpriteRenderer sr;
    public float targetAlpha = 0.3f; 
    public float fadeSpeed = 1f;   

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        Color c = sr.color;
        c.a = 0f;
        sr.color = c;

        while (c.a < targetAlpha)
        {
            c.a += Time.deltaTime * fadeSpeed;
            c.a = Mathf.Min(c.a, targetAlpha); 
            sr.color = c;
            yield return null;
        }
    }
}