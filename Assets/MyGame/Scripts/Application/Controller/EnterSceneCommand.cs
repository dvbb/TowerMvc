﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class EnterSceneCommand : Controller
{
    public override void Execute(object data)
    {
        SceneArgs e = (SceneArgs)data;

        // Register view
        switch (e.SceneIndex)
        {
            case 0: // init
                break;
            case 1: // Title
                break;
            case 2: // Map
                //GameObject.Find("Cancas").transform.Find("UIWin").GetComponent<UIMap>();
                //GameObject.Find("Cancas").transform.Find("UILost").GetComponent<UIMap>();
                //RegisterView(GameObject.Find("Canvas").transform.Find("UIEscWindow").GetComponent<UIEscWindow>());
                break;
            case 3: // Level
                RegisterView(GameObject.Find("UILevel").GetComponent<UILevel>());
                RegisterView(GameObject.Find("Canvas").transform.Find("UICardTable").GetComponent<UICardTable>());
                RegisterView(GameObject.Find("Canvas").transform.Find("UICardShower").GetComponent<UICardShower>());
                RegisterView(GameObject.Find("Canvas").transform.Find("UIEscWindow").GetComponent<UIEscWindow>());
                RegisterView(GameObject.Find("Canvas").transform.Find("UIInfoWindow").GetComponent<UIInfoWindow>());
                GameObject.Find("UICardShower").SetActive(false);
                break;
            case 4:
                break;
            default:
                break;
        }
    }
}