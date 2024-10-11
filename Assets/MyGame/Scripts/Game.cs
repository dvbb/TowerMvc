using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;

public class Game : ApplicationBase<Game>
{
    // Global Access function
    public ObjectPollController ObjectPool;
    public MusicManager MusicController;
    public StaticData StaticData;

    // Global Function
    public void LoadScene(int level)
    {
        // Exist current scene
        SceneArgs e = new SceneArgs();
        e.SceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (e.SceneIndex == 3)
        {
            SendEvent(Consts.E_ExitLevel);
        }
        SendEvent(Consts.E_ExitScene, e);

        // Enter new scene
        SceneManager.LoadScene(level, LoadSceneMode.Single);
    }

    private void OnLevelWasLoaded(int level)
    {
        // Register view event for new scene
        SceneArgs e = new SceneArgs();
        e.SceneIndex = level;
        SendEvent(Consts.E_EnterScene, e);
    }

    // Game Init
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);

        // 1. Add new Components
        // 1.1 Basic components
        this.gameObject.AddComponent<ObjectPollController>();
        this.gameObject.AddComponent<MusicManager>();

        // 1.3 Card

        // 1.4 Data Persistency

        // Init components
        StaticData = new StaticData();
        ObjectPool = GetComponent<ObjectPollController>();
        MusicController = GetComponent<MusicManager>();
        MusicManager.Instance.PlayMusic(MusicEnum.MusicType_Main.Forever);

        // Init Static data
        StaticData.Instance.SetResolution();

        // Establish mapping relationship
        RegisterController(Consts.E_StartUp, typeof(StartUpCommand));

        SendEvent(Consts.E_StartUp);
    }
}
