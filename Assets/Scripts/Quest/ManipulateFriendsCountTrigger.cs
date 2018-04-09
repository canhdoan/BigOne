﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ManipulateFriendsCountTrigger : MonoBehaviour {

    bool triggered;
    Level1Quest level1quest;
    public PlayableDirector playableDirector;

    private void Start()
    {
        level1quest = FindObjectOfType<Level1Quest>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && triggered == false)
        {
            FMOD.Studio.EventInstance fmodEvent = FMODUnity.RuntimeManager.CreateInstance(level1quest.soundEffect);
            fmodEvent.start();
            fmodEvent.release();
            triggered = true;
            playableDirector.Play();
            level1quest.friendsSaved++;
        }
    }
}
