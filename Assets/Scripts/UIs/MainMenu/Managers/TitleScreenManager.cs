using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BladesOfDeceptionCapstoneProject
{
    public class TitleScreenManager : MonoBehaviour
    {
        public void StartNewGame()
        {
            StartCoroutine(WorldSaveGameManager.instance.LoadNewGame());
        }
    }
}
