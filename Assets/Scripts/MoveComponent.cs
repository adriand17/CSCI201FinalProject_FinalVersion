using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Text;

public class MoveComponent : MonoBehaviour
{
    //member variables
    Animator animation;
    Rigidbody2D rB;
    Transform sprite;
    Vector2 moveVector;
    public InputAction mouse;
    Vector2 mousePositionInput;
    Camera cam;
    bool dead;
    List<MoveComponent> killableTargets;
    public bool guest = false;

    //public member variables
    public bool myPlayer;
    public static MoveComponent thisPlayer;
    public InputAction keys;
    public float mSpeed;
    public InputAction interaction;
    public LayerMask layer;
    public GameObject body;
    public bool impostor;
    public InputAction kill_;
    public Collider2D collider_;
    public SpriteRenderer thisSprite;
    public SpriteRenderer colorSprite;
    static Color thisColor;

    public String ID;
    //List of things for networking
    public static TcpClient client = null;
    NetworkStream stream = null;
    public PlayerInfo thisInfo;

    //set role of player
    public void SetRole(bool impos)
    {
        impostor = impos;
    }

    //Update functions

    //Seeing if any players are in our range (UPON ENTERING THE TRIGGER CIRCLE COLLIDER 2D)
    //add to list of possible targets for impostor to kill.
    private void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.tag == "Player")
       //{
         //   MoveComponent targ = other.GetComponent<MoveComponent>();
         //   //check if we are impostor
         //   if (impostor)
         //   {
         //       if (!targ.impostor)
         //       {
          //         killableTargets.Add(targ);
          //      }
          //  }
       // }
    }

    //Remove players that leave our trigger

    private void OnTriggerExit2D(Collider2D other)
    {
        //if (other.tag == "Player")
        //{
         //   MoveComponent targ = other.GetComponent<MoveComponent>();
        //    //check if we are impostor
         //   if (impostor)
           /// {
           //     if (!targ.impostor)
           ///     {
            //        killableTargets.Remove(targ);
            //    }
         //   }
      //  }
    }


    //Misc. Functions
    //kill the selected target if there is one
    void killTarget(InputAction.CallbackContext con)
    {
        if (con.phase == InputActionPhase.Performed)
        {
            if (killableTargets.Count > 0)
            {
                int target = killableTargets.Count - 1;
                if (!killableTargets[target].dead)
                {
                    transform.position = killableTargets[target].transform.position;
                    killableTargets[target].kill();
                    Debug.Log("size");
                    Debug.Log(killableTargets.Count); Debug.Log(target);
                    killableTargets.RemoveAt(target - 1);
                }
            }
            else
            {
                return;
            }
        }
        else
        {
            return;
        }
    }

    //perform kill
    public void kill()
    {
        dead = true;
        collider_.enabled = false;


        //spawn body
        GameObject deadBody = Instantiate(body, transform.position, transform.rotation);
        deadBody.transform.localScale = new Vector3(3, 3, 3);
        deadBody.GetComponent<DeadBodyComponent>().fixColor(thisSprite.color);
        deadBody.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 0;
        //sprite.gameObject.SetActive(false);
        animation.SetBool("isDead", true);
        sprite.gameObject.SetActive(false);
    }

    private void Awake()
    {
        interaction.performed += Interact;
        kill_.performed += killTarget;
    }


    private void OnEnable()
    {
        keys.Enable();
        mouse.Enable();
        interaction.Enable();
        kill_.Enable();
    }

    private void OnDisable()
    {
        keys.Disable();
        mouse.Disable();
        interaction.Disable();
        kill_.Disable();
    }

    void Start()
    {
        if (myPlayer)
        { 
            thisPlayer = this;
        }
        rB = GetComponent<Rigidbody2D>();
        sprite = transform.GetChild(0);
        animation = GetComponent<Animator>();
        cam = transform.GetChild(1).GetComponent<Camera>();
        killableTargets = new List<MoveComponent>();


        colorSprite = this.GetComponent<SpriteRenderer>();
        if (thisColor == Color.clear)
        {
            thisColor = Color.white;
        }
        colorSprite.color = thisColor;




        //Networking

        if (!guest)
        {
            client = new TcpClient("localhost", 3456);
            stream = client.GetStream();
            client.NoDelay = true;
            thisInfo = new PlayerInfo(thisPlayer.transform.position, ID, 1.0);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!myPlayer)
        {
            return;
        }
        moveVector = keys.ReadValue<Vector2>();
        if (moveVector.x != 0)
        {
            sprite.localScale = new Vector2(Mathf.Sign(-moveVector.x), 1);
        }
        animation.SetFloat("animationSpeed", moveVector.magnitude);





        //Networking
        if (myPlayer && !guest)
        {
            thisInfo.Position[0] = thisPlayer.transform.position.x;
            thisInfo.Position[1] = thisPlayer.transform.position.y;
            thisInfo.Position[2] = thisPlayer.transform.position.z;
            //thisInfo.PlayerState = Animator.GetFloat("animation");
            stream = client.GetStream();

            string json = JsonUtility.ToJson(thisInfo);
            Byte[] data = new Byte[256];
            //print(data.Length);
            Byte[] tempdata = System.Text.Encoding.ASCII.GetBytes(json);
            for (int i = 0; i < tempdata.Length; i++)
            {
                data[i] = tempdata[i];
            }
            //print(data.Length);

            stream.Write(data, 0, data.Length);

            if (stream.DataAvailable)
            {
                data = new Byte[256];
                String response = "";
                Int32 bytes = stream.Read(data, 0, data.Length);
                response = System.Text.Encoding.ASCII.GetString(data, 0, bytes).TrimEnd('\0');
                //print(response);
                PlayerInfo newInfo = JsonUtility.FromJson<PlayerInfo>(response);
                //print(newInfo.PlayerId);
                if (newInfo.PlayerId != thisInfo.PlayerId)
                {
                    String name = ("Astronaut" + newInfo.PlayerId);
                    //print(name);
                    GameObject updatepl = GameObject.Find(name);
                    Vector3 updatepos;
                    updatepos.x = (float)newInfo.Position[0];
                    updatepos.y = (float)newInfo.Position[1];
                    updatepos.z = (float)newInfo.Position[2];
                    updatepl.transform.position = updatepos;
                }
                //Console.WriteLine("Received: {0}", response);
            }
            else
            {
                print("Nothing");
            }
        }

    }

    private void FixedUpdate()
    {
        rB.velocity = moveVector * mSpeed;
    }


    void Interact(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            //Debug.Log("cam");
            //Debug.Log(cam.transform.position.x);
            var mousePOS = Input.mousePosition;
            mousePOS.z = 10;
            Vector3 worldPoint = cam.ScreenToWorldPoint(mousePOS);
            Vector2 wp2d = new Vector2(worldPoint.x, worldPoint.y);
            Debug.Log("WP");
            Debug.Log(wp2d.x);
            Debug.Log(wp2d.y);
            RaycastHit2D hit = Physics2D.Raycast(wp2d, Vector2.zero, 1.0f, layer);
            if (hit)
            {
                Debug.Log(hit.transform.gameObject);
                if (hit.transform.tag == "Interactible")
                {
                    if (!hit.transform.GetChild(0).gameObject.activeInHierarchy)
                    {
                        return;
                    }
                    Interactible inter = hit.transform.GetComponent<Interactible>();
                    inter.Play(thisPlayer);
                    myPlayer = false;
                    Debug.Log("Played!");
                    
                }
            }
        }
    }


    public void SetColor(Color color)
    {
        thisColor = color;
        if(colorSprite != null) 
        {
            colorSprite.color = color;
        }
    }
}