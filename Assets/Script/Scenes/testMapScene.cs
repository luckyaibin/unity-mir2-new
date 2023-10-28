using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testMapScene : MonoBehaviour
{
    private MapController mapController;
    // Start is called before the first frame update
    void Awake(){
         MapConfigs.init();
    }
    void Start()
    {
        mapController = GetComponent<MapController>();
        mapController.setMapInfo(110,110,"0");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
