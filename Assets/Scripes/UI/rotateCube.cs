using UnityEngine;

public class rotateCube : MonoBehaviour
{
    public float speed = 10f;
    public Vector3 rotation = new Vector3(0,0,0);
    public float speedUpAmount = 5f;
    public float originalSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotation * speed * Time.unscaledDeltaTime);
    }
    public void SpeedUp()
    {
        speed += speedUpAmount;
    }
    public void ResetSpeed()
    {
        speed = originalSpeed;
    }
}
