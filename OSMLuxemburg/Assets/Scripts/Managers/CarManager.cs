using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour {

    [SerializeField]
    private int carPopulationMin;

    [SerializeField]
    private int carPopulationMax;

    [SerializeField]
    [Range(0, 100)]
    private int carSpawnFrequency = 100;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
