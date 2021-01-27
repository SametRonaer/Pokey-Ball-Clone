using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetecPointType : MonoBehaviour
{
    Ray ray;
    string rayHitTag;
    PointManager pointManager;
    private void Start()
    {
        //DrawRay();
        pointManager = GetComponent<PointManager>();
    }

    public bool DrawRay(Transform sphere)
    {
        Vector3 dir = -Vector3.forward*100;
        Debug.DrawRay(sphere.position, dir, Color.blue, 10, false);
        ray = new Ray(sphere.position, dir);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        rayHitTag = hit.collider.gameObject.tag;
        print(rayHitTag);
        return CheckSurface(rayHitTag);
    }

    bool CheckSurface(string rayHitTag)
    {
        if(rayHitTag == "Stone")
        {
            print("Stop");
            return false;
        }
        else if(rayHitTag == "Wall")
        {
            print("Contiune");

        }
        else
        {
            pointManager.SetPointText(rayHitTag);
        }

        return true;
    }


  


   
}
