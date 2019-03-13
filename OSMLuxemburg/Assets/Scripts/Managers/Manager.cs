using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

    [SerializeField]
    private CarManager carManager;
    private DataImporter dataImporter;
    

    // Use this for initialization
    void Start () {
        dataImporter = new DataImporter();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
