using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy;
    private Transform enemyTransform;

    // Start is called before the first frame update
    void Start()
    {
        CreateEnermy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateEnermy()
    {
        enemy = Instantiate(enemy, new Vector3(-135f, -0.085f, -244f), Quaternion.identity);
        enemy.name = "ss";
    }
}
