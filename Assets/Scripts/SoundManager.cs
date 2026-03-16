using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.InputSystem;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private const string PlayerPrefsSoundEffectsVolume = "SoundEffectsVolume";
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioClipRefsSO audioClipRefsSO;

    private float volume = .1f;

    private void Awake()
    {
        Instance = this;
        volume  = PlayerPrefs.GetFloat(PlayerPrefsSoundEffectsVolume, 1f);
    }
    private void Start()

    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;  
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickedSomething += Player_OnPickedSomething;
        BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
    }

    private void TrashCounter_OnAnyObjectTrashed(object sender, EventArgs e)
    {
        PlaySound(audioClipRefsSO.trash, ((TrashCounter)sender).transform.position);
    }

    private void BaseCounter_OnAnyObjectPlacedHere(object sender, EventArgs e)
    {
        PlaySound(audioClipRefsSO.ObjectDrop, ((BaseCounter)sender).transform.position);
    }
    private void Player_OnPickedSomething(object sender, EventArgs e)
    {
        PlaySound(audioClipRefsSO.ObjectPickUp, Player.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, EventArgs e)
    {
        PlaySound(audioClipRefsSO.chop, ((CuttingCounter)sender).transform.position);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefsSO.deliveryFailed, deliveryCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefsSO.deliverySuccess, deliveryCounter.transform.position);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 postion, float volume = 1f)
    {
        PlaySound(audioClipArray[UnityEngine.Random.Range(0, audioClipArray.Length)], postion, volume);

    }
    private void PlaySound(AudioClip audioClip, Vector3 postion, float volumeMultiplier = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, postion, volumeMultiplier * volume);
    }

    public void PlayFootstepsSound(Vector3 postion, float volume)
    {
        PlaySound(audioClipRefsSO.footstep, postion, volume);
    }
    public void ChangeVolume()
    {
        volume += .1f;
        if(volume > 1f)
        {
            volume = 0f;
        }

        PlayerPrefs.SetFloat("SoundEffectsVolume", volume);
        PlayerPrefs.Save(); 
    }
    public float GetVolume()
    {
        return volume;
    }
}
