using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    public void SoundPunch()
    {
        SoundManager.instance.SoundPunch();
    }

    public void SoundDoor()
    {
        SoundManager.instance.SoundDoor();
    }

    public void SoundPistol()
    {
        SoundManager.instance.SoundPistol();
    }
}
