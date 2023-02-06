using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameGUI : MonoBehaviour
{
    public HPBar hpBar;
    public CanvasGroup screenBlood;    

    // Start is called before the first frame update
    void Start()
    {
        screenBlood.alpha = 0;
        OnPlayerHeal(GlobalInfo.currentHealth / GlobalInfo.playerGenome.GetMaxHealth());
        Player.onDamage += OnPlayerHurt;
        Player.onHeal += OnPlayerHeal;
    }

    private void OnDestroy()
    {
        Player.onDamage -= OnPlayerHurt;
        Player.onHeal -= OnPlayerHeal;
    }

    void OnPlayerHurt(float percent)
    {
        hpBar.Set(GlobalInfo.currentHealth, GlobalInfo.playerGenome.GetMaxHealth());
        screenBlood.alpha = 1;
        Debug.Log("HIT");
    }

    void OnPlayerHeal(float percent)
    {
        hpBar.Set(GlobalInfo.currentHealth, GlobalInfo.playerGenome.GetMaxHealth());        
    }

    private void Update()
    {
        if(screenBlood.alpha > 0)
        {
            screenBlood.alpha -= Time.deltaTime * 2f;
        }
    }

    private void OnGUI()
    {
        GUILayout.Label("GameGUI.cs: " + GlobalInfo.currentHealth + " / " + GlobalInfo.playerGenome.GetMaxHealth());
    }
}
