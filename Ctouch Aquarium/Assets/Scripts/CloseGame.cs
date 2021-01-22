using UnityEngine;
using Persistence;
public class CloseGame : MonoBehaviour
{
    public FishPersistence p;
    public FishSpawner f;
    public Score score;
    public void Close()
    {
        p.SaveFishes(f.GetFishObjects());
        score.SaveScore();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}