using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetecPointType : MonoBehaviour
{
    Ray ray;
    private void Start()
    {
        
    }

    public int GetPointType(GameObject sphere)
    {
        ray = new Ray(sphere.transform.position, new Vector3(0, 0, 1));
      
       // Physics.Raycast()
        return (int)Vector3.Magnitude(sphere.transform.position);
    }


   
}
