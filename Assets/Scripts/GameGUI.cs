using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameGUI : MonoBehaviour
{
    public CanvasGroup screenBlood;    

    // Start is called before the first frame update
    void Start()
    {
        OnPlayerHurt(GlobalInfo.currentHealth / GlobalInfo.playerGenome.GetMaxHealth());
        Player.onDamage += OnPlayerHurt;
    }

    private void OnDestroy()
    {
        Player.onDamage -= OnPlayerHurt;
    }

    void OnPlayerHurt(float percent)
    {
        screenBlood.alpha = 1 - percent;
    }

    private void OnGUI()
    {
        GUILayout.Label("GameGUI.cs: " + GlobalInfo.currentHealth + " / " + GlobalInfo.playerGenome.GetMaxHealth());
    }
}
