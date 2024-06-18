using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ParallaxLayer
{
    public Transform layer;  
    public float parallaxFactor;  
}

public class ParallaxEffect : MonoBehaviour
{
    public ParallaxLayer[] layers;  
    public float smoothing = 1f;

    private Transform cam; 
    private Vector3 previousCamPos;  

    void Awake()
    {
        
        cam = Camera.main.transform;
    }

    void Start()
    {
       
        previousCamPos = cam.position;
    }

    void Update()
    {
        
        foreach (ParallaxLayer layer in layers)
        {
            
            float parallax = (previousCamPos.x - cam.position.x) * layer.parallaxFactor;

           
            float backgroundTargetPosX = layer.layer.position.x + parallax;

           
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, layer.layer.position.y, layer.layer.position.z);

           
            layer.layer.position = Vector3.Lerp(layer.layer.position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        previousCamPos = cam.position;
    }
}