using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.IO;
using System.Net.Http;
using System;
using System.Net;
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections.ObjectModel;

// La escritura de punatajes fue obtenida de este link:https://docs.microsoft.com/en-us/troubleshoot/dotnet/csharp/read-write-text-file

public enum BreakoutGameState { playing, won, lost };

public class BreakoutGame : MonoBehaviour
{
    public string playername;
    public static string username;
    public static BreakoutGame SP;

    public Transform ballPrefab;

    private int totalBlocks;
    private int blocksHit;

    public static int stotalblocks;
    public static int sblockshit;

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
        Instantiate(ballPrefab, new Vector3(1.81f, 1.0f, 9.75f), Quaternion.identity);
    }

    void OnGUI()
    {

        GUILayout.Space(10);
        GUILayout.Label("  Hit: " + blocksHit + "/" + totalBlocks);
        playername = PlayerPrefs.GetString("playername");
        username = playername;


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
        if (blocksHit % 10 == 0) //Every 10th block will spawn a new ball
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
        sblockshit = blocksHit;
        stotalblocks = totalBlocks;
        PostAsyncLink("http://localhost:3000/scores", username, sblockshit, stotalblocks);
        WriteTXT();
        gameState = BreakoutGameState.won;
    }

    public void LostBall()
    {
        int ballsLeft = GameObject.FindGameObjectsWithTag("Player").Length;
        if (ballsLeft <= 1)
        {
            //Was the last ball..
            SetGameOver();
        }
    }

    public void SetGameOver()
    {
        Time.timeScale = 0.0f; //Pause game
        sblockshit = blocksHit;
        stotalblocks = totalBlocks;
        PostAsyncLink("http://localhost:3000/scores", username, sblockshit, stotalblocks);

        WriteTXT();
        gameState = BreakoutGameState.lost;
    }

    public static async void PostAsyncLink(string link, string username, int blockshit, int totalblocks)
    {
        var client = new HttpClient();
        //string puntaje = $"{blockshit}/{totalblocks}";
        print(blockshit.ToString());

        Dictionary<string, string> values = new Dictionary<string, string>();

        values.Add("player", username);
        values.Add("score", blockshit.ToString());

        var content = new FormUrlEncodedContent(values);

        var response = await client.PostAsync(link, content);
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
            else
            {
                sw = new StreamWriter("../Puntajes.txt");
                sw.WriteLine("Puntajes!");
            }
            sw.WriteLine(playername + "  Hit: " + blocksHit + "/" + totalBlocks);
            sw.Close();
        }
        finally
        {
            print("Written File!");
        }
    }
}
