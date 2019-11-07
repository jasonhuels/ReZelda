using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class School3DoorOpen : MonoBehaviour
{
    public AudioClip sfxOpen;
    public AudioSource openSource;
    // Start is called before the first frame update
    void Start()
    {
        openSource = gameObject.GetComponent<AudioSource>();
        openSource.clip = sfxOpen;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("School3_Door_1") == null && GameObject.Find("School3_Door_2") == null && GameObject.Find("School3_Door_Null_else") == null && GameObject.Find("School3_Door_Null_if") == null)
        {
            openSource.Play();
            Destroy(this);
        }
    }
}
