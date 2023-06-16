using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour{

    //
    public float flap = 10f;
    public float Hflap = 15f;
    public float Wflap = 15f;
    public float scroll = 5f;
    public float dia = 3f;
    public float Wdia = 3f;
    public float stop = 50f;
    public float movelimit = 250f;
    public float moveForceMultiplier;
    public float vlimiter = 0.1f;
    float direction = 0;
    bool jump = false;
    string status = "NoReady";
    Rigidbody2D rigid;
    Vector2 moveVector = Vector2.zero;

    // Start is called before the first frame update
    void Start(){
        rigid = GetComponent<Rigidbody2D>();
        GameObject player = GameObject.Find("Player");
        Debug.Log("Game Start");
    }

    // Update is called once per frame
    void Update(){
        
    }

    private void FixedUpdate(){
        //if (Input.GetKey(KeyCode.RightArrow)){
        //    direction = 1f;
        //}else if (Input.GetKey(KeyCode.LeftArrow)){
        //    direction = -1f;
        //}else{
        //    direction = 0f;
        //}
        
        float x = Input.GetAxis("Horizontal");
        Vector2 force = new Vector2(x * scroll, 0);
        rigid.AddForce(force);
        //

        rigid.AddForce(moveForceMultiplier * (moveVector - rigid.velocity));
        //
        //if (Input.GetKey(KeyCode.RightArrow)){
        //    rigid.Addforce2D(Vecter2.right * scroll);
        //}else if (Input.GetKey(KeyCode.LeftArrow)){
        //    rigid.Addforce2D(Vecter2.left * scroll);
        //}

        //rigid.velocity = new Vector2(scroll * direction, rigid.velocity.y);

        
        if (Mathf.Abs(rigid.velocity.x) >= movelimit){
            
            rigid.AddForce(moveForceMultiplier * (moveVector - rigid.velocity) * vlimiter);
        }

        
        if (Input.GetKeyDown("z")){
            Rdirection = new Vector2(-3, -7);
            play = GameObject.Find("Player").transform.position;
            Ray2D R_ray = new Ray2D(play, Rdirection);
            RaycastHit2D Rhit = Physics2D.Raycast(play, Rdirection, RmaxDistance, layerMask);
            if (Rhit.collider == null){
            }else {
                if(Rhit.collider.tag == "Ground"){
                    if(rigid.velocity.y < 0){
                        status = "RJumpReady";
                        fallSpeed = Mathf.Abs(rigid.velocity.y) / 4;
                        Debug.Log("RJumpReady");
                    }
                }else if(Rhit.collider.tag == "Wall"){
                    rigid.AddForce(Vector2.up * Wflap);
                    rigid.AddForce(Vector2.right * Wdia);
                    status = "NoReady";
                    Debug.Log("RWJ");
                }
            }
        Debug.DrawRay(R_ray.origin, R_ray.direction * RmaxDistance, Color.red, 5);
        }

        if (Input.GetKeyDown("x")){
            Rdirection = new Vector2(0, -2);
            play = GameObject.Find("Player").transform.position;
            Ray2D D_ray = new Ray2D(play, Rdirection);
            RaycastHit2D Dhit = Physics2D.Raycast(play, Rdirection, DmaxDistance, layerMask);
            if (Dhit.collider == null){
            }else {
                if(rigid.velocity.y < 0){
                    status = "HJumpReady";
                    fallSpeed = Mathf.Abs(rigid.velocity.y) * 6;
                        Debug.Log(Mathf.Abs(rigid.velocity.y));
                    Debug.Log("HJumpReady");
                }
            }
            Debug.DrawRay(D_ray.origin, D_ray.direction * DmaxDistance, Color.red, 5);
        }
        if (Input.GetKeyDown("c")){
            Rdirection = new Vector2(3, -7);
            play = GameObject.Find("Player").transform.position;
            Ray2D L_ray = new Ray2D(play, Rdirection);
            RaycastHit2D Lhit = Physics2D.Raycast(play, Rdirection, LmaxDistance, layerMask);
            if (Lhit.collider == null){
            }else {
                if(Lhit.collider.tag == "Ground"){
                    if(rigid.velocity.y < 0){
                        status = "LJumpReady";
                        Debug.Log("LJumpReady");
                    }
                }else if(Lhit.collider.tag == "Wall"){
                    rigid.AddForce(Vector2.up * Wflap);
                    rigid.AddForce(Vector2.left * Wdia);
                    status = "NoReady";
                    Debug.Log("LWJ");
                }
            }
            Debug.DrawRay(L_ray.origin, L_ray.direction * LmaxDistance, Color.red, 5);
        }

        if (Input.GetKey(KeyCode.DownArrow)){
            rigid.AddForce(Vector2.down * stop);
        }
    }


    Vector2 play;
    //Vector3 origin;
    Vector2 Rdirection = Vector2.zero;
    public int RmaxDistance = 3;
    public int DmaxDistance = 3;
    public int LmaxDistance = 3;
    float fallSpeed;
    int layerMask = 1 << 9 | 1 << 10;
    RaycastHit2D Rhit, Dhit, Lhit;

    

    

    void OnCollisionEnter2D(Collision2D other){
        if (other.gameObject.CompareTag("Ground")){
            jump = false;
            if (status == "RJumpReady" && !jump){
                rigid.AddForce(Vector2.up * flap);
                rigid.AddForce(Vector2.right * dia);
                jump = true;
                status = "NoReady";
            }else if (status == "HJumpReady" && !jump){
                rigid.AddForce(Vector2.up * (Hflap + fallSpeed));
                jump = true;
                fallSpeed = 0;
                status = "NoReady";
            }else if (status == "LJumpReady" && !jump){
                rigid.AddForce(Vector2.up * flap);
                rigid.AddForce(Vector2.left * dia);
                jump = true;
                status = "NoReady";
            }else {
                rigid.AddForce(Vector2.up * flap);
            }
        }else if (other.gameObject.CompareTag("Damage")){
            Debug.Log("Damage");
            SceneManager.LoadScene("GameOver");
        }else if (other.gameObject.CompareTag("Goal")){
            Debug.Log("Clear");
            SceneManager.LoadScene("ClearScene");
        }
    //void OnCollisionStay2D(Collision2D collision){
    //    if (collision.gameObject.CompareTag("Wall")){
    //        Debug.Log("Collide wall");
    //        if (status == "RWJumpReady"){
    //            rigid.AddForce(Vector2.up * Wflap);
     //           rigid.AddForce(Vector2.right * Wdia);
    //            status = "NoReady";
    //        }else if (status == "LWJumpReady"){
    //            rigid.AddForce(Vector2.up * Wflap);
    //            rigid.AddForce(Vector2.left * Wdia);
    //            status = "NoReady";
     //       }
    //    }
    //}


        //if (Input.GetKeyDown("x") && !jump){
        //    rigid.AddForce(Vector2.up * flap);
        //    jump = true;
        //}else if (Input.GetKeyDown("z") && !jump){
        //    rigid.AddForce(Vector2.up * flap);
        //  rigid.AddForce(Vector2.right * dia);
        //   jump = true;
        //}else if (Input.GetKeyDown("c") && !jump){
        //    rigid.AddForce(Vector2.up * flap);
        //    rigid.AddForce(Vector2.left * dia);
        //    jump = true;
        
        

    }

}
