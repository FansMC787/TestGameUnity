using UnityEngine;

public class Cameramovement : MonoBehaviour
{


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
[SerializeField] private Transform SpielerCheck;
[SerializeField] private float zoffset = -12.5f;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3 (SpielerCheck.position.x, SpielerCheck.position.y, zoffset) ;
    }
}
