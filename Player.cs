using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D body;
    float vertical;
    float horizontal;
    public float runSpeed = 5;
    public float jumpPower = 20;
    public bool isOnGround;
    public LayerMask ground;
    public GameObject claw;
    public Vector3 hazardSavePos;
    public GameObject feet;
    public bool hurt;



    // Start is called before the first frame update
   

    void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        isOnGround = Physics2D.OverlapBox(feet.transform.position, new Vector2(1f, 0.1f), 0, ground);
        if (Input.GetKeyDown(KeyCode.UpArrow) && isOnGround == true && hurt == false)
        {
            body.velocity = new Vector2(body.velocity.x, 1 * jumpPower);

        }




    }
    private void FixedUpdate()
    {


        if(hurt == false)
        {
            body.velocity = new Vector3(horizontal * runSpeed, body.velocity.y);
        }
       

        if (isOnGround)
        {
            hazardSavePos = feet.transform.position;
        }


    }
    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == "Spikes")
        {
            gameObject.transform.position = new Vector3(hazardSavePos.x, hazardSavePos.y + 1);

        }
        if (col.collider.tag == "Enemy")
        {
            StartCoroutine(SafeFrames());
            IEnumerator SafeFrames()
            {
                if(horizontal != 0)
                {
                    body.velocity = new Vector3(-horizontal * 7.5f, 0);
                }
                else if(col.collider.transform.position.x < gameObject.transform.position.x)
                {
                    body.velocity = new Vector3(7.5f, 0);
                }
                else if(col.collider.transform.position.x > gameObject.transform.position.x)
                {
                    body.velocity = new Vector3(-7.5f, 0);
                }
               
                
                hurt = true;
               
                yield return new WaitForSeconds(2);
                hurt = false;
                Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), col.collider, true);

                yield return new WaitForSeconds(0.5f);

                Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), col.collider, false);
               


                StopCoroutine(SafeFrames());

            }
        }



    }

}
