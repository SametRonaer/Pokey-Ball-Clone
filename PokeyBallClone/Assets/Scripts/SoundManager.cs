using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource metal, wood, fly;
    bool rePlay = true;

    public void PlaySound(string soundType)
    {
        if (rePlay)
        {
            rePlay = false;
            if(soundType == "metal")
            {
                metal.Play();
                print("metal");
                Invoke("RePlay", 1);
            }else if(soundType == "wood")
            {
                wood.Play();
                print("wood");
                Invoke("RePlay", 1);
            }else if(soundType == "fly")
            {
                fly.Play();
                Invoke("RePlay", 1);
            }
        }
    }

    void RePlay()
    {
        rePlay = true;
    }
}
