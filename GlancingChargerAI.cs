using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlancingChargerAI : MonoBehaviour
{
    public Rigidbody2D body;
    float horizontal;
    public float strollSpeed = 3;
    public bool hurt;
    public LayerMask ThisEnemies;
    public bool playerInSight;
    public float lookDirect = 1;
    public float gazeDirect;
    public float RNGController;
    public bool isInAction;

    // Start is called before the first frame update
    void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
        InvokeRepeating("ControlWhatDo", 0, Random.Range(1,2.5f));
    }
   
    // Update is called once per frame
    void Update()
    {
        
        playerInSight = Physics2D.OverlapBox(gameObject.transform.position, new Vector2(gazeDirect * 15, 2), 0, ThisEnemies);
       if(lookDirect != 0)
        {
            gazeDirect = lookDirect;
        }


        if (playerInSight)
        {
            strollSpeed = 6;
        }
        else
        {
            strollSpeed = 3;
        }
        if (playerInSight)
        {
            print("Spotted!");
        }
        else
        {
            print("Hidden!");
        }
        if (hurt == false)
        {
            body.velocity = new Vector3(lookDirect * strollSpeed, body.velocity.y);
            if (RNGController == 1)
            {
                lookDirect = 1;
            }
            if (RNGController == 2)
            {
                lookDirect = -1;
            }
            if (RNGController == 3)
            {
                lookDirect = 0;
            }
        }
        
    }
    private void FixedUpdate()
    {


       

    }
    public void ControlWhatDo()
    {
        RNGController = Random.Range(1, 4);
    }
    public void charge()
    {
        body.velocity = new Vector3(gazeDirect * strollSpeed * 2, body.velocity.y);
    }
}
