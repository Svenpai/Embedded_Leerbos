﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ManagerScript : MonoBehaviour
{
    public GameObject WeightParent;
    public ScaleHandScript LeftHand;
    public ScaleHandScript RightHand;
    public List<GameObject> ObjectsToWeigh;

    private GameObject currentObject;
    private bool gameOver;
    private int objectsWeighed;

	// Use this for initialization
	void Start ()
	{
	    gameOver = false;
	    objectsWeighed = 0;
        ResetGame();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CheckAnswer()
    {
        if (gameOver == false)
        {
            int leftMass = LeftHand.GetTotalMass();
            int rightMass = RightHand.GetTotalMass();

            if (leftMass == rightMass && leftMass + rightMass > 0)
            {
                objectsWeighed++;

                if (objectsWeighed >= 5 || ObjectsToWeigh.Count == 0)
                {
                    gameOver = true;
                    print("A WINNER IS YOU");
                }
                else
                {
                    ResetGame();
                }
            }
        }
    }

    public void ResetGame()
    {
        //if not first rotation
        if (currentObject != null)
        {
            foreach (WeightedObjectScript weight in WeightParent.GetComponentsInChildren<WeightedObjectScript>())
            {
                weight.SelfDestruct();
            }
        }
        currentObject = ObjectsToWeigh[Random.Range(0, ObjectsToWeigh.Count)];
        ObjectsToWeigh.Remove(currentObject);
        currentObject = SpawnPrefab(currentObject,ScaleHand.Right);
        
    }

    //spawn the submitted prefab above one of the scales
    public GameObject SpawnPrefab(GameObject prefab,ScaleHand hand)
    {
        float x;
        switch (hand)
        {
            case ScaleHand.Left:
                x = GetHandX(LeftHand);
                break;
            case ScaleHand.Right:
                x = GetHandX(RightHand);
                break;
            default:
                print("how the heck did you get here with an invalid enum");
                throw new ArgumentException();
        }
        return Instantiate(prefab, new Vector2(x, 10),Quaternion.Euler(0,0,0),WeightParent.transform);
    }

    //get x position of the left scale
    float GetHandX(ScaleHandScript hand)
    {
        return hand.gameObject.transform.position.x;
    }
}

public enum ScaleHand
{
    Left,
    Right
}
