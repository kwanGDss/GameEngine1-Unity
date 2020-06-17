using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoExhaustionItem : MonoBehaviour
{
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
            FirstPersonController.exhaustion = false;
            FirstPersonController.stemina = 100f;

            gameObject.GetComponent<BoxCollider>().enabled = false;
            StartCoroutine(NoExhaustion());
        }
    }

    IEnumerator NoExhaustion()
    {
        foreach (var renderer in renderers)
        {
            renderer.enabled = false;
        }

        for(int i=0; i<10; ++i)
        {
            FirstPersonController.stemina = 100f;
            yield return new WaitForSeconds(1f);
        }

        Destroy(gameObject);
    }
}
