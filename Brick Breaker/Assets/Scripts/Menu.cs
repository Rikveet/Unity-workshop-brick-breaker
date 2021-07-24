using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
public class Menu : MonoBehaviour
{
    public int linkTo;
    public static GameObject currentWorld;
    
    public void LoadLevel(){
        SceneManager.LoadScene(sceneBuildIndex:linkTo);
    }

    public static void loadMenu(){
        currentWorld = GameObject.FindGameObjectWithTag("NotPause");
        currentWorld.SetActive(false);
        Debug.Log(currentWorld.name);
        Instantiate(Resources.Load("PauseMenu") as GameObject);
        GameObject PauseMenu = GameObject.FindGameObjectWithTag("Pause");
        PauseMenu.transform.Find("Menu").transform.Find("MUSIC").transform.Find("Music Vol").GetComponent<Scrollbar>().value = BackgroundData.musicVolume;
        PauseMenu.transform.Find("Menu").transform.Find("SFX").transform.Find("Sfx Vol").GetComponent<Scrollbar>().value = BackgroundData.sfxVolume;
    }
    public static void closeOptions(){
        Destroy(GameObject.FindGameObjectWithTag("Pause"));
        currentWorld.SetActive(true);
    }

    public void quit(){
        Application.Quit();
    }

    public void sfxVolumeChange(Scrollbar slider){
        BackgroundData.sfxVolume = slider.value;
    }

    public void musicVolumeChange(Scrollbar slider){
        BackgroundData.musicVolume = slider.value;
    }
}
