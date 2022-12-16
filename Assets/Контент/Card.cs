using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private Sprite[] allVar;
    [SerializeField] private Image spriteRenderer;

    [HideInInspector] public int cardNum;

    private void Start()
    {
        spriteRenderer.sprite = allVar[0];
    }

    public void Spawn()
    {
        cardNum = Random.Range(1, allVar.Length);
        spriteRenderer.sprite = allVar[cardNum];

        if (cardNum == 2)
        {
            SlotMachineScript.instance.PriceUp();
            SlotMachineScript.instance.Sound1();
            gameObject.SetActive(false);
        }

        else
        {
            SlotMachineScript.instance.Sound1();
            StartCoroutine(Restart());
        }
    }

    private IEnumerator Restart()
    {
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("Game1");
    }
}
