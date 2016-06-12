using UnityEngine;
using System.Collections;

public class SoundPlayer : MonoBehaviour {

    public SoundPlayer Instance;

    private float timeSincePlayed = 0.0f;

    public UserModeManager userModeManagerScript;
    private MarkedItems markedItemsScript;
    private bool playing = false;



    public void Awake()
    {
        Instance = this;
    }

    public void Start() {
        markedItemsScript = FindObjectOfType<MarkedItems>();
        userModeManagerScript = FindObjectOfType<UserModeManager>();
    }

    public void PlaySound(string soundType)
    {
        if (timeSincePlayed < 2.0f)
        {

            return;
        }

        // Do play sound here...
        if(soundType == "item") {
            //Debug.Log(userModeManagerScript.isLearnMode());

            if (userModeManagerScript.isLearnMode())
            {
                if (markedItemsScript.isCurrentLearningItem(gameObject))
                {
                    if (!playing)
                    {
                        pronounceItem();
                        playing = true;
                    }
                }
            }
            else
            {
                //if (playing)
                //{
                    //itemAudioSource.Stop();
                    playing = false;
                //}
            }
            timeSincePlayed += Time.deltaTime;
        }

        timeSincePlayed = 0.0f;
    }

    public void Update()
    {
        timeSincePlayed += Time.deltaTime;
    }


    public void pronounceItem()
    {
        //if (timeSincePlayed < 2f)
        //{
        //    return;
        //}

        //if (played)
        //{
        //    ttsManager.audioSource.Play();
        //}
        //else
        //{
        //    played = true;
        //    if (string.IsNullOrEmpty(this.ItemName))
        //    {
        //        ttsManager.SpeakText("Unknown");
        //    }
        //    else
        //    {
        //        ttsManager.SpeakText(this.ItemName);
        //    }
        //}
        //timeSincePlayed = 0;
    }
}

