using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
  
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Player.State = "attacking";
            Player.PlayerAnim.Play("Attack1");
        }
    }
}
