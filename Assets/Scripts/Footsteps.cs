﻿using Player;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Footsteps : MonoBehaviour
{
    [SerializeField] private AudioClip[] footstepClips;


    [HideInInspector] public float volumeMin, volumeMax;

    [HideInInspector] public float stepDistance;

    private float _accumulatedDistance;

    private AudioSource _footstepSound;

    private void Awake()
    {
        _footstepSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        CheckFootstepSound();
    }

    private void CheckFootstepSound()
    {
        if (!PlayerController.IsGrounded)
            return;

        if (PlayerController.MovementState == PlayerController.MovementStates.Walking
            || PlayerController.MovementState == PlayerController.MovementStates.Running
            || PlayerController.MovementState == PlayerController.MovementStates.Crouching)
        {
            _accumulatedDistance += Time.deltaTime;

            if (!(_accumulatedDistance > stepDistance)) return;

            _footstepSound.volume = Random.Range(volumeMin, volumeMax);
            _footstepSound.clip = footstepClips[Random.Range(0, footstepClips.Length)];
            _footstepSound.Play();

            _accumulatedDistance = 0;
        }
        else
        {
            _accumulatedDistance = 0;
        }
    }
}