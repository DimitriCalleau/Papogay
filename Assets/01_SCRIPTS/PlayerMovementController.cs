using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    InputPapogay inputs;

    public LayerMask floor;

    public Animator playerAnimator;

    public Transform floorDetectorPosition;
    public float floorDetectionRayRange;
    public float floorDetectionSphereRange;
    [Header("Movement")]

    public float turnSmooth;
    public float gravity;
    Vector2 move;
    Vector3 moveDir;
    Quaternion currentRollDir;

    public GameObject skin;
    Quaternion rotation;
    float currentSpeed;

    Transform cam;
    PlayerStats stats;

    bool isrolling;
    bool isboosted;

    bool isInvincible;
    float rollTimer;
   
    void Awake()
    {
        inputs = new InputPapogay();
        inputs.Actions.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        inputs.Actions.Move.canceled += ctx => move = Vector2.zero;
        inputs.Actions.Roll.performed += ctx => TriggerRoll();
    }
    void Start()
    {
        cam = GameManager.Instance.mainCam.transform;
        stats = GameManager.Instance.playerStats;
    }
    void Update()
    {
        if(GameManager.Instance.gameState.pause == false)
        {
            if (rollTimer > 0)
            {
                Roll(currentRollDir);
                rollTimer -= Time.deltaTime;
            }
            else
            {
                GameManager.Instance.playerStats.Invincibility(false);
                Move();
            }
            switch (isrolling)
            {
                case true:
                    if (isboosted == true)
                    {
                        currentSpeed = stats.rollSpeed * stats.boostFactor;
                    }
                    else
                    {
                        currentSpeed = stats.rollSpeed;
                    }
                    break;

                case false:
                    if (isboosted == true)
                    {
                        currentSpeed = stats.speed * stats.boostFactor;
                    }
                    else
                    {
                        currentSpeed = stats.speed;
                    }
                    break;
            }
        }
    }
    public void Move()
    {
        float targetRotation = Mathf.Atan2(move.x, move.y) * Mathf.Rad2Deg + cam.eulerAngles.y;
        rotation = Quaternion.Euler(skin.transform.rotation.x, cam.eulerAngles.y, skin.transform.rotation.z); //Vecteur de rotation
        Collider[] groundCheck = Physics.OverlapSphere(floorDetectorPosition.position, floorDetectionSphereRange, floor);
        Vector3 securityGravity = Vector3.zero;
        if(groundCheck.Length == 0)
        {
            securityGravity = Vector3.down;
        }

        if (move != Vector2.zero)
        {
            playerAnimator.SetBool("Forward", true);
            moveDir = Quaternion.Euler(FloorInclinaison()) * (Quaternion.Euler(0, targetRotation, 0) * (Vector3.forward * currentSpeed)) + securityGravity;
        }
        else
        {
            playerAnimator.SetBool("Forward", false);
            moveDir = Vector3.zero; 
        }

        transform.Translate(moveDir * Time.deltaTime);
        skin.transform.rotation = Quaternion.Lerp(skin.transform.rotation, rotation, turnSmooth); 

        currentRollDir = Quaternion.Euler(0f, targetRotation, 0f);
    }

    public Vector3 FloorInclinaison()
    {
        RaycastHit hitFloor;
        
        if (Physics.Raycast(floorDetectorPosition.position, Vector3.down, floor))
        {
            Physics.Raycast(floorDetectorPosition.position, Vector3.down, out hitFloor, floorDetectionRayRange, floor);
            float testX = Mathf.Atan2(hitFloor.normal.z, hitFloor.normal.y) * Mathf.Rad2Deg;
            float testZ  = Mathf.Atan2(- hitFloor.normal.x, hitFloor.normal.y) * Mathf.Rad2Deg;
            return new Vector3(testX, 0, testZ);//floor rotation at given point
        }
        else
        {
            return Vector3.zero;
        }
    }

    void TriggerRoll()
    {
        rollTimer = GameManager.Instance.playerStats.invincibilityTime;
        playerAnimator.SetTrigger("Roulade");
    }
    public void Roll(Quaternion rollDirection)
    {
        isrolling = true;
        skin.transform.rotation = rollDirection;
        GameManager.Instance.playerStats.Invincibility(true);
        isInvincible = true;

        Vector3 rollDir = Quaternion.Euler(FloorInclinaison()) * rollDirection * (Vector3.forward * GameManager.Instance.playerStats.rollSpeed);

        transform.Translate(rollDir * Time.deltaTime);
    }

    public void Boost(float boosTime, float boostSpeed)
    {

    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(floorDetectorPosition.position, Vector3.down);
        Gizmos.DrawWireSphere(floorDetectorPosition.position, floorDetectionSphereRange);
    }

    void OnEnable()
    {
        inputs.Actions.Enable();
    }
    void OnDisable()
    {
        inputs.Actions.Disable();
    }
}
