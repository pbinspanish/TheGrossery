using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySuspicionBar : MonoBehaviour
{
    public EnemyController parent;
    public Slider slider;
    public Image fill;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 parentPosOnScreen = Camera.main.WorldToScreenPoint(parent.transform.position);
        parentPosOnScreen.y += 40;

        slider.transform.position = parentPosOnScreen;

        slider.value = parent.suspicion;

        if (slider.value >= 50) {
            fill.color = Color.red;
        }
        else {
            fill.color = new Color(255, 200, 0, 255);
        }
    }
}
