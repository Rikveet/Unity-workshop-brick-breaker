using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBar : MonoBehaviour
{
    public Rigidbody physics;
    public int constantSpeed;

    // Start is called before the first frame update
    void Start()
    {
        physics = this.GetComponent<Rigidbody>();
        constantSpeed = 500;
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.position.x >-500 && this.transform.position.x <500){
            physics.velocity = new Vector3(1,0,0) * Input.GetAxisRaw("Horizontal") * constantSpeed;
            GameObject[] balls = GameObject.FindGameObjectsWithTag("PlayerBall");
            if(balls!=null){
                for(int i=0; i< balls.Length; i++){
                    if(!balls[i].GetComponent<BallHandler>().ballOn){
                        balls[i].transform.position = Vector3.Lerp(balls[i].transform.position, this.transform.position + new Vector3(0,15,0), 0.5F);
                    }
                }
            }
            
        }else{
            if(this.transform.position.x >500){
                this.transform.position= new Vector3(499, this.transform.position.y, this.transform.position.z);
            }else{
                this.transform.position= new Vector3(-499, this.transform.position.y, this.transform.position.z);
            }
        }

        if(Input.GetKeyUp("space")){
            GameObject[] ballObjects = GameObject.FindGameObjectsWithTag("PlayerBall");
            for(int i = 0; i<ballObjects.Length; i++){
                if(!ballObjects[i].GetComponent<BallHandler>().ballOn){
                    ballObjects[i].GetComponent<BallHandler>().Launch();
                    break;
                }
            }
        }
    }
}
