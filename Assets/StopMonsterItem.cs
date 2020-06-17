using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMonsterItem : MonoBehaviour
{
    private GameObject[] monsters;
    private Renderer[] renderers;

    // Start is called before the first frame update
    void Start()
    {
        renderers = gameObject.GetComponentsInChildren<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            monsters = GameObject.FindGameObjectsWithTag("Monster");

            foreach (var monster in monsters)
            {
                monster.GetComponent<Animator>().SetFloat("speed", 0f);
            }
            gameObject.GetComponent<BoxCollider>().enabled = false;
            StartCoroutine(StopMonster());
        }
    }

    IEnumerator StopMonster()
    {
        foreach(var renderer in renderers)
        {
            renderer.enabled = false;
        }

        yield return new WaitForSeconds(10f);

        foreach(var monster in monsters)
        {
            monster.GetComponent<Animator>().SetFloat("speed", 2f);
        }

        Destroy(gameObject);
    }
}
