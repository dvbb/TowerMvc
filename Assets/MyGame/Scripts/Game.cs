using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;

public class Game : ApplicationBase<Game>
{
    // Global Access function
    public ObjectPollController ObjectPool;
    public MusicController MusicController;
    public StaticData StaticData;

    // Global Function
    public void LoadScene(int level)
    {
        // Exist current scene
        SceneArgs e = new SceneArgs();
        e.SceneIndex = SceneManager.GetActiveScene().buildIndex;
        //SendEvent(Consts.e,e);

        // Enter new scene
        SceneManager.LoadScene(level, LoadSceneMode.Single);
    }

    private void OnLevelWasLoaded(int level)
    {

    }

    // Game Init
    protected override void Awake()
    {
        base.Awake();

        ObjectPool = new ObjectPollController();
        MusicController = new MusicController();
        StaticData = new StaticData();

        DontDestroyOnLoad(this);

        // Establish mapping relationship
        RegisterController(Consts.E_StartUp,typeof(StartUpCommand));

        SendEvent(Consts.E_StartUp);
    }
}
