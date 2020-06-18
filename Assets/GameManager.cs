using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy;
    private GameObject[] randomTransform;

    [SerializeField]
    private GameObject scoreUpCrystal;
    [SerializeField]
    private GameObject stopMonsterCrystal;
    [SerializeField]
    private GameObject noExhaustionCrystal;

    private int random;

    private int enemyCount = 0;
    public static int Score { get; set; } = 0;

    public static bool winner = false;
    public static bool loser = false;

    // Start is called before the first frame update
    void Start()
    {
        randomTransform = GameObject.FindGameObjectsWithTag("Dust");
        random = Random.Range(0, randomTransform.Length);

        CreateItem();
        StartCoroutine(CreateEnemyCoroutine());
        StartCoroutine(WinGame());
        StartCoroutine(LoseGame());
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindGameObjectWithTag("Crystal") == null)
            winner = true;
    }

    private void CreateEnemy()
    {
        enemy = Instantiate(enemy, new Vector3(randomTransform[random].transform.position.x, -0.085f, randomTransform[random].transform.position.z), Quaternion.identity);
        enemy.name = "Monster";

        random = Random.Range(0, randomTransform.Length);
    }

    IEnumerator CreateEnemyCoroutine()
    {
        while (true)
        {
            if (Score / 3 >= enemyCount)
            {
                CreateEnemy();
                enemyCount += 1;
            }
            yield return new WaitForSeconds(1f);
        }
    }

    private void CreateItem()
    {
        Instantiate(scoreUpCrystal, new Vector3(randomTransform[random].transform.position.x, -0.085f, randomTransform[random].transform.position.z), Quaternion.identity);
        random = Random.Range(0, randomTransform.Length);

        Instantiate(stopMonsterCrystal, new Vector3(randomTransform[random].transform.position.x, -0.085f, randomTransform[random].transform.position.z), Quaternion.identity);
        random = Random.Range(0, randomTransform.Length);

        Instantiate(noExhaustionCrystal, new Vector3(randomTransform[random].transform.position.x, -0.085f, randomTransform[random].transform.position.z), Quaternion.identity);
        random = Random.Range(0, randomTransform.Length);
    }

    IEnumerator WinGame()
    {
        while (true)
        {
            if (winner == true)
            {
                yield return new WaitForSeconds(3f);
                GameInitialize();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                SceneManager.LoadScene("StartScene");
            }
            else
            {
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    IEnumerator LoseGame()
    {
        while (true)
        {
            if (loser == true)
            {
                yield return new WaitForSeconds(3f);
                GameInitialize();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                SceneManager.LoadScene("StartScene");
            }
            else
            {
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    private void GameInitialize()
    {
        FirstPersonController.stemina = 100;
        FirstPersonController.exhaustion = false;
        PauseMenu.paused = false;
        winner = false;
        loser = false;
        Score = 0;
    }
}

