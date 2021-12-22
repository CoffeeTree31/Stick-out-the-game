using UnityEngine;
using UnityEngine.SceneManagement;

public class play_script: MonoBehaviour
{
    public void StartLevel()
    {
        SceneManager.LoadScene("Play");
    }
}