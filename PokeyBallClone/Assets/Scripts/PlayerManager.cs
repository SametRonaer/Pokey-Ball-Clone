using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    float forceAmount;
    float forceLimit;
    float normalizedForce;
    Touch touch;
    [SerializeField] GameObject stick;
    [SerializeField] GameObject sphere;
    Animator stickAnim;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        forceAmount = 0;
        forceLimit = -200;
        stickAnim = stick.GetComponent<Animator>();
        rb = sphere.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        GetForce();
        AnimateStick();
    }

    private void AnimateStick()
    {
        stickAnim.SetFloat("direction", normalizedForce);
    }

    private void GetForce()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                if (touch.deltaPosition.y < 0)
                {
                    if (forceAmount > forceLimit)
                    {
                        forceAmount += touch.deltaPosition.y;
                        normalizedForce = forceAmount / forceLimit;
                    }
                    print(normalizedForce);
                }
                if (touch.deltaPosition.y > 0)
                {
                    if (forceAmount < -1)
                    {
                        forceAmount += touch.deltaPosition.y;
                        normalizedForce = -(forceAmount / forceLimit);
                    }
                    print(normalizedForce);
                }
            }
            if (touch.phase == TouchPhase.Ended)
            {
                forceAmount = 0;
                normalizedForce = 0;
                AddForce();
            }
        }
    }

    void AddForce()
    {
        rb.AddForce(Vector3.up * 1000);
        rb.useGravity = true;
    }
}
