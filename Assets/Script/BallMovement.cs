using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BallMovenet : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;
    public float Force;

    private Vector3 mousePressDownPos;
    private Vector3 mouseReleasePos;

    private Rigidbody rb;
    
    private bool isShoot=false;
    private float forceMultiplier = 5;
    private void Start()
    {
        
        rb = GetComponent<Rigidbody>();
       


    }
    
    private void Update()
    {
       
    }
   

    private void OnMouseDown()
    {
       mousePressDownPos = Input.mousePosition;

    }

    private void OnMouseDrag()
    {
        
        Vector3 forceInit = (Input.mousePosition - mousePressDownPos);
        Debug.Log("좌표측정 성공?:"+forceInit);
        Vector3 forceV = (new Vector3(forceInit.x, forceInit.y, forceInit.y)) * forceMultiplier;
        if (!isShoot)
            DrawTrajectory.Instance.UpdateTracjetory(forceV, rb,transform.position);
    }
    private void OnMouseUp()
    {
        DrawTrajectory.Instance.HideLine();
        mouseReleasePos = Input.mousePosition;
        shoot( mouseReleasePos - mousePressDownPos);
    }

    
    private void shoot(Vector3 Force)
    {
       
        if (isShoot)
            return;
      
        rb.AddForce(new Vector3(Force.x,Force.y,Force.y)*forceMultiplier);
        
        isShoot = true;

        Spawner.Instance.RetrySpawnRequest();
        
        
       
    }
}
