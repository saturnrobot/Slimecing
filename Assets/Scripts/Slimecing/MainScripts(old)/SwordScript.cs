using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour {

    private bool MouseInput; //Are you doing mouse movement or key input for keyboard
    public bool swordDone = true; //Sword finished thrust
    public bool isBlocked = false; //Was blocked
    public bool isFalling;

    public float swordSpeed; //How fast the sword moves under keyboard input
    public float swordMouseSpeed; //How fast the sword moves under mouse input

    public float thrustSpeed; //How fast the sword is thrusted forward
    public float thrustTime; //How long the thrust lasts until it returns to original spot

    [Range(0, 5)]
    public float Radius = 1f; //How far the sword is away from player in normal position
    [Range(0, 10)]
    public float maxRadius; //How far the sword is allowed to go during thrust

    public float dangerTime = 1f; //How long the sword can be in the danger zone till the enemy dies

    public float basicHitDistance; //How far the enemy will go on a basic hit
    public float thrustHitDistance; //How far the enemy will go on a thrust hit
    public float blockHitDistanceTPunish; //How far the enemy will go on blocked hit on thrust
    public float blockHitDistanceTDefensive; //How far player blocking will be pushed on thrust
    public float noTHitDistancePunish; //normal hit push distance
    public float noTHitDistanceDefensive;

    public Vector3 blockedOffset;

    public GameObject Owner; //Who owns the sword
    private GameObject enemy;

    public Coroutine thrust;
    public Coroutine killMovement;

    private Coroutine count; //The coroutine timer for the danger zone

    private Animator SlimeAnim; //animator of owner slime of sword
    private Rigidbody rb; //Rigid body of sword

    private float theY = 1; //The static height of the sword 
    private float angle; //angle for sword when using keyboard
    private float h;
    private float v;

    private Vector3 center; //Basically location of player 
    private Vector3 offset; //changing sword location based on rotation and keeping radius from player

    private bool lastState; //handeling thrust button pushes/button pushes in general
    private bool swordThrusted = false; //Started thrust
    private bool inZone = false; //In the zone or not
    //private bool hasBeenBlocked = false;

    private PlayerMovement playerMovement; //Player movement script holder
    private BlockScript blockScript; //Block Hitbox Script
    private HitPercentageHolder hitPercentageHolder;

    private string inputHoriSw;
    private string inputVerSw;
    private string inputThru;

    private Vector3 swordTarget;
    private Vector3 pointPosition;

    [HideInInspector]
    public bool thrustHit;
    void Start ()
    {
        rb = GetComponent<Rigidbody>(); 
        SlimeAnim = Owner.transform.GetChild(0).GetComponent<Animator>();

        playerMovement = Owner.GetComponent<PlayerMovement>();
        blockScript = GetComponentInChildren<BlockScript>();
        hitPercentageHolder = GameObject.Find("PercentageHits").GetComponent<HitPercentageHolder>();

        transform.localPosition = (Owner.transform.position * Radius);

        inputHoriSw = playerMovement.inputHoriSw;
        inputVerSw = playerMovement.inputVerSw;
        inputThru = playerMovement.inputThru;
        MouseInput = playerMovement.MouseInput;

        Physics.IgnoreCollision(Owner.GetComponent<Collider>(), GetComponent<Collider>());

    }
	
	// Update is called once per frame
	void Update () {
        center = Owner.transform.position; //center variable is set to player location

        //Dont let sword go past max radius during thrust
        if (!swordDone && !isBlocked)
        {
            if (Vector3.Distance(transform.position, Owner.transform.position) > maxRadius)
            {
                rb.velocity = Vector3.zero;
            }
        }

    }
    private void FixedUpdate()
    {
        transform.LookAt(2 * transform.position - new Vector3(center.x, transform.position.y, center.z)); //Set rotation of sword to look away from player

        if (swordDone && !playerMovement.dashProcess && !isBlocked) //If the sword is not being thrusted, and player is not in dash take input for moving sword
        {
            MoveSword();
        }
        if (isBlocked) {
            Blocked();
        }

        //Treat a GetAxis like GetButtonDown
        swordThrusted = LikeOnKeyDown(inputThru);
        //Check when thust is pressed
        if (Input.GetAxis(inputThru) != 0) //Checks if the fire button is pressed and the sword is not already thrusted
        {
            //When key is up and not dashing or thrusting start a thrust 
            if (swordThrusted && swordDone && !playerMovement.dashProcess)
            {
                ThrustSword();
            }
        }
    }

    public void MoveSword() {
        //Key input
        if (!MouseInput) {
            //Get horizontal sword movement (Like movement but just horizontal)
            h = Input.GetAxisRaw(inputHoriSw);
            v = Input.GetAxisRaw(inputVerSw);

            //swordPoint.transform.position = center;
            if (Mathf.Abs(h) > 0.05 || Mathf.Abs(v) > 0.05) {
                float pointX = center.x + Radius * h;
                float pointZ = center.z + Radius * -v;
                pointPosition = new Vector3(pointX, theY, pointZ);
            }
            offset = transform.position - center;
            Vector3 swordTarget = Vector3.RotateTowards(offset, (pointPosition - center), swordMouseSpeed, 0f);
            swordTarget.y = center.y + theY;
            transform.position = center + swordTarget.normalized * Radius;

        } //mouse input
        else {
            //Generate a plane horizontally from the player position to get raycast hits
            Plane playerPlane = new Plane(Vector3.up, center);
<<<<<<< HEAD
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //ray from camera angle diraction from mouse position
            float hitdist = 0.0f; //hit distance
            //when ray hits generated plane
            if (playerPlane.Raycast(ray, out hitdist)) {
                //location where raycast hit
                Vector3 targetPoint = ray.GetPoint(hitdist);
                //Debug.Log(targetPoint);
                //set offset (basically radius)
                offset = transform.position - center;
                //set the height to static variable 
                //Rotate sword towards ray hit position based on speed 
                Vector3 swordTarget = Vector3.RotateTowards(offset, (targetPoint - center), swordMouseSpeed, 0f);
                swordTarget.y = center.y + theY;
                //Set position to that rotated towards
                transform.position = center + swordTarget.normalized * Radius;

            }

=======
            if (Camera.main != null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //ray from camera angle diraction from mouse position
                float hitdist = 0.0f; //hit distance
                //when ray hits generated plane
                if (playerPlane.Raycast(ray, out hitdist)) {
                    //location where raycast hit
                    Vector3 targetPoint = ray.GetPoint(hitdist);
                    //Debug.Log(targetPoint);
                    //set offset (basically radius)
                    offset = transform.position - center;
                    //set the height to static variable 
                    //Rotate sword towards ray hit position based on speed 
                    Vector3 swordTarget = Vector3.RotateTowards(offset, (targetPoint - center), swordMouseSpeed, 0f);
                    swordTarget.y = center.y + theY;
                    //Set position to that rotated towards
                    transform.position = center + swordTarget.normalized * Radius;

                }
            }
>>>>>>> Added triggers and lots of backend
        }
        //Keep static height 
        transform.position = new Vector3(transform.position.x, (theY + center.y), transform.position.z);
    }

    private bool LikeOnKeyDown(string axis)
    {
        //Converts axis input to just true/false like button press
        var currentState = Input.GetAxis(axis) > 0.1;
        if (currentState && lastState)
        {
            return false;
        }

        lastState = currentState;
        return currentState;
    }

    private void ThrustSword()
    {
        //Started thrust sword is not done thrust
        swordDone = false;

        if (AudioManager.getInstance() != null)
        {
            AudioManager.getInstance().Find("thrust").source.Play();
        }

        //play thrust animation
        SlimeAnim.Play("THRUST");

        //player looks where he is thrusting
        Owner.transform.LookAt(new Vector3(transform.position.x, Owner.transform.position.y, transform.position.z));

        //change velocity of sword to set speed forwards
        rb.velocity += transform.forward * thrustSpeed;

        //Start the trust debuff handler in PlayerMovement script
        playerMovement.thrustDebuff = true;
        //half speed of player
        playerMovement.currentSpeed = playerMovement.currentSpeed / 2;
        //Start the heal debuff timer in PlayerMovement script
        playerMovement.killMovement = playerMovement.StartCoroutine(playerMovement.KillMovement(playerMovement.thrustDebuffTime));
        //Stop the thrust (return to hand and set velocity to zero after set time)
        thrust = StartCoroutine(StopThrust(thrustTime));

        if (AudioManager.getInstance() != null && AudioManager.getInstance().Find("thrust").source.mute)
        {
            AudioManager.getInstance().Find("thrust").source.Stop();
            AudioManager.getInstance().Find("thrust").source.mute = false;
        }
    }

    private IEnumerator StopThrust(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        swordDone = true;
        rb.velocity = Vector3.zero;
        //Can move sword again set to position incase mouse position was changed during thrust
        MoveSword();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent != Owner.transform && other.gameObject.name == "DefendArea" && swordDone && !inZone) //Checks if the zone is it's own and if it's not in the zone yet
        {
            enemy = other.transform.parent.gameObject;
            PlayerMovement enemyPM = enemy.GetComponent<PlayerMovement>();
            enemyPM.SlimeAnim.Play("TAKEDAMAGE2");
            Debug.Log(Owner.name + " !Hit! " + enemy.name);
            count = StartCoroutine(Countdown(dangerTime)); //Begins the death countdown
        }
        if (other.transform.parent != transform && other.gameObject.GetComponent("BlockScript") != null && !isBlocked) //Checks if the collision is the parent object
        {
            //Starts the block process
            isBlocked = true;
            blockScript.blockSword(other.transform.parent.gameObject);
            SwordScript enemySS = other.transform.parent.gameObject.GetComponent<SwordScript>();
            enemy = enemySS.Owner;
            Debug.Log(Owner.name + " !Blocked! " + enemy.name);
            PlayerMovement enemyPM = enemy.GetComponent<PlayerMovement>();
            playerMovement.blockCenter = (Owner.transform.position + enemy.transform.position) / 2;
            enemyPM.blockCenter = (Owner.transform.position + enemy.transform.position) / 2;
            if (thrustHit)
            {
                if (AudioManager.getInstance() != null)
                {
                    AudioManager.getInstance().Find("blockthrust").source.Play();
                }

                Debug.Log( Owner.name + " PUNISHED");
                enemyPM.startHitSequence(blockHitDistanceTDefensive, hitPercentageHolder.normalBlockPercentage, 0);
                playerMovement.startHitSequence(blockHitDistanceTPunish, hitPercentageHolder.punishBlockPercentage);

                thrustHit = false;
            }
            else
            {
                if (AudioManager.getInstance() != null)
                {
                    AudioManager.getInstance().Find("swordonsword").source.Play();
                }

                Debug.Log(Owner.name + " normal hit.");
                enemyPM.startHitSequence(noTHitDistanceDefensive, 0, 0);
                //playerMovement.startHitSequence(noTHitDistancePunish);
            }
            
        }
        if (other.transform.parent != Owner.transform && other.gameObject.name == "DefendArea" && !swordDone) //Checks if the zone is it's own and if it's not in the zone yet
        {
            if (AudioManager.getInstance() != null)
            {
                AudioManager.getInstance().Find("hurt").source.Play();
            }

            enemy = other.transform.parent.gameObject;
            Debug.Log(Owner.name + " !Dead! " + enemy.name);
            PlayerMovement enemyPM = enemy.GetComponent<PlayerMovement>();
            enemyPM.SlimeAnim.Play("TAKEDAMAGE");
            enemyPM.blockCenter = (enemy.transform.position + Owner.transform.position) / 2;
            enemyPM.startHitSequence(thrustHitDistance, hitPercentageHolder.thrustBodyHitPercentage);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.parent != Owner.transform && other.gameObject.name == "DefendArea" && inZone) //Checks to see if the sword has exited the zone
        {
            StopCoroutine(count); //Resets and stops the death countdown
            inZone = false; //Exits the zone
        }
    }

    private IEnumerator Countdown(float waitTime)
    {
        inZone = true; //Enters the zone
        yield return new WaitForSeconds(waitTime);
        PlayerMovement enemyPM = enemy.GetComponent<PlayerMovement>();
        enemyPM.blockCenter = (enemy.transform.position + Owner.transform.position) / 2;
        enemyPM.startHitSequence(basicHitDistance, hitPercentageHolder.normalBodyHitPercentage);
        inZone = false; //Exits the zone
    }

    public void stopCoroutines()
    {
        StopCoroutine(thrust);
    }

    private void Blocked()
    {
        transform.position = Owner.transform.position + blockedOffset;
    }
}