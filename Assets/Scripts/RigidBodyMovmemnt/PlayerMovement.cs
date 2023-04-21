using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public enum PlayerStates
    {
        grounded,
        airborne,
        onwall,
        ledgegrab
    }

    public PlayerStates CurrentState;

    [Header("Physics")]
    public float MaxSpeed;
    public float BackwardsMovmentSpeed;
    public float InAirControl;
    public float XMOV;
    public float YMOV;
    public float Jumpforce;

    private float ActSpeed;

    public float Acceleration;
    public float Deceleration;
    public float DirectionalControl;
    private float InAirTimer;
    private float GroundedTimer;
    private float AdjustmentAmt;

    [Header("Turning")]
    public float TurnSpeedInAir;
    public float TurnSpeedOnWall;

   [Header("Debug")]
    public Text speedometer;

    private PlayerCollision Coli;
    private Rigidbody Rigid;
    private Animator Anim;

    private void Start()
    {
        Coli = GetComponent<PlayerCollision>();
        Rigid = GetComponent<Rigidbody>();
        Anim = GetComponent<Animator>();


        AdjustmentAmt = 1;
    }

    private void Update()
    {

        //speedometer.text = "Speed: " + ActSpeed.ToString();

        XMOV = Input.GetAxis("Horizontal");
        YMOV = Input.GetAxis("Vertical");


        if (CurrentState == PlayerStates.grounded)
        {
            if (Input.GetButtonDown("Jump"))
                JumpUp();

        }
        else if (CurrentState == PlayerStates.airborne)
        {

        }
        else if (CurrentState == PlayerStates.ledgegrab)
        {

        }
        else if (CurrentState == PlayerStates.onwall)
        {

        }

    }

    private void FixedUpdate()
    {
        float Del = Time.deltaTime;
        

        if (CurrentState == PlayerStates.grounded)
        {
            if (GroundedTimer < 10)
                GroundedTimer += Del;
            //float inputmag = new Vector2(XMOV, YMOV).normalized.magnitude;
            float inputmag = new Vector2(XMOV, YMOV).magnitude;
            float targetSpd = Mathf.Lerp(BackwardsMovmentSpeed, MaxSpeed, YMOV);

            speedometer.text = "inputmag: " + inputmag.ToString();

            lerpSpeed(inputmag, Del, targetSpd);

            MovePlayer(XMOV, YMOV, Del);
        }
        else if (CurrentState == PlayerStates.airborne)
        {

        }
        else if (CurrentState == PlayerStates.ledgegrab)
        {

        }
        else if (CurrentState == PlayerStates.onwall)
        {

        }
    }

    void JumpUp() 
    {
        Rigid.AddForce(Vector3.up * Jumpforce, ForceMode.Impulse);
    }

    void lerpSpeed(float Mag, float d, float spd)
    {
        float LaMT = spd * Mag;

        float Accel = Acceleration;
        if (Mag == 0)
            Accel = Deceleration;   

        ActSpeed = Mathf.Lerp(ActSpeed, LaMT, d * Accel);
    }

    void MovePlayer(float hor, float ver, float d)
    {
        Vector3 MovDir = (transform.forward * ver) + (transform.right * hor);
        MovDir = MovDir.normalized;

        if (hor == 0 && ver == 0)
            MovDir = Rigid.velocity.normalized;

        MovDir *= ActSpeed;

        MovDir.y = Rigid.velocity.y;

        float Acel = DirectionalControl * AdjustmentAmt;
        Vector3 LerpVel = Vector3.Lerp(Rigid.velocity, MovDir, Acel*d);
        Rigid.velocity = LerpVel;

        //Rigid.AddForce(MovDir.normalized * Acceleration * 10);
    }

   

    
}

    

