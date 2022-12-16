using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpawnerTwo : MonoBehaviour
{
    public static SpawnerTwo instance;

    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private GameObject sound, sound1, sound2, sound3;

    [SerializeField] private int xSize;
    [SerializeField] private int ySize;

    [SerializeField] private Vector2 tileSize;

    private Vector2 newTile;

    [SerializeField] private int score;
    [SerializeField] private TextMeshProUGUI scoreText;

    //[SerializeField] private GameObject clickSound;
    [SerializeField] private GameObject spin;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        newTile = transform.position;
    }

    public void CreateBoard()
    {
        Sound();
        StartCoroutine(Spin());
        AddCoin();
    }

    private void RandomObjectSpawn()
    {
        Instantiate(prefabs[Random.Range(0, prefabs.Length)], newTile, Quaternion.identity);
    }
    private IEnumerator Spin()
    {
        Sound3();
        float xPos = transform.position.x;
        float yPos = transform.position.y;

        for (int x = 0; x <= xSize; x++)
        {
            for (int y = 0; y <= ySize; y++)
            {
                newTile = new Vector3(xPos + ((tileSize.x + 0.25f) * x), yPos + ((tileSize.y + 0.25f) * y), 0);
                RandomObjectSpawn();
            }
        }

        yield return new WaitForSeconds(2.5f);


        spin.SetActive(true);
    }
    private void AddCoin()
    {
        score += Random.Range(100, 300);
        scoreText.text = score.ToString();
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

    public void Sound3()
    {
        Instantiate(sound3, transform.position, Quaternion.identity);
    }
}
