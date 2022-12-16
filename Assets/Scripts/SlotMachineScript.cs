using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class SlotMachineScript : MonoBehaviour
{
    public static SlotMachineScript instance;

    public static event Action HandlePulled = delegate { };

    [SerializeField]
    private TextMeshProUGUI priceText;

    [SerializeField]
    private Row[] rows;

    public float price;
    int rand;

    private bool resultChecked = false;
    private bool isGet = true;

    [SerializeField] private GameObject cardObj;
    [SerializeField] private GameObject sound, sound1, sound2;

    private void Start()
    {
        instance = this;
        priceText.text = price.ToString();
    }
    private void Update()
    {
        priceText.text = price.ToString();
        if (!rows[0].rowStopped || !rows[1].rowStopped || !rows[2].rowStopped)
        {
            resultChecked = false;
        }
        if (rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped)
        {
            if (!isGet)
            {
                price += UnityEngine.Random.Range(100, 300);
                isGet = true;
            }
        }
    }
    public void Rotate()
    {
        isGet = false;
        rand = UnityEngine.Random.Range(1, 10);
        if (rand == 1)
        {
            cardObj.SetActive(true);
        }
        Debug.Log(rand);

        Instantiate(sound, transform.position, Quaternion.identity);
    }
    private void CheckResults()
    {
        //// 3 совпадение
        //if (rows[0].stoppedSlot == "Diamond"
        //    && rows[0].stoppedSlot == "Diamond"
        //    && rows[0].stoppedSlot == "Diamond")

        //    price += 200;
        ////generationManager.ObjectGeneration();

        //else if (rows[0].stoppedSlot == "Banana"
        //    && rows[0].stoppedSlot == "Banana"
        //    && rows[0].stoppedSlot == "Banana")

        //    price += 200;
        ////generationManager.ObjectGeneration();

        //else if (rows[0].stoppedSlot == "GDiamond"
        //    && rows[0].stoppedSlot == "GDiamond"
        //    && rows[0].stoppedSlot == "GDiamond")

        //    price += 200;
        ////generationManager.ObjectGeneration();

        //else if (rows[0].stoppedSlot == "Ananas"
        //    && rows[0].stoppedSlot == "Ananas"
        //    && rows[0].stoppedSlot == "Ananas")

        //    price += 200;
        ////generationManager.ObjectGeneration();

        //else if (rows[0].stoppedSlot == "7"
        //    && rows[0].stoppedSlot == "7"
        //    && rows[0].stoppedSlot == "7")

        //    price += 200;
        ////generationManager.ObjectGeneration();

        //else if (rows[0].stoppedSlot == "Melon"
        //    && rows[0].stoppedSlot == "Melon"
        //    && rows[0].stoppedSlot == "Melon")

        //    price += 200;
        ////generationManager.ObjectGeneration();

        //else if (rows[0].stoppedSlot == "Cherry"
        //    && rows[0].stoppedSlot == "Cherry"
        //    && rows[0].stoppedSlot == "Cherry")

        //    price += 200;
        ////generationManager.ObjectGeneration();

        //else if (rows[0].stoppedSlot == "RDiamond"
        //    && rows[0].stoppedSlot == "RDiamond"
        //    && rows[0].stoppedSlot == "RDiamond")

        //    price += 200;
        ////generationManager.ObjectGeneration();

        //else if (rows[0].stoppedSlot == "GDiamond"
        //    && rows[0].stoppedSlot == "GDiamond"
        //    && rows[0].stoppedSlot == "GDiamond")

        //    price += 200;
        //    //generationManager.ObjectGeneration();

        //// допалнительные совпадение
        //else if (((rows[0].stoppedSlot == rows[1].stoppedSlot)
        //    && (rows[0].stoppedSlot == "7"))

        //    || ((rows[0].stoppedSlot == rows[2].stoppedSlot)
        //    && (rows[0].stoppedSlot == "7"))

        //    || ((rows[1].stoppedSlot == rows[2].stoppedSlot)
        //    && (rows[1].stoppedSlot == "Diamond")))

        //    price += 200;

        //else if (((rows[0].stoppedSlot == rows[1].stoppedSlot)
        //    && (rows[0].stoppedSlot == "Diamond"))

        //    || ((rows[0].stoppedSlot == rows[2].stoppedSlot)
        //    && (rows[0].stoppedSlot == "Diamond"))

        //    || ((rows[1].stoppedSlot == rows[2].stoppedSlot)
        //    && (rows[1].stoppedSlot == "7")))

        //    price += 200;

        //else if (((rows[0].stoppedSlot == rows[1].stoppedSlot)
        //    && (rows[0].stoppedSlot == "RDiamond"))

        //    || ((rows[0].stoppedSlot == rows[2].stoppedSlot)
        //    && (rows[0].stoppedSlot == "RDiamond"))

        //    || ((rows[1].stoppedSlot == rows[2].stoppedSlot)
        //    && (rows[1].stoppedSlot == "7")))

        //    price += 200;
        //else if (((rows[0].stoppedSlot == rows[1].stoppedSlot)
        //    && (rows[0].stoppedSlot == "7"))

        //    || ((rows[0].stoppedSlot == rows[2].stoppedSlot)
        //    && (rows[0].stoppedSlot == "Cherry"))

        //    || ((rows[1].stoppedSlot == rows[2].stoppedSlot)
        //    && (rows[1].stoppedSlot == "Cherry")))

        //    price += 200;
        //else if (((rows[0].stoppedSlot == rows[1].stoppedSlot)
        //    && (rows[0].stoppedSlot == "Cherry"))

        //    || ((rows[0].stoppedSlot == rows[2].stoppedSlot)
        //    && (rows[0].stoppedSlot == "7"))

        //    || ((rows[1].stoppedSlot == rows[2].stoppedSlot)
        //    && (rows[1].stoppedSlot == "Cherry")))

            resultChecked = true;
    }
    public void PriceUp()
    {
        price += 200f;
    }

    public void Sound()
    {
        Instantiate(sound, transform.position, Quaternion.identity);
    }
    public void Sound1()
    {
        Instantiate(sound1, transform.position, Quaternion.identity);
    }

    public void Sound2()
    {
        Instantiate(sound2, transform.position, Quaternion.identity);
    }
}
