using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SteminaBar : MonoBehaviour
{
    private Slider steminaBar;
    private Image steminaBarFill;

    // Start is called before the first frame update
    void Start()
    {
        steminaBar = gameObject.GetComponent<Slider>();

        steminaBarFill = GameObject.Find("Fill").gameObject.GetComponent<Image>();
        steminaBarFill.color = Color.cyan;
    }

    // Update is called once per frame
    void Update()
    {
        steminaBar.value = FirstPersonController.stemina;

        if (FirstPersonController.exhaustion)
            steminaBarFill.color = Color.red;
        else
            steminaBarFill.color = Color.cyan;
    }
}
