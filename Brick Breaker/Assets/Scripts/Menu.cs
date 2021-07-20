using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public int linkTo;

    
    public void LoadLevel(){
        SceneManager.LoadScene(sceneBuildIndex:linkTo);
        Debug.Log(linkTo);
    }
    public void quit(){
        Application.Quit();
    }


}
