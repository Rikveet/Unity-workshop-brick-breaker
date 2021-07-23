using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHandler : MonoBehaviour
{
    public Rigidbody physics;
    public float constantSpeed;

    public Vector3 prevVelocity;
    public bool pause;
    public bool ballOn;
    // Start is called before the first frame update
    void Start()
    {
        physics = this.gameObject.GetComponent<Rigidbody>();
        physics.velocity = new Vector3(0,0,0);
        constantSpeed = 300;
        ballOn = false;
        pause = false;
    }

    public void Launch(){
        physics.velocity = new Vector3(0,constantSpeed,0);
        prevVelocity = physics.velocity;
        ballOn = true;
    }
    void OnCollisionEnter(Collision collision){
        //Debug.Log(collision.gameObject.tag);
        switch (collision.gameObject.tag)
        {
            case "Brick":{
                Destroy(collision.gameObject);
                break;
            }
            case "Player":{
                Vector3 collisionPostion = collision.contacts[0].point - collision.gameObject.transform.position;
                 if(collisionPostion.x < -15){
                    physics.velocity = new Vector3(-constantSpeed,constantSpeed,0);
                    //Debug.Log("Left");
                 }
                 else if(collisionPostion.x > 15){
                    physics.velocity = new Vector3(constantSpeed,constantSpeed,0);
                    //Debug.Log("Right");
                 }else{
                    physics.velocity = new Vector3(0,constantSpeed,0);
                    //Debug.Log("Center");
                 }
                break;
            }

        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(pause && physics.velocity.magnitude != 0){
            prevVelocity = physics.velocity;
            physics.velocity = new Vector3(0,0,0);
        }else{
            if(physics.velocity.magnitude == 0){
                physics.velocity = prevVelocity;
            }
            physics.velocity = physics.velocity.normalized*constantSpeed;
        }
        
    }
}
