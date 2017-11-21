using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public enum State{A,B,C,D }
    public State CurrentState = State.A;
    public float speed = 12f;
    public LayerMask layerMask;
    public UIManager uiManager;    
    public Transform[] teleportPosition;
    public Transform[] raycastPos;
    private Rigidbody2D rig;
    private string state="Right";
    [HideInInspector]
    public Animator ani;
    private float h;
    private float v;
    [HideInInspector]
    public bool gameStart = false;
    [HideInInspector]
    public bool finalLife;
    public GameObject boomPrefab;


    bool right = true;
    bool left = true;
    bool up = true;
    bool down = true;
    bool L = true;
    bool R = true;
    bool U = true;
    bool D = true;

    void Start()
    {
        CurrentState = State.B;
        rig = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (!gameStart)
            return;

        RayCasting();

        if (left || right)
        {
            if ((L || R))
            {
                h = Input.GetAxisRaw("Horizontal");
            }
        }

        if (up || down)
        {
            if ((U || D))
            {
                v = Input.GetAxisRaw("Vertical");
            }
        }

        walkDirection(state);
        NowState(h, v);        
    }

    void RayCasting()
    {
        Debug.DrawLine(transform.position, raycastPos[0].position, Color.red);
        Debug.DrawLine(transform.position, raycastPos[1].position, Color.red);
        Debug.DrawLine(transform.position, raycastPos[2].position, Color.red);
        Debug.DrawLine(transform.position, raycastPos[3].position, Color.red);

        Debug.DrawLine(raycastPos[4].position, raycastPos[5].position, Color.blue);
        Debug.DrawLine(raycastPos[6].position, raycastPos[7].position, Color.blue);
        Debug.DrawLine(raycastPos[8].position, raycastPos[10].position, Color.blue);
        Debug.DrawLine(raycastPos[9].position, raycastPos[11].position, Color.blue);

        left = !Physics2D.Linecast(transform.position, raycastPos[0].position, layerMask);
        right = !Physics2D.Linecast(transform.position, raycastPos[1].position, layerMask);
        up = !Physics2D.Linecast(transform.position, raycastPos[2].position, layerMask);
        down = !Physics2D.Linecast(transform.position, raycastPos[3].position, layerMask);

        L= !Physics2D.Linecast(raycastPos[4].position, raycastPos[5].position, layerMask);
        R= !Physics2D.Linecast(raycastPos[6].position, raycastPos[7].position, layerMask);
        U = !Physics2D.Linecast(raycastPos[8].position, raycastPos[10].position, layerMask);
        D = !Physics2D.Linecast(raycastPos[9].position, raycastPos[11].position, layerMask);
    }

    void NowState(float h,float v)
    {
        if (h > 0)
        {
            state = "Right";
            ani.SetBool("Move", true);
        }
        if (h < 0)
        {
            state = "Left";
            ani.SetBool("Move", true);
        }
        if (v > 0)
        {
            state = "Up";
            ani.SetBool("Move", true);
        }
        if (v < 0)
        {
            state = "Down";
            ani.SetBool("Move", true);
        }
    }

    void walkDirection(string nowState)
    {
        switch (nowState)
        {
            case ("Idle"):
                {
                    break;
                }
            case ("Right"):
                {
                    if (!right)
                    {
                        state = "Idle";
                        ani.SetBool("Move", false);                        
                    }
                    else
                    {
                        rig.velocity = new Vector2(speed, 0);
                    }
                    break;
                }
            case("Left"):
                {
                    if (!left)
                    {
                        state = "Idle";
                        ani.SetBool("Move", false);                        
                    }
                    else
                    {
                        rig.velocity = new Vector2(speed * -1, 0);
                    }
                    break;
                }
            case ("Up"):
                {
                    if (!up)
                    {
                        state = "Idle";
                        ani.SetBool("Move", false);                        
                    }
                    else
                    {
                        rig.velocity = new Vector2(0, speed);
                    }
                    break;
                }
            case ("Down"):
                {
                    if (!down)
                    {
                        state = "Idle";
                        ani.SetBool("Move", false);                        
                    }
                    else
                    {
                        rig.velocity = new Vector2(0, speed * -1);
                    }
                    break;
                }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.transform.name)
        {
            case ("TeleportUp"):
                {
                    transform.position = teleportPosition[1].position;
                    break;
                }
            case ("TeleportDown"):
                {
                    transform.position = teleportPosition[0].position;
                    break;
                }
            case ("TeleportLeft"):
                {
                    transform.position = teleportPosition[3].position;
                    break;
                }
            case ("TeleportRight"):
                {
                    transform.position = teleportPosition[2].position;
                    break;
                }
            case ("feed"):
                {
                    Destroy(collision.gameObject);
                    uiManager.scoreAmount += 10;
                    uiManager.feed--;
                    break;
                }
            case ("Enemy"):
                {
                    GameObject boom = Instantiate(boomPrefab, transform.position, Quaternion.identity);
                    Destroy(boom, .5f);
                    uiManager.lifeReduce();

                    if (finalLife)
                    {
                        return;
                    }

                    Death();                    
                    break;
                }
        }
    }

   void Death()
    {
        state = "Right";
        this.gameObject.SetActive(false);
        transform.position = new Vector2(-5.5f, -11f);
        Invoke("reStart", 1f);      
    }

    void reStart()
    {
        this.gameObject.SetActive(true);
        
        uiManager.respawnGame();
        state = "Right";
    }
}
