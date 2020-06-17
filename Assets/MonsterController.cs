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

        InvokeRepeating("CheckArriveDust", 3f, 10f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            navMesh.SetDestination(other.gameObject.transform.position);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            navMesh.SetDestination(randomTransform[random].transform.position);
    }

    private void CheckArriveDust()
    {
        if (Vector3.Distance(transform.position, randomTransform[random].transform.position) < 3.0f)
        {
            random = Random.Range(0, randomTransform.Length);

            navMesh.SetDestination(randomTransform[random].transform.position);
        }
    }
}
