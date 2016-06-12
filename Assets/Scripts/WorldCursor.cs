using UnityEngine;

public class WorldCursor : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    public GameObject markedItemObject;

    public GameObject markedItems;
    private MarkedItems markedItemsScript;
    public GameObject soundPlayer;
    private SoundPlayer soundPlayerScript;

    public GameObject userModeManager;
    private UserModeManager userModeManagerScript;

    private float timeSincePlayed;

    // Use this for initialization
    void Start()
    {
        // Grab the mesh renderer that's on the same object as this script.
        meshRenderer = this.gameObject.GetComponentInChildren<MeshRenderer>();
        markedItems = GameObject.Find("MarkedItems");
        soundPlayer = GameObject.Find("SoundPlayer");
        userModeManager = GameObject.Find("UserModeManager");
        userModeManagerScript = userModeManager.GetComponent<UserModeManager>();
        soundPlayerScript = soundPlayer.GetComponent<SoundPlayer>();
        markedItemsScript = markedItems.GetComponent<MarkedItems>();
    }

    // Update is called once per frame
    void Update()
    {
        
        // Do a raycast into the world based on the user's
        // head position and orientation.
        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        RaycastHit hitInfo;
        if (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
        {
            // If the raycast hit a hologram...

            // Display the cursor mesh.
            meshRenderer.enabled = true;
            // Move the cursor to the point where the raycast hit.
            this.transform.position = hitInfo.point;
            // Rotate the cursor to hug the surface of the hologram.
            this.transform.rotation =
                Quaternion.FromToRotation(Vector3.up, hitInfo.normal);

            if(userModeManagerScript.isLearnMode()) {
                GameObject collidedItem = hitInfo.collider.gameObject;
                if (markedItemsScript.isCurrentLearningItem(collidedItem))
                {
                    RemoveMarker(collidedItem);
                }
                else if (markedItemsScript.isInLearningItems(collidedItem))
                {
                    playAlert();
                }
            }
            
        }
        else
        {
            // If the raycast did not hit a hologram, hide the cursor mesh.
            meshRenderer.enabled = false;
        }
        timeSincePlayed += Time.deltaTime;
    }

    void RemoveMarker(GameObject marker) {
        MarkedItems markedItems = markedItemObject.GetComponent<MarkedItems>();
        if(markedItems.gameObjectList.Contains(marker)
            && marker.GetComponent<ItemInfo>().createdTime < Time.time - 2) {
            markedItems.gameObjectList.Remove(marker);
            Destroy(marker);
        }
    }

    void playAlert() {
        if (timeSincePlayed < 2.0f)
        {
            return;
        }
        timeSincePlayed = 0.0f;
        //play alert sound
        //NOTE:has to have an audioSource component!!!
        AudioSource audio = soundPlayerScript.GetComponent<AudioSource>();
        audio.clip = Resources.Load<AudioClip>("alert");
        audio.Play();
    }
}
