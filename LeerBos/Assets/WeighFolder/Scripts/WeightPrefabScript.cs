﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightPrefabScript : WeightedObjectScript {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected override void Click(Vector3 clickposition)
    {
        if (RemoveFromList())
        {
            Destroy(gameObject);
        }
    }
}
