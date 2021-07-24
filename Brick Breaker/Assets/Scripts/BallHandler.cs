using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHandler : MonoBehaviour
{
    public Rigidbody physics;

    public GameObject playerBar;
    public float constantSpeed;
    public bool ballOn;

    public bool destroyObject;
    public AudioClip[] breakBrick = new AudioClip[4];

    public AudioClip Bounce;

    public AudioSource audioPlayer;


    // Start is called before the first frame update
    void Start()
    {
        physics = this.gameObject.GetComponent<Rigidbody>();
        physics.velocity = new Vector3(0,0,0);
        constantSpeed = 300;
        ballOn = false;
        destroyObject =false;
        playerBar = GameObject.FindGameObjectWithTag("Player");
        audioPlayer = GameObject.FindGameObjectWithTag("Audio Source").GetComponent<AudioSource>();
    }

    public void Launch(){
        physics.velocity = new Vector3(0,constantSpeed,0);
        ballOn = true;
    }

    void OnCollisionEnter(Collision collision){
        //Debug.Log(collision.gameObject.tag);
        audioPlayer.PlayOneShot(Bounce,BackgroundData.sfxVolume);
        switch (collision.gameObject.tag)
        {
            case "Brick":{
                Destroy(collision.gameObject);
                BackgroundData.score++;
                audioPlayer.PlayOneShot(breakBrick[Random.Range(0,4)],BackgroundData.sfxVolume);
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
            case "Bottom Wall":{
                destroyObject = true;
                break;
            }

        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(destroyObject && !audioPlayer.isPlaying){
            Destroy(this.gameObject);
        }
        if(this.transform.position.y - playerBar.transform.position.y > 500 ){
            physics.velocity = new Vector3(physics.velocity.x,-physics.velocity.y,physics.velocity.z).normalized * constantSpeed;
        }
        else{
            physics.velocity = physics.velocity.normalized*constantSpeed;
        }
        if(this.gameObject.transform.position.y < playerBar.transform.position.y){
            destroyObject = true;
        }
    }

}
