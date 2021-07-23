using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public GameObject[] BrickArray = new GameObject[8];
    public GameObject PlayerBar;
    public GameObject PowerUps;

    public GameObject Ball;

    public GameObject Camera;

    //public bool pause;

    public int ScaleOfBall;

    void spawnBricks(float mx, float my, float MX, float MY){
        for( float i=mx; i < MX; i+=(ScaleOfBall+10)){
            for( float j=my; j<MY; j+=(ScaleOfBall+10)){
                GameObject aBrick = Instantiate(BrickArray[Random.Range(0,8)]);
                aBrick.transform.position =  new Vector3(i,j,0);
                aBrick.transform.localScale = new Vector3(ScaleOfBall,ScaleOfBall,ScaleOfBall);
            }
        }
    }

    void spawnPlayer(){
        PlayerBar = Instantiate(PlayerBar);
        PlayerBar.transform.position = new Vector3(0,15,0);
    }

    void spawnBall(){
        Ball = Instantiate(Ball);
        Ball.transform.position = PlayerBar.transform.position + new Vector3(0,15,0);
        Debug.Log("PlayerBar "+PlayerBar.transform.position.ToString());
        Debug.Log("Ball "+Ball.transform.position.ToString());
    }
    void Start()
    {
        ScaleOfBall = 30;
        spawnBricks(-500,200,500,1000);
        spawnPlayer();
        spawnBall();
        //Camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.P)){
            Debug.Log("Escape pressed");
            PlayerBar.GetComponent<PlayerBar>().pause = !PlayerBar.GetComponent<PlayerBar>().pause;
            GameObject[] ballObjects = GameObject.FindGameObjectsWithTag("PlayerBall");
            for(int i=0; i< ballObjects.Length; i++){
                ballObjects[i].GetComponent<BallHandler>().pause = !ballObjects[i].GetComponent<BallHandler>().pause;
            }
        }
    }

    void FixedUpdate(){
        GameObject[] brickObjects = GameObject.FindGameObjectsWithTag("Brick");
        float y = int.MaxValue;
        for(int i=0;i <brickObjects.Length;i++){
            y = y > brickObjects[i].transform.position.y ? brickObjects[i].transform.position.y:y;
        }
        if(y-PlayerBar.transform.position.y>200){
            PlayerBar.transform.position = Vector3.Lerp(PlayerBar.transform.position, new Vector3(PlayerBar.transform.position.x,y-PlayerBar.transform.position.y,PlayerBar.transform.position.z),0.05F);
        }
        Camera.transform.position = Vector3.Lerp(Camera.transform.position, new Vector3(Camera.transform.position.x,PlayerBar.transform.position.y+200,Camera.transform.position.z),0.05F);
        
    }
}
