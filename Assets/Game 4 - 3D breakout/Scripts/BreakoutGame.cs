using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.IO;

// La escritura de punatajes fue obtenida de este link:https://docs.microsoft.com/en-us/troubleshoot/dotnet/csharp/read-write-text-file

public enum BreakoutGameState { playing, won, lost };

public class BreakoutGame : MonoBehaviour
{
    public string playername;
    public static BreakoutGame SP;

    public Transform ballPrefab;

    private int totalBlocks;
    private int blocksHit;
    private BreakoutGameState gameState;


    void Awake()
    {
        SP = this;
        blocksHit = 0;
        gameState = BreakoutGameState.playing;
        totalBlocks = GameObject.FindGameObjectsWithTag("Pickup").Length;
        Time.timeScale = 1.0f;
        SpawnBall();
    }

    void SpawnBall()
    {
        Instantiate(ballPrefab, new Vector3(1.81f, 1.0f , 9.75f), Quaternion.identity);
    }

    void OnGUI(){

        GUILayout.Space(10);
        GUILayout.Label("  Hit: " + blocksHit + "/" + totalBlocks);
        playername = PlayerPrefs.GetString("playername");
        if (gameState == BreakoutGameState.lost)
        {
            GUILayout.Label("You Lost!");
            if (GUILayout.Button("Try again"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        else if (gameState == BreakoutGameState.won)
        {
            GUILayout.Label("You won!");
            if (GUILayout.Button("Play again"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    public void HitBlock()
    {
        blocksHit++;
        
        //For fun:
        if (blocksHit%10 == 0) //Every 10th block will spawn a new ball
        {
            SpawnBall();
        }

        
        if (blocksHit >= totalBlocks)
        {
            WonGame();
        }
    }

    public void WonGame()
    {
        Time.timeScale = 0.0f; //Pause game
        WriteTXT();
        gameState = BreakoutGameState.won;
    }

    public void LostBall()
    {
        int ballsLeft = GameObject.FindGameObjectsWithTag("Player").Length;
        if(ballsLeft<=1){
            //Was the last ball..
            SetGameOver();
        }
    }

    public void SetGameOver()
    {
        Time.timeScale = 0.0f; //Pause game
        WriteTXT();
        gameState = BreakoutGameState.lost;
    }

    public void WriteTXT()
    {
        try
        {
            StreamWriter sw;
            if (File.Exists("../Puntajes.txt"))
            {
                sw = File.AppendText("../Puntajes.txt");
            }
            else {
                sw = new StreamWriter("../Puntajes.txt");
                sw.WriteLine("Puntajes!");
            }
            sw.WriteLine(playername +"  Hit: " + blocksHit + "/" + totalBlocks);
            sw.Close();
        }
        finally
        {
            print("Written File!");
        }
    }
}
