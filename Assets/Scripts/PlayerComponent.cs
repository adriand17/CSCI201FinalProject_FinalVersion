using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerComponent : MonoBehaviour
{
    //member variables
    bool dead;
    List<PlayerComponent> killableTargets;

    //public member variables
    public GameObject body;
    public bool impostor;
    public InputAction kill_;
    public Collider2D collider_;
    public SpriteRenderer thisSprite;




    //initialization functions
    private void Awake()
    {
        kill_.performed += killTarget;
    }

    private void OnEnable()
    {
        kill_.Enable();
    }
    // Start is called before the first frame update
    void Start()
    {
        killableTargets = new List<PlayerComponent>();
    }
    //set role of player
    public void SetRole(bool impos)
    {
        impostor = impos;
    }




    //disable functions
    private void OnDisable()
    {
        kill_.Disable();
    }


    //Update functions

    //Seeing if any players are in our range (UPON ENTERING THE TRIGGER CIRCLE COLLIDER 2D)
    //add to list of possible targets for impostor to kill.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerComponent targ = other.GetComponent<PlayerComponent>();
            //check if we are impostor
            if (impostor)
            {
                if (!targ.impostor)
                {
                    killableTargets.Add(targ);
                }
            }
        }
    }

    //Remove players that leave our trigger

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerComponent targ = other.GetComponent<PlayerComponent>();
            //check if we are impostor
            if (impostor)
            {
                if (!targ.impostor)
                {
                    killableTargets.Remove(targ);
                }
            }
        }
    }


    //Misc. Functions
    //kill the selected target if there is one
    void killTarget(InputAction.CallbackContext con)
    {
        if(con.phase == InputActionPhase.Performed)
        {
            // check if there is a target
            if(killableTargets.Count >= 0)
            {
                int target = killableTargets.Count -1;
                if(!killableTargets[target].dead)
                {
                    transform.position = killableTargets[target].transform.position;
                    killableTargets[target].kill();
                    killableTargets.RemoveAt(target);
                }
            }
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
    }

}
