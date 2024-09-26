using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class GameSceneTest : MonoBehaviour
{
    private void Update()
    {
        // play main bgm
        if (Input.GetKeyDown(KeyCode.P))
        {
            MusicController.Instance.PlayMusicByName(MusicEnum.MusicType_Main.MainBgm);
        }
    }
}
