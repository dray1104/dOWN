using UnityEngine;

public class InitializePlayer : MonoBehaviour
{
   public Camera cam;
   public Animator anim;
    void Start()
    {
        Player.PlayerCam = cam;
        Player.PlayerAnim = anim;
    }

}
