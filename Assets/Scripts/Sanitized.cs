using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sanitized : MonoBehaviour
{
    Animator anim;
    int handsanitizer = 0;
    bool sanitized = false;
    float timeLeftSan = 8.0f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (handsanitizer != 0)
            {
                handsanitizer--;
                sanitized = true;
                anim.SetBool("sanitized", true);
                
            }
            
        }
        
        if (sanitized == true)
        {
            timeLeftSan -= Time.deltaTime;
            if (timeLeftSan == 0)
            {
                sanitized = false;
                anim.SetBool("sanitized", false);
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "handsan")
        {
            handsanitizer++;
         
        }
    }
}
