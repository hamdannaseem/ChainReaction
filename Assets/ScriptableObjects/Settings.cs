using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "Scriptable Objects/Settings")]
public class Settings : ScriptableObject
{
    public int SpawnLimit, ReactionTime, SpawnSize, MusicVolume, SFXVolume, MasterVolume;
}
