using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//namespace Slimecing.Slime { }
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {
    Rigidbody rb; //Rigidbody for the slime
    [HideInInspector]
    public Animator SlimeAnim; //Animation Controller for slime

    public float speed; //Movement speed of slime
    public float rotSpeed; //Rotation speed of slime
    public float dashForce; //Amount of force applied forward on slime when dashing (Dashing is AddForce)
    public float dashLength; //Time of dash. How long the dash goes until velocity is set to zero
    public float dashCooldown; //Time until can dash again
    public float thrustDebuffTime; //How long thrusting debuf will last after a thrust
    public float hitTime;
    public float hitSpeed = 100f;
    public float fallRange = 0.2f;
    public float slowSpeed = 0.1f;

    public Coroutine killMovement;
    public Coroutine getHit;

    private Vector3 movement; //Handling movement of character
    private Vector3 hitDirection;
    private Vector3 lastSlowPos;

    public Vector3 blockCenter;

    private float charge = 1; //Dash charge (Mostly here for held dash)
    //private float previousY;

    private bool isDashing = false; //When you dash this is set to true for when the dash button is down (Dash process started)
    private bool dashCooldownSwitch = true; //When this is false you are unable to dash. Set true when thrustdebuffTime has expired
    //private bool isInBounds = true;

    private SwordScript swordScript; //Holder for the sword script
    private WinChecker winChecker;
    public ParticleSystem SlimeTrail; //SlimeTrail particlesystem

    private float timer = 0;

    [HideInInspector]
    public float currentSpeed; //Current speed of player used to change speed without losing set speed. Used by SwordScript 
    [HideInInspector]
    public bool thrustDebuff; //Is true when the player is experiencing a debuf. Used by SwordScript
    [HideInInspector]
    public bool isHit;
    [HideInInspector]
    public bool isSlowing;
    [HideInInspector]
    public bool dashProcess = false; //On when the player starts a dash and false when dash ends. For swordscript to change offset of sword.

    public bool MouseInput;
    public string inputDash;
    public string inputVer;
    public string inputHori;
    public string inputHoriSw;
    public string inputVerSw;
    public string inputThru;
    public string emoteOne;

    public bool isAlive;

    private float h;
    private float v;

    public float slimePercentage = 0;
    public float minPercent = -2;
    public float maxPercent = 100;

    public bool inv;

    public float health;
    public GameObject Sword;
    public GameObject Hat;
    private void Start()
    {
        isAlive = true;

        inv = true;

        currentSpeed = speed; //Set current speed to set speed
        rb = GetComponent<Rigidbody>(); //Get rigidbody of slime character
        SlimeAnim = transform.GetChild(0).GetComponent<Animator>(); //Get the animator of slime (Located as child as of test model)

        swordScript = Sword.GetComponent<SwordScript>(); //Find the sword and the attached script

        GameObject dataH = GameObject.Find("DataHandler"); 
        winChecker = dataH.GetComponent<WinChecker>(); 
        health = PublicStatHandler.GetInstance().health;

        StartCoroutine(IFrames(0.4f));
    }
    private void Update () {

        movement = Vector3.zero; //Set movement to zero to be changed later. Makes for accurate movement by frame

        //Input for movement
        h = Input.GetAxis(inputHori);
        v = Input.GetAxis(inputVer);
        //Set to a vector
        movement = new Vector3(h, 0, v);

        //Setting rotation of player. Only is set when movement is not zero (Not always rotating). Under thrust debuff rotation does not happen.
        if (movement != Vector3.zero && !thrustDebuff)
        {
            //Slerp rotation to movement vector. Slerp just makes the rotation smooth.
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), rotSpeed);
        }

        //Dash starter. Can't dash when dash button not pressed, dash button is already pressed, dash is under cooldown
        if(Input.GetAxis(inputDash) != 0 && !isDashing && dashCooldownSwitch)
        {
            //You are preparing to dash
            isDashing = true;
            //Dash cooldown has started
            dashCooldownSwitch = false;

            //Charge functionality
            /*charge += 0.5f;

            if (charge > 100)
            {
                charge = 100;
            }*/
        }

        //Dash button is released
        if(Input.GetAxis(inputDash) == 0 && isDashing)
        {
            //dashing no longer pressed
            isDashing = false;
            //Start dash
            Dash();
        }
        //previousY = transform.position.y;

        HealthCheck();
    }

    //Handels adding the force and starting the necessary coroutines for a dash
    private void Dash()
    {

        if (AudioManager.getInstance() != null)
        {
            AudioManager.getInstance().Find("dash").source.Play();
        }
        //You have started your dash process
        dashProcess = true;

        //Start a dash animation
        SlimeAnim.Play("SLIDE");

        //Add the dash force to the player
        rb.AddForce(transform.forward * (dashForce * charge));
        //Start the dash length coroutine
        StartCoroutine(StopDash(dashLength));
        //Start the dash cooldown
        StartCoroutine(DashCoolDown(dashCooldown));
        //charge = 0;
    }

    public void startHitSequence(float forceAddOn, float percent)
    {
        slimePercentage += percent;
        if (slimePercentage > maxPercent)
        {
            slimePercentage = maxPercent;
        }
        else if (slimePercentage < minPercent)
        {
            slimePercentage = minPercent;
        }
        isHit = true;
        hitDirection = transform.position - blockCenter;
        hitDirection = new Vector3(hitDirection.x, 0f, hitDirection.z);
        if (!inv)
        {
            rb.AddForce(hitDirection * /*hitSpeed **/ (forceAddOn + slimePercentage));
        }
        Debug.Log(this.name + " Force: " + rb.velocity.magnitude);
        isHit = false;
        getHit = StartCoroutine(gotHit(hitTime));
    }

    public void startHitSequence(float forceAddOn, float percent, float mod)
    {
        slimePercentage += percent;
        if (slimePercentage > maxPercent)
        {
            slimePercentage = maxPercent;
        }
        else if (slimePercentage < minPercent)
        {
            slimePercentage = minPercent;
        }
        isHit = true;
        hitDirection = transform.position - blockCenter;
        hitDirection = new Vector3(hitDirection.x, 0f, hitDirection.z);
        if (!inv)
        {
            rb.AddForce(hitDirection * /*hitSpeed **/ (forceAddOn + (slimePercentage * mod)));
        }
        Debug.Log(this.name + " Force: " + rb.velocity.magnitude);
        isHit = false;
        getHit = StartCoroutine(gotHit(hitTime));
    }

    private void HealthCheck()
    {
        if (health <= 0 && isAlive)
        {
            Debug.Log("Dead " + this.name);
            isAlive = false;
            winChecker.Dead(this.gameObject);
        }
    }
    //Basically swiches on a switch when cooldown is done so player can dash again
    private IEnumerator DashCoolDown(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        dashCooldownSwitch = true;
    }

    //No longer in dash process, velocity is set to zero to stop huge dash force.
    private IEnumerator StopDash(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        dashProcess = false;
        rb.velocity = Vector3.zero;
    }

    private IEnumerator IFrames (float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        inv = false;
    }

    //Started after thrust changes speed back to normal and kills the debuff switch
    public IEnumerator KillMovement(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        currentSpeed = speed;
        thrustDebuff = false;
    }

    public IEnumerator gotHit(float hitTime)
    {
        hitDirection = transform.position - blockCenter;
        yield return new WaitForSeconds(hitTime);
        isHit = false;
        isSlowing = true;
    }

    //Handeling stuff like movement and dash that will depend heavily on frame rate
    private void FixedUpdate()
    {
        //move to movement vector
        if (!isHit)
        {
            rb.MovePosition(rb.position + movement * currentSpeed * Time.fixedDeltaTime);
        }

        //Set sword to behind player when dash is started
        if (dashProcess)
        {
            swordScript.transform.position = transform.position + swordScript.Radius * -transform.forward.normalized;
        }

        timer += Time.deltaTime;

        if (movement != Vector3.zero && timer >= 0.1f)
        {
            SlimeTrail.Play();
            timer = 0;
        }

        /*if ((Input.GetKey("w") || Input.GetKey("d") || Input.GetKey("s") || Input.GetKey("a")) && timer >= 0.1) //If any inputs are inputted it plays the SlimeTrail particle system
        {
            SlimeTrail.Play();
            timer = 0;
        }*/

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "LevelBounds")
        {
            CameraController camController = Camera.main.GetComponent<CameraController>();

            Transform existingSlime = ExistsInCamList(camController.players, this.gameObject);
            if (existingSlime == null)
            {
                Debug.Log("Slime In Bounds!");
                camController.players.Add(transform);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "LevelBounds")
        {
            CameraController camController = Camera.main.GetComponent<CameraController>();

            Transform existingSlime = ExistsInCamList(camController.players, this.gameObject);
            if (existingSlime != null) 
            {
                Debug.Log("Slime Out Of Bounds!");
                camController.players.Remove(existingSlime);
            }
            
        }
    }

    private Transform ExistsInCamList(List<Transform> pList, GameObject slime)
    {
        Transform existingPlayer = null;
        foreach (Transform slimeTransform in pList)
        {
            if(slimeTransform == slime.transform)
            {
                existingPlayer = slimeTransform;
            }
        }
        return existingPlayer;
    }

    public void stopCoroutines()
    {
        StopCoroutine(killMovement);
    }

    public void setUpControls(int i)
    {
        //int currentController = PublicStatHandler.GetInstance().controllers[i];

        MouseInput = PublicStatHandler.GetInstance().Inputs[i].mouseInput;
        inputDash = PublicStatHandler.GetInstance().Inputs[i].dashInput;
        inputVer = PublicStatHandler.GetInstance().Inputs[i].yMoveAxis;
        inputHori = PublicStatHandler.GetInstance().Inputs[i].xMoveAxis;
        inputHoriSw = PublicStatHandler.GetInstance().Inputs[i].xSwordAxis;
        inputVerSw = PublicStatHandler.GetInstance().Inputs[i].ySwordAxis;
        inputThru = PublicStatHandler.GetInstance().Inputs[i].ThrustInput;
        emoteOne = PublicStatHandler.GetInstance().Inputs[i].emoteOne;
    }
}
