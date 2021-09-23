using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour {

    void Awake()
    {       
        //Make this script persistent(Not destroy when loading a new level)
        DontDestroyOnLoad(this);

        Time.timeScale = 1.0f; //In case some game does not UN-pause..
    }

	void OnGUI () {    

        //Detect if we're in the main menu scene
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            MainMenuGUI();
        }
        else
        {
            //Game scene
            InGameGUI();
        }	
	}

    void StartGame(int nr)
    {
        SceneManager.LoadScene(nr);
    }

    void InGameGUI()
    {
        //Top-right
        GUILayout.BeginArea(new Rect(0, 0, Screen.width, 50));
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Back to menu"))
        {
            Destroy(gameObject); //Otherwise we'd have two of these..
            SceneManager.LoadScene(0);
        }
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }

    public GUIStyle invisibleButton;

    void MainMenuGUI()
    {
        int leftPix = (Screen.width - 600) / 2;
        int topPix = (Screen.height - 450) / 2;
  

        if (GUI.Button(new Rect(leftPix, topPix, 204, 158), "Start Game", invisibleButton))
        {
            StartGame(1);
        }



        GUI.color = Color.black;

        GUILayout.BeginArea(new Rect(Screen.width/2-150, Screen.height/2-100, 300, 200));
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        GUILayout.BeginVertical();
        GUILayout.FlexibleSpace();

        GUILayout.Label("Select a game!");
       
        GUILayout.FlexibleSpace();
        GUILayout.EndVertical();

        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.EndArea();

    }


   

}
