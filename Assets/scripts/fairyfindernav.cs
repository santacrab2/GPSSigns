using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class fairyfindernav : MonoBehaviour
{
    public Button thisbutton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        thisbutton.onClick.AddListener(async ()=> { SceneManager.LoadScene(3);
            await SceneManager.UnloadSceneAsync(0);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
