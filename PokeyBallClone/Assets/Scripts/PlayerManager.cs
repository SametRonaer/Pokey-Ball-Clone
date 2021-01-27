using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    float forceAmount;
    float forceLimit;
    float normalizedForce;
    float animationSpeed;
    Vector3 initialSphereLocalPosition;
    [SerializeField] float maxSpeed = 2000;

    bool sphereAnim;
    bool sphereFree;
    bool stopSphere;
    bool openTrail = true;

    Touch touch;
    [SerializeField] GameObject stickParent;
    [SerializeField] GameObject stick;
    [SerializeField] GameObject sphere;
    [SerializeField] GameObject blackHole;
    Animator stickAnim;
    Rigidbody rb;
    Transform sphereParent;
    DetecPointType detecPointType;
    SoundManager soundManager;

    enum soundType
    {
        metal,
        wood,
        fly
    }

    // Start is called before the first frame update
    void Start()
    {
        forceAmount = 0;
        forceLimit = -200;
        stickAnim = stickParent.GetComponent<Animator>();
        rb = sphere.GetComponent<Rigidbody>();
        initialSphereLocalPosition = sphere.transform.localPosition;
        detecPointType = GetComponent<DetecPointType>();
        soundManager = GetComponent<SoundManager>();
    }



    // Update is called once per frame
    void Update()
    {

     
        GetForce();
        AnimateStick();
  
    }

    private void GetNextPosition()
    {
            rb.velocity = new Vector3(0, 0, 0);
            rb.useGravity = false;  
    }

    private void AnimateSphere()
    {
        if (!sphereAnim)
        {
            stickParent.transform.position += new Vector3(0,0.15f, 0);
            sphereAnim = true;
        }
        else
        {
            stickParent.transform.position-= new Vector3(0, 0.15f, 0);
            sphereAnim = false;
        }
    }

    private void AnimateStick()
    {
        stickAnim.SetFloat("direction", animationSpeed);
    }

    private void GetForce()
    {
        if (!sphereFree)
        {
            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);
                GetForceAmount();
                if (touch.phase == TouchPhase.Ended)
                {
                    SetSphereFree();
                    AddForce();
                    TrailEffect();
                    PlaySound(soundType.fly.ToString());
                    sphereParent = sphere.transform.parent.transform;
                    sphere.transform.parent = null;
                    stickParent.transform.parent = sphere.transform;
                    stopSphere = true;
                    AddBlackHole();
                }
            }
        }
        else if (stopSphere)
        {
            StopForce();
           
        }
    }

    private void TrailEffect()
    {
        if (openTrail)
        {
            sphere.transform.GetChild(0).gameObject.SetActive(true);
            openTrail = false;
        }
        else
        {
            sphere.transform.GetChild(0).gameObject.SetActive(false);
            openTrail = true;
        }
    }

    private void AddBlackHole()
    {
        GameObject oldHole = Instantiate(blackHole, blackHole.transform.position, blackHole.transform.rotation);
        oldHole.transform.localScale = new Vector3(0.1831896f, 0.1953195f, 0.1777989f);
        blackHole.SetActive(false);
    }

    private void GetForceAmount()
    {
        if (touch.phase == TouchPhase.Moved)
        {
            if (touch.deltaPosition.y < 0)
            {
                if (forceAmount > forceLimit)
                {
                    forceAmount += touch.deltaPosition.y;
                    normalizedForce = forceAmount / forceLimit;
                    animationSpeed = normalizedForce;
                }
               // print(normalizedForce);
            }
            if (touch.deltaPosition.y > 0)
            {
                if (forceAmount < -1)
                {
                    forceAmount += touch.deltaPosition.y;
                    normalizedForce = forceAmount / forceLimit;
                    animationSpeed = -normalizedForce;
                }
                //print(normalizedForce);
            }
        }
    }

    private void StopForce()
    {
        if (Input.touchCount > 0)
        {
            if (DetectSurfaceType()){
                GetNextPosition();
                PlaySound(soundType.wood.ToString());
                Invoke("SetSphereFree", 0.2f);
                stick.SetActive(true);
                blackHole.SetActive(true);
                stopSphere = false;
                stickParent.transform.parent = null;
                sphere.transform.parent = sphereParent;
                FixStickPosition();
                TrailEffect();
            }
            else
            {
                FallDown();
                PlaySound(soundType.metal.ToString());
            }
        }
    }

    void PlaySound(string soundType)
    {
        soundManager.PlaySound(soundType);
    }

    private void FallDown()
    {
        rb.velocity = new Vector3(0, 0, 0);
        rb.useGravity = true;
    }

    private bool DetectSurfaceType()
    {
        return detecPointType.DrawRay(sphere.transform);
    }

    private void FixStickPosition()
    {
       
        sphere.transform.localPosition = initialSphereLocalPosition;
        stickParent.transform.position -= new Vector3(0, 1f, 0);
    }

    void SetSphereFree()
    {
        if(sphereFree == false)
        {
            sphereFree = true;
        }
        else
        {
            sphereFree = false;
        }
    }

    void AddForce()
    {
        if(normalizedForce > 0.01f)
        {
            rb.AddForce(Vector3.up * maxSpeed*normalizedForce);
            rb.useGravity = true;
            forceAmount = 0;
            normalizedForce = 0;
            stick.SetActive(false);
            animationSpeed = 0;
            stickAnim.SetTrigger("restart");

        }
    }
}
