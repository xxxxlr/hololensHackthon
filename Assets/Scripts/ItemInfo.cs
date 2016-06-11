using UnityEngine;
using System.Collections;

public class ItemInfo : MonoBehaviour {

    public float createdTime;
    public GameObject markedItems;
    public GameObject userModeManager;

    public UserModeManager userModeManagerScript;


    private MarkedItems markedItemsScript;
    private AudioSource itemAudioSource;
    private bool playing;

    // Use this for initialization
    void Start () {
        createdTime = Time.time;
        markedItems = GameObject.Find("MarkedItems");
        userModeManager = GameObject.Find("UserModeManager");
        markedItemsScript = markedItems.GetComponent<MarkedItems>();
        userModeManagerScript = userModeManager.GetComponent<UserModeManager>();


        itemAudioSource = gameObject.GetComponent<AudioSource>();
        itemAudioSource.clip = Resources.Load<AudioClip>("table");
    }
	
	// Update is called once per frame
	void Update () {

        //Debug.Log(userModeManagerScript.isLearnMode());

        if(userModeManagerScript.isLearnMode()) 
        {
            if (markedItemsScript.isCurrentLearningItem(gameObject))
            {
                if (!playing)
                {
                    itemAudioSource.Play();
                    playing = true;
                }
            }
        } else {
            if (playing)
            {
                itemAudioSource.Stop();
                playing = false;
            }
        }
       


    }


}
