using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    public float lightTimeToDisappear,delayLightOff;
    public Material lightMaterial;
    GameObject [] Obstacles;
    GameObject spotLight;
    Color tempColor;
    float delay;
    bool isLightOn;
    
    void Start()
    {
        Obstacles = GameObject.FindGameObjectsWithTag("Obstacles");
        spotLight = GameObject.FindGameObjectWithTag("Light");
        spotLight.SetActive(false);
        isLightOn = false;
        tempColor.a = 0;
        tempColor = lightMaterial.color;
        for(int i=0;i<Obstacles.Length;i++)
        {
            Obstacles[i].GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void Update()
    {
        if(Player.isAlive)
        {
            if(delay > 0)
                delay -= Time.deltaTime;
            if(isLightOn && delay <= 0)
            {
                delay = 0;
                TurnLightOff();
            }
            else if (!isLightOn)
            {
                TurnLightOn();
            }
        }
        else
        {
            TurnLightOff();
        }
    }
    void TurnLightOn()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            TurnLightOff();
            spotLight.SetActive(true);
            tempColor = lightMaterial.color;
            tempColor.a = 1;
            lightMaterial.color = tempColor;
            lightMaterial.color = Color.red;
            delay = delayLightOff;
            isLightOn = true;
        }
        else if(Input.GetKeyDown(KeyCode.Q))
        {
            spotLight.SetActive(true);
            tempColor = lightMaterial.color;
            tempColor.a = 1;
            lightMaterial.color = tempColor;
            lightMaterial.color = Color.blue;
            delay = delayLightOff;
            isLightOn = true;
        }
    }
    void TurnLightOff()
    {
        tempColor = lightMaterial.color;
        tempColor.a -=Time.deltaTime;
        lightMaterial.color = tempColor;
        if(lightMaterial.color.a <= 0.5f)
            spotLight.SetActive(false);
        if(lightMaterial.color.a <= 0)
        {
            tempColor.a = 0;
            lightMaterial.color = tempColor;
            isLightOn = false;
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Obstacles")
        {
            if(lightMaterial.color == tempColor && tempColor.b == 0)
            {
                other.gameObject.GetComponent<Renderer>().enabled = true;
            }
            else
            {
                other.gameObject.GetComponent<Renderer>().enabled = false;
            }
        }
        if(other.gameObject.tag == "Collectables")
        {
            if(lightMaterial.color == tempColor && tempColor.r == 0)
            {
                other.gameObject.GetComponent<Renderer>().enabled = true;
            }
            else
            {
                other.gameObject.GetComponent<Renderer>().enabled = false;
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Obstacles")
        {
            other.gameObject.GetComponent<Renderer>().enabled = false;
        }
        if(other.gameObject.tag == "Collectables")
        {
            other.gameObject.GetComponent<Renderer>().enabled = false;
        }
    }
}
                //Obstacles[i].GetComponent<Renderer>().enabled = true;
