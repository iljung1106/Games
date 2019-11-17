using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animEventor : MonoBehaviour
{
    public CharacterController character;
    public AudioSource slashAudio;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Shot()
    {
        character.Fire();
    }
    public void SlashSound()
    {
        slashAudio.Play();
        Debug.Log("SLash");
    }
}
