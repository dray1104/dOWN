using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class playermovement : MonoBehaviour
{

    [Header("Player Movement Settings")]
    public Rigidbody rb;
    public Vector3 input;
    [Header("Player dash Settings")]
    public bool dashcooldown = false;
    public float playerrotation = -90f;
    public float dashspeed = 30f;
    public int health = 4;
    public int maxhealth = 4;
    [Header("Player animation Settings")]
    private int walking;
    [Header("Player Audio Settings")]
    public Vector3 CameraOffset = new Vector3(0, 20, -10);
    [Header("Player UI Settings")]
    public GameObject pauseMenu;
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

        if (health <= 0)
        {
            Destroy(gameObject);
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
            Debug.LogWarning("walking");
        }else{
            Player.PlayerAnim.SetInteger("walking", 0);
            Debug.LogWarning("not walking");
        }
        
    }
     public void lookAtMouse()
    {
        Ray cameraRay = Player.PlayerCam.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, new Vector3(0, transform.position.y, 0));
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Vector3 direction = pointToLook - transform.position;

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(-direction);
                transform.rotation = targetRotation * Quaternion.Euler(0, 90, 0);
            }

            Debug.DrawLine(cameraRay.origin, pointToLook, Color.green);
        }
        else
        {
            Debug.LogWarning("Ray did not hit the ground plane.");
        }
    }  
    public void LateUpdate()
    {
        Vector3 cameraOffset = transform.position + CameraOffset; // Adjust the offset as needed
        Vector3 smoothedPosition = Vector3.Lerp(Player.PlayerCam.transform.position, cameraOffset, 0.1f); // Adjust the smoothing factor as needed
        Player.PlayerCam.transform.position = smoothedPosition;
    }
   
}
       

