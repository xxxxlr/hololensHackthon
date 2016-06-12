using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class SpeechManager : MonoBehaviour
{
    public GameObject itemPreFab;
    public GameObject userModeManager;

    private UserModeManager userModeManagerScript;

    private MarkedItems items = null;

    KeywordRecognizer keywordRecognizer = null;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    private string[] commands;


    // Use this for initialization
    void Start()
    {
        string[] outCommands = {"table", "laptop", "floor", "ceiling", "television",
         "person", "wall", "cup", "chair", "refrigerator",
         "switch", "clock","microwave", "soda",  "gaggle"
          };
        commands = outCommands;

        userModeManagerScript = userModeManager.GetComponent<UserModeManager>();


        keywords.Add("Reset world", () =>
        {
            // Call the OnReset method on every descendant object.
            this.BroadcastMessage("OnReset");

        });

        keywords.Add("Drop Sphere", () =>
        {
            var focusObject = GazeGestureManager.Instance.FocusedObject;
            if (focusObject != null)
            {
                // Call the OnDrop method on just the focused object.
                focusObject.SendMessage("OnDrop");
            }
        });

        keywords.Add("learn", () =>
        {
            userModeManagerScript.setUserMode("learn");
        });

        keywords.Add("teach", () =>
        {
            userModeManagerScript.setUserMode("teach");
        });

        GameObject markedItems = GameObject.Find("MarkedItems");

        foreach (string command in commands) {
            keywords.Add(command, () =>
            {
                // Do a raycast into the world based on the user's
                // head position and orientation.
                var headPosition = Camera.main.transform.position;
                var gazeDirection = Camera.main.transform.forward;

                RaycastHit hitInfo;
                if (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
                {
                    // 
                    var position = hitInfo.point;

                    //GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

                    //GUI drag
                    GameObject cube = Instantiate(itemPreFab);

                    cube.transform.position = position;
                    cube.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    var iteminfo = cube.GetComponent<ItemInfo>();
                    iteminfo.Title = command;
                    iteminfo.ItemName = command;
                    cube.name = command;

                    iteminfo.markedItems = markedItems;
                    //iteminfo.userModeManager = userModeManager;
                    iteminfo.userModeManagerScript = userModeManagerScript;

                    //as long as assing somethign has a type.
                    //var itemAudioSource = cube.GetComponent<AudioSource>();
                    //itemAudioSource.clip = Resources.Load<AudioClip>(command);
                    if(items == null) {
                        items = markedItems.GetComponent<MarkedItems>();
                    }
                    items.gameObjectList.Add(cube);


                }
            });
        };
        


        // Tell the KeywordRecognizer about our keywords.
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

        // Register a callback for the KeywordRecognizer and start recognizing!
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    void Update() {
    /*
        if(mode == "teach") {
            if(playing) {
                audioSource.Stop();
                playing = false;
            }
        } 
        if(mode == "learn") {
            if(!playing) {
                audioSource.Play();
                playing = true;
            }
        }
        */
    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }
}
