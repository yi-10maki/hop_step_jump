using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour{


    public static int sceneN;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){
        if(Input.GetKeyDown(KeyCode.Return)){
            Debug.Log("Enter");
            if(SceneManager.GetActiveScene().name == "TitleScene"){
                SceneManager.LoadScene("StageSelect");
            }
            if(SceneManager.GetActiveScene().name == "ClearScene"){
                SceneManager.LoadScene("TitleScene");
            }
        }

        if(SceneManager.GetActiveScene().name == "StageSelect"){
            if(Input.GetKeyDown("1")){
                SceneManager.LoadScene("Game1Scene");
            }
            if(Input.GetKeyDown("2")){
                SceneManager.LoadScene("Game2Scene");
            }
            if(Input.GetKeyDown("3")){
                SceneManager.LoadScene("Game3Scene");
            }
        }

        if(SceneManager.GetActiveScene().name == "GameOver"){
            if(Input.GetKeyDown("1")){
                if(sceneN == 1){
                    SceneManager.LoadScene("Game1Scene");
                }else if(sceneN == 2){
                    SceneManager.LoadScene("Game2Scene");
                }else if(sceneN == 3){
                    SceneManager.LoadScene("Game3Scene");
                }
            }
            if(Input.GetKeyDown("2")){
                SceneManager.LoadScene("StageSelect");
            }
        }

        if(SceneManager.GetActiveScene().name == "Game1Scene"){
            sceneN = 1;
            Debug.Log("scene1");
        }
        if(SceneManager.GetActiveScene().name == "Game2Scene"){
            sceneN = 2;
            Debug.Log("scene2");
        }
        if(SceneManager.GetActiveScene().name == "Game3Scene"){
            sceneN = 3;
        }

        if(Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();
        }

    }


    



}
