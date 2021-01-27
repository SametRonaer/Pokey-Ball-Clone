using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    [SerializeField] GameObject getPointText;
    int pointCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

   public void SetPointText(string point)
    {
        getPointText.SetActive(true);
        getPointText.GetComponent<TextMeshPro>().text = "+" + point;
        pointCounter += int.Parse(point);
        print(pointCounter);
        Invoke("TextDisappear", 0.5f);
    }

    void TextDisappear()
    {
        getPointText.SetActive(false);
    }
}
