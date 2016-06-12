using UnityEngine;
using System.Collections;
using HoloToolkit.Unity;

public class ItemInfo : MonoBehaviour {

    public float createdTime;
    private float timeSincePlayed;
    public GameObject markedItems;
    public GameObject userModeManager;

    public UserModeManager userModeManagerScript;
    public TextToSpeechManager ttsManager;
    public SoundPlayer soundPlayer;

    private MarkedItems markedItemsScript;
    private AudioSource itemAudioSource;
    private bool playing = false;
    private bool played = false;

    // Use this for initialization
    void Start () {
        createdTime = Time.time;
        markedItems = GameObject.Find("MarkedItems");
        userModeManager = GameObject.Find("UserModeManager");
        markedItemsScript = markedItems.GetComponent<MarkedItems>();
        userModeManagerScript = FindObjectOfType<UserModeManager>();

        ttsManager = FindObjectOfType<TextToSpeechManager>();
        soundPlayer = FindObjectOfType<SoundPlayer>();

        itemAudioSource = gameObject.GetComponent<AudioSource>();
        // itemAudioSource.clip = Resources.Load<AudioClip>("table");
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
                    pronounceItem();
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
        timeSincePlayed += Time.deltaTime;

    }
    public void pronounceItem()
    {
        ttsManager.audioSource.loop = true;
        if (timeSincePlayed < 2f)
        {
            return;
        }

        if (played)
        {
            ttsManager.audioSource.Play();
        }
        else
        {
            played = true;
            if (string.IsNullOrEmpty(this.ItemName))
            {
                ttsManager.SpeakText("Unknown");
            }
            else
            {
                ttsManager.SpeakText(this.ItemName);
            }
        }
        timeSincePlayed = 0;
    }

    public string Title { get; set; }
    public string ItemName { get; set; }
}
