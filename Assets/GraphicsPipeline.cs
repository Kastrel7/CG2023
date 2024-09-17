using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsPipeline : MonoBehaviour
{
    Model myLetter;
    // Start is called before the first frame update
    void Start()
    {
        myLetter = new Model();
        myLetter.CreateUnityGameObject();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
