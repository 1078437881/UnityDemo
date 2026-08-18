using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 1.0f;
    
    private GameObject mainCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        mainCamera.transform.position += new Vector3(horizontal, vertical, 0) * Time.deltaTime * speed;
        
    }
}
