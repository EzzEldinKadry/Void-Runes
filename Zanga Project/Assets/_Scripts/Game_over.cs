using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game_over : MonoBehaviour
{

    public GameObject Game_over_text;
    private static float time = 2f;
    public static void SceneReload()
    {
        while(Game_over.time<=0)
        {
            print("Timer");
            Game_over.time -= Time.deltaTime;
            if(Game_over.time<=0)
            {
                SceneManager.LoadScene(1);
            }
        }
    }
}
