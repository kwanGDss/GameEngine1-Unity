using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    private NavMeshAgent navMesh;
    private GameObject[] randomTransform;   //Dust(s)

    private int random;

    // Start is called before the first frame update
    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
        randomTransform = GameObject.FindGameObjectsWithTag("Dust");

        random = Random.Range(0, randomTransform.Length);

        navMesh.SetDestination(randomTransform[random].transform.position);
        //navMesh.SetDestination(playerTransform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, randomTransform[random].transform.position) < 3.0f)
        {
            print("Deis");

            random = Random.Range(0, randomTransform.Length);

            navMesh.SetDestination(randomTransform[random].transform.position);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            navMesh.SetDestination(other.gameObject.transform.position);

        print("Collider");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            navMesh.SetDestination(randomTransform[random].transform.position);
    }
}
