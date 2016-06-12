using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MarkedItems : MonoBehaviour {

    public List<GameObject> gameObjectList = new List<GameObject>();

	// Use this for initialization
	void Start () {


    }

    // Update is called once per frame
    void Update () {
	
	}

    public bool isCurrentLearningItem(GameObject targetItem) {
        if(gameObjectList.Count > 0){
            return gameObjectList[gameObjectList.Count - 1] == targetItem;
        } else {
            return false;
        }
    }

    public bool isInLearningItems(GameObject targetItem) {
        return gameObjectList.Contains(targetItem);
    }
}
