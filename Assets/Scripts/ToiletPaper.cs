using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletPaper : MonoBehaviour
{

    float throwTime = 0.8f;
    float waitTime = 2.0f;
    float speed = 0.03f;

    // Start is called before the first frame update
    void Start()
    {
        faceMouse();
    }

    // Update is called once per frame
    void Update()
    {
        
        throwTime -= Time.deltaTime;

        transform.Translate(0, speed, 0);

        if (throwTime < 0) {
            speed = 0;
            while (waitTime > 0) {

                waitTime -= Time.deltaTime;
                
            }
            if (waitTime == 0) {

                Destroy(gameObject);
            }
        }
    }

    void faceMouse() {

        Vector2 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

        transform.up = direction;
    
    }
}
