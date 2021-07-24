using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
public class World : MonoBehaviour
{
    public GameObject[] BrickArray = new GameObject[8];
    public GameObject PlayerBar;
    public GameObject PowerUps;

    public GameObject Ball;

    public GameObject Camera;
    public bool gameOver;
    public int ScaleOfBall;

    public int ScaleOfBrick;

    public Text score;

    public AudioClip lost;
    public AudioClip won;
    public AudioSource audioPlayer;


    void spawnBricks(float mx, float my, float MX, float MY){
        for( float i=mx; i < MX; i+=(ScaleOfBrick)){
            for( float j=my; j<MY; j+=(ScaleOfBrick)){
                GameObject aBrick = Instantiate(BrickArray[Random.Range(0,8)]);
                aBrick.transform.SetParent(this.gameObject.transform);
                aBrick.transform.position =  new Vector3(i,j,0);
                aBrick.transform.localScale = new Vector3(ScaleOfBrick-5,ScaleOfBrick-5,10);
                //randomScaleY = Random.Range(30,ScaleOfBrick);
            }
            //randomScaleX = Random.Range(30,ScaleOfBrick);
        }
    }

    void spawnPlayer(){
        PlayerBar = Instantiate(PlayerBar);
        PlayerBar.transform.SetParent(this.gameObject.transform);
        PlayerBar.transform.position = new Vector3(0,15,0);
    }

    void spawnBall(){
        Ball = Instantiate(Ball);
        Ball.transform.SetParent(this.gameObject.transform);
        Ball.transform.position = PlayerBar.transform.position + new Vector3(0,15,0);
        Debug.Log("PlayerBar "+PlayerBar.transform.position.ToString());
        Debug.Log("Ball "+Ball.transform.position.ToString());
    }
    void Start()
    {
        ScaleOfBall = 30;
        ScaleOfBrick = 50;
        gameOver = false;
        spawnBricks(-500,200,500,700);
        spawnPlayer();
        spawnBall();
        audioPlayer = GameObject.FindGameObjectWithTag("Audio Source").GetComponent<AudioSource>();
        //Camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.P)){
            Menu.loadMenu();
        }
        score.text = BackgroundData.score.ToString();
        if(BackgroundData.score>BackgroundData.highScore){
            BackgroundData.highScore = BackgroundData.score;
        }
    }

    void FixedUpdate(){
        GameObject[] brickObjects = GameObject.FindGameObjectsWithTag("Brick");
        if(brickObjects.Length == 0 && !gameOver){
            audioPlayer.PlayOneShot(won,BackgroundData.sfxVolume);
            gameOver = true;
        }
        float y = int.MaxValue;
        for(int i=0;i <brickObjects.Length;i++){
            y = y > brickObjects[i].transform.position.y ? brickObjects[i].transform.position.y:y;
        }
        if(y-PlayerBar.transform.position.y>200){
            PlayerBar.transform.position = Vector3.Lerp(PlayerBar.transform.position, new Vector3(PlayerBar.transform.position.x,y-PlayerBar.transform.position.y,PlayerBar.transform.position.z),0.05F);
        }
        Camera.transform.position = Vector3.Lerp(Camera.transform.position, new Vector3(Camera.transform.position.x,PlayerBar.transform.position.y+200,Camera.transform.position.z),0.05F);
        if(GameObject.FindGameObjectWithTag("PlayerBall")==null && !gameOver){
            audioPlayer.PlayOneShot(lost,BackgroundData.sfxVolume);
            gameOver = true;
        }
        if(gameOver && !audioPlayer.isPlaying){
            SceneManager.LoadScene(sceneBuildIndex:3);
        }
    }
}
