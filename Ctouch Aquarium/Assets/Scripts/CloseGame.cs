using UnityEngine;
using Persistence;
public class CloseGame : MonoBehaviour
{
    public FishPersistence p;
    public FishSpawner f;
    public void Close()
    {
        p.SaveFishes(f.GetFishObjects());
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}