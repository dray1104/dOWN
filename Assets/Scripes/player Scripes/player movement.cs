using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class playermovement : MonoBehaviour
{

    [Header("Player Movement Settings")]
    public Rigidbody rb;
    public BoxCollider JumpBC;
    public Vector3 input;
    [Header("Player dash Settings")]
    public bool dashcooldown = false;
    public float playerRotationRight = 35;
    public float playerRotationLeft = 145;
    public float jumpForce = 5;
    public float jumpCutMultiplier = 0.5f;
    public bool isGrounded = true;
    public float dashspeed = 30f;
    public int health = 4;
    public int maxhealth = 4;
    [Header("Player animation Settings")]
    private int walking;
    [Header("Player Audio Settings")]
    public Vector3 CameraOffset = new Vector3(0, 0, -20);
    [Header("Player UI Settings")]
    public GameObject pauseMenu;
    public GameObject ShootingArm;
    private pauseMenu pauseMenuScript;
    [Header("Player Ability1 Settings")]
    public bool abilityBeingUsed1 = false;
    public Slot abilitySlot;
    [Header("Player Ability2 Settings")]
    public Slot abilitySlot2;
    public Slot abilitySlot3;
    public Slot abilitySlot4;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        walking = Animator.StringToHash("walking");
        pauseMenuScript = pauseMenu.GetComponent<pauseMenu>();
                 
    }
    void Update()
    {
       
     lookAtMouse();

        
      
        Movement();
        Rotate();
        Jump();

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    void Rotate()
    {
        if(input.x <= 0)
        {
            transform.rotation = Quaternion.Euler(0f, playerRotationLeft, 0f);
        }
        if (input.x >= 0)
        {
            transform.rotation = Quaternion.Euler(0f, playerRotationRight, 0f);
        }
   }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space)&& isGrounded)
        {

            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
            isGrounded = false; 
            Player.PlayerAnim.SetInteger("Jumping", 1);
            Player.State = "airborne";

        }
        if (Input.GetKeyUp(KeyCode.Space)&& rb.linearVelocity.y > 0)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y * jumpCutMultiplier, rb.linearVelocity.z);
        }
        
    }
    void Movement()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.z = Input.GetAxisRaw("Vertical");
        input = new Vector3(input.x, 0f, input.z);
        input = input.normalized;
         transform.Translate(input * Player.speed * Time.deltaTime, Space.World);
        if( input != Vector3.zero && Player.State == "idle"){
            Player.PlayerAnim.SetInteger("walking", 1);
            
        }else{
            Player.PlayerAnim.SetInteger("walking", 0);
           
        }
        
    }
     public void lookAtMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Mathf.Abs(Player.PlayerCam.transform.position.z - ShootingArm.transform.position.z);
        mousePos = Player.PlayerCam.ScreenToWorldPoint(mousePos);
        Vector2 direction = (mousePos - ShootingArm.transform.position);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        ShootingArm.transform.rotation = Quaternion.Euler(0f,0f,angle - 90f);

    }  
    public void LateUpdate()
    {
        Vector3 cameraOffset = transform.position + CameraOffset; // Adjust the offset as needed
        Vector3 smoothedPosition = Vector3.Lerp(Player.PlayerCam.transform.position, cameraOffset, 0.1f); // Adjust the smoothing factor as needed
        Player.PlayerCam.transform.position = smoothedPosition;
    }
    private void OnTriggerEnter(Collider other)
    {
        // Use CompareTag to identify what entered the zone safely
        if (other.CompareTag("Level"))
        {
            Debug.Log("The Player has entered the trigger zone!");
             isGrounded = true; 
              Player.PlayerAnim.SetInteger("Jumping", 0);
              Player.State = "idle";
        }
    }

   
}
       

