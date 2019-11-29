using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour {

    public float width = 5; //Width of the block hitbox
    public float blockSpeed = 5;
    public float blockTime = 1;

    private GameObject sword; //Parent sword
    private GameObject Owner; //Owner slime
    
    private SwordScript swordScript; //Parent's sword script
    private PlayerMovement playerMovement;

	void Start ()
    {
        transform.localScale = new Vector3(width, 1.15f, 0.43f); //Sets the scale of the block hitbox
        sword = transform.parent.gameObject;
        Debug.Log(sword);
        swordScript = sword.GetComponent<SwordScript>();
        Owner = swordScript.Owner;
        playerMovement = Owner.GetComponent<PlayerMovement>();
	}

    public void blockSword(GameObject otherSword)
    {
        sword = transform.parent.gameObject;
        SwordScript swordScript = sword.GetComponent<SwordScript>();
        Rigidbody swordRB = sword.gameObject.GetComponent<Rigidbody>();
        //Vector3 relativePoint = sword.transform.InverseTransformPoint(otherSword.transform.position);
        if(!swordScript.swordDone)
        {
            swordScript.stopCoroutines();
            playerMovement.stopCoroutines();
            playerMovement.currentSpeed = playerMovement.speed;
            playerMovement.thrustDebuff = false;
            swordScript.swordDone = true;
            swordRB.velocity = Vector3.zero;
            //Can move sword again set to position incase mouse position was changed during thrust
            swordScript.MoveSword();
            swordScript.isBlocked = false;
            swordScript.thrustHit = true;
        }
        else
        {
            swordScript.blockedOffset = sword.transform.position - swordScript.Owner.transform.position;
            StartCoroutine(Countdown(blockTime));
        }
    }

    private IEnumerator Countdown(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        sword.GetComponent<SwordScript>().isBlocked = false;
    }

}
