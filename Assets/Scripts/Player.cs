using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour
{

    public AudioSource audioData;
    //movement
    public float speed;
    private Rigidbody2D rb2d;
    //--------------------------------------------------------------------
    //infection
    float timeLeft = 5.0f;
    bool infecting1 = false;
    bool infecting2 = false;
    bool infecting3 = false;
    //--------------------------------------------------------------------
    //inventory
    int toiletpaper = 0;
    public GameObject TP;
    int vaccine = 0;
    public GameObject Vaccine;
    int handsanitizer = 0;
    bool sanitized = false;
    float timeLeftSan = 8.0f;
    bool mask = false;
    float timeLeftMask = 3.0f;
    //--------------------------------------------------------------------
    //List

    bool bananaGet = false;
    bool BeanzGet = false;
    bool PizzaGet = false;

    //--------------------------------------------------------------------
    //UI
    public Text TpText;
    public Text VaccineText;
    public Text HandSanText;
    public Text TimeLeftText;
    public Text bananaGot;
    public Text pizzaGot;
    public Text beanzGot;
    //--------------------------------------------------------------------
    //Animation
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioData = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        //movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        UnityEngine.Vector2 movement = new UnityEngine.Vector2(horizontal, vertical);
        rb2d.velocity = movement * speed;
        //--------------------------------------------------------------------------------------------------
        //Animation
        if (horizontal > 0)
        {
            anim.SetBool("moving right", true);
        }
        else {
            anim.SetBool("moving right", false);
        }

        if (horizontal < 0)
        {
            anim.SetBool("moving left", true);
        }
        else
        {
            anim.SetBool("moving left", false);
        }

        if (vertical > 0)
        {
            anim.SetBool("moving up", true);
        }
        else
        {
            anim.SetBool("moving up", false);
        }

        if (vertical < 0)
        {
            anim.SetBool("moving down", true);
        }
        else
        {
            anim.SetBool("moving down", false);
        }

        if (horizontal == 0 && vertical == 0)
        {

            anim.SetBool("Idle", true);
        }
        else {
            anim.SetBool("Idle", false);
        
        }

        //--------------------------------------------------------------------------------------------------
        //infection
        if (infecting1 == true)
        {
            timeLeft -= Time.deltaTime;
            
        }else if (infecting2 == true)
        {
            timeLeft -= Time.deltaTime + 0.003f;
            
        }else if (infecting3 == true)
        {
            timeLeft -= Time.deltaTime + 0.006f;
            
        }
        else
        {
            timeLeft = 5.0f;
        }

        if (timeLeft < 0)
        {
            Death();
        }

        //--------------------------------------------------------------------------------------------------
        //item use

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (toiletpaper != 0) {
                toiletpaper--;
                Instantiate(TP, transform.position, transform.rotation);
            }
            
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (vaccine > 0)
            {
                vaccine--;
                Instantiate(Vaccine, transform.position, transform.rotation);
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (handsanitizer > 0)
            {
                handsanitizer--;
                sanitized = true;
                
            }
        }

        if (mask == true)
        {
            timeLeftMask -= Time.deltaTime;
            if (timeLeftMask < 0)
            {
                mask = false;
                anim.SetBool("mask on", false);
            }
        }

        if (sanitized == true)
        {
            timeLeftSan -= Time.deltaTime;
            if (timeLeftSan < 0)
            {
                sanitized = false;
                
            }
        }
        //----------------------------------------------------------------------------------
        //UI

        TpText.text = toiletpaper.ToString();
        VaccineText.text = vaccine.ToString();
        HandSanText.text = handsanitizer.ToString();
        TimeLeftText.text = timeLeft.ToString("F2");
        if (bananaGet == true) {

            bananaGot.text = "Got";
        
        }
        if (PizzaGet == true)
        {

            pizzaGot.text = "Got";

        }
        if (BeanzGet == true)
        {

            beanzGot.text = "Got";

        }

    }
        

    private void OnCollisionEnter2D(Collision2D col)
    {
        //audioData.Play();

        if (col.gameObject.tag == "toiletpap")
        {
            toiletpaper++;
            
            Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "mask")
        {
            Destroy(col.gameObject);
            mask = true;
            anim.SetBool("mask on", true);

        }
        if (col.gameObject.tag == "vaccine")
        {
            vaccine++;
            Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "handsan")
        {
            handsanitizer++;
            Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "Banana") {
            bananaGet = true;
            Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "Pizza") {
            PizzaGet = true;
            Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "Beanz") {

            BeanzGet = true;
            Destroy(col.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "win")
        {
            if (bananaGet == true && BeanzGet == true && PizzaGet == true)
            {

                SceneManager.LoadScene(5);
            }
            else {
                Debug.Log("nah");
            
            }
            
        
        
        }

            if (col.gameObject.tag == "Covid1") {

            if (sanitized == false && mask == false)
            {
                infecting1 = true;
            }
        }
        if (col.gameObject.tag == "Covid2")
        {

            if (sanitized == false && mask == false)
            {
                infecting2 = true;
            }
        }
        if (col.gameObject.tag == "Covid3")
        {

            if (sanitized == false && mask == false)
            {
                infecting3 = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        infecting1 = false;
        infecting2 = false;
        infecting3 = false;
    }

    void Death() {

        Debug.Log("You died");
        timeLeft = 0;
        SceneManager.LoadScene(4);
    
    }
}
