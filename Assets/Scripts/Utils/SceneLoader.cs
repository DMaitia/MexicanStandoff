using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utils
{
    public class SceneLoader : MonoBehaviour
    {
        public void Load(String sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void LoadNewMatch()
        {
            SceneManager.LoadScene("MexicanStandoff");
        }
    }
}
