using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLogic : MonoBehaviour
{
    public static TimeLogic instance;
    [SerializeField] private Sprite[] all;

    [SerializeField] private SpriteRenderer spriteRenderer;

    private int num = 0;
    private int count = 0;

    private void Start()
    {
        instance = this;

        StartCoroutine(Rotate());
    }

    private IEnumerator Rotate()
    {
        if (count < 5)
        {
            yield return new WaitForSeconds(Random.Range(0.1f, 0.3f));
            num++;
            if (num >= all.Length + 1)
            {
                num = 0;
            }
            spriteRenderer.sprite = all[num];

            StartCoroutine(Rotate());
            count++;
        }

        spriteRenderer.sprite = all[Random.Range(0, all.Length)];
    }


    public void Stop()
    {
        StopAllCoroutines();
    }
}
