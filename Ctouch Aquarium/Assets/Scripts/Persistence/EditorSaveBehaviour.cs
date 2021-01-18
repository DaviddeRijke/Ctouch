using FishDataFolder;
using UnityEngine;

namespace Persistence
{
    [ExecuteInEditMode]
    public class EditorSaveBehaviour : MonoBehaviour
    {
        [SerializeField]
        private FishPersistence Persistence;
        
        public void SaveAllInScene()
        {
            Debug.Log("Saving all fish in scene");
            var fishInScene = FindObjectsOfType<Fish>();
            Persistence.SaveUnsavedFishInScene(fishInScene);
        }

        public void Reset()
        {
            if (Persistence == null) return;
            Persistence.ResetOwnedFish();
        }

        public void ResetSaveFile()
        {
            Persistence.ResetSaveFile();
        }
    }
}