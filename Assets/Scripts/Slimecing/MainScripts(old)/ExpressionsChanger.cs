using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpressionsChanger : MonoBehaviour
{
    public Material m_Norm; //Normal expression for this material
    public Material m_Squint; //Squint expression for this material
    public Material m_Happy; //Happy expression for this material

    private Animator ExprAnim;

    private GameObject slime;
    private PlayerMovement playerMovement;


    void Start()
    {
        ExprAnim = GetComponentInParent<Animator>();

        slime = transform.root.gameObject;
        playerMovement = slime.GetComponent<PlayerMovement>();
    }


    void FixedUpdate()
    {

        /*if (Input.GetKey("r")) //if r is pressed, run code
        {
            ExprAnim.Play("DEATH2"); //play DEATH2 animation

            GetComponent<Renderer>().material = m_Squint; //change material to Squint
        }
        if (Input.GetKey("e")) //if e is pressed, run code
        {
            GetComponent<Renderer>().material = m_Norm; //change material to Norm(al)
        }
        if (Input.GetKey("q")) //if w is pressed, run code
        {
            ExprAnim.Play("WIN2"); //play WIN2 animation

            GetComponent<Renderer>().material = m_Happy; //change material to Happy
        }*/

        

        if (Input.GetButtonDown(playerMovement.emoteOne))
        {
            ExprAnim.Play("WIN2"); //play WIN2 animation

            if (AudioManager.getInstance() != null && !AudioManager.getInstance().Find("baldwin").source.mute)
            {
                AudioManager.getInstance().Find("baldwin").source.Stop();
            }

            if (playerMovement.Hat != null) {
                Invoke("AddHatForce", 0.8f);
            }
        }

        if (this.ExprAnim.GetCurrentAnimatorStateInfo(0).IsName("WIN2")) //When WIN2 taunt is playing the material is happy
        {
            GetComponent<Renderer>().material = m_Happy; //change material to Happy
        }

        if (this.ExprAnim.GetCurrentAnimatorStateInfo(0).IsName("TAKEDAMAGE")) //When WIN2 taunt is playing the material is happy
        {
            GetComponent<Renderer>().material = m_Squint; //change material to Happy
        }

        if (ExprAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1) //if statement where if there is no animation playing it runs
        {
            GetComponent<Renderer>().material = m_Norm; //changes the material to normal
        }
    }

    private void AddHatForce()
    {
        playerMovement.Hat.GetComponent<Rigidbody>().AddTorque(-playerMovement.Hat.transform.up * 100);
    }

    
}