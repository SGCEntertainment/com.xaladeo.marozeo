using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Row : MonoBehaviour
{
    private int randomValue;
    private float timeInterval;

    public bool rowStopped;
    public string stoppedSlot;

    private void Start()
    {
        rowStopped = true;
        SlotMachineScript.HandlePulled += StartRotating;
    }
    public void StartRotating()
    {
        stoppedSlot = "";
        StartCoroutine("Rotate");
    }

    private IEnumerator Rotate()
    {
        rowStopped = false;
        timeInterval = 0.025f;

        for (int i = 0; i < 30; i++)
        {
            if (transform.position.y <= 0)
                transform.position = new Vector2(transform.position.x, 7.5f);

            transform.position = new Vector2(transform.position.x, transform.position.y - 1.5f);

            yield return new WaitForSeconds(timeInterval);
        }

        randomValue = Random.Range(60, 100);

        switch (randomValue % 3)
        {
            case 1:
                randomValue += 2;
                break;
            case 2:
                randomValue += 1;
                break;
        }
        for (int i = 0; i < randomValue; i++)
        {
            if (transform.position.y <= 0f)
                transform.position = new Vector2(transform.position.x, 7.5f);

            transform.position = new Vector2(transform.position.x, transform.position.y - 1.5f);

            if (i > Mathf.RoundToInt(randomValue * 0.25f))
                timeInterval = 0.05f;
            if (i > Mathf.RoundToInt(randomValue * 0.5f))
                timeInterval = 0.1f;
            if (i > Mathf.RoundToInt(randomValue * 0.75f))
                timeInterval = 0.15f;
            if (i > Mathf.RoundToInt(randomValue * 0.95f))
                timeInterval = 0.2f;
            yield return new WaitForSeconds(timeInterval);

        }

        //if (transform.position.y == 0f)
        //    stoppedSlot = "7";
        //else if (transform.position.y == -1.5f)
        //    stoppedSlot = "RDiamond";
        //else if (transform.position.y == -3f)
        //    stoppedSlot = "Cherry";
        //else if (transform.position.y == -4.5f)
        //    stoppedSlot = "Banana";
        //else if (transform.position.y == -6f)
        //    stoppedSlot = "Melon";
        //else if (transform.position.y == -7.5f)
        //    stoppedSlot = "Diamond";
        //else if (transform.position.y == 9f)
        //    stoppedSlot = "GDiamond";
        //else if (transform.position.y == 10.5f)
        //    stoppedSlot = "Ananas";

        rowStopped = true;
    }
    private void OnDestroy()
    {
        SlotMachineScript.HandlePulled -= StartRotating;
    }
}
