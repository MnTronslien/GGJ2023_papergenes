using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TMPro.TextMeshProUGUI))]
public class SpookyFont : MonoBehaviour
{
    private TMPro.TextMeshProUGUI _t;
    private string text;
    private bool doneReveal;
    private float pause;

    public float revealSpeed;
    public float spawnDuration;

    public TMPro.TMP_FontAsset spawnFont;
    public TMPro.TMP_FontAsset flickerFont;

    void Awake()
    {
        _t = GetComponent<TMPro.TextMeshProUGUI>();
        text = _t.text;
        _t.text = "";

        StartCoroutine(Show());
    }

    IEnumerator Show()
    {
        doneReveal = false;
        pause = text.Length / revealSpeed / 100;



        int i = 0;
        while (i < text.Length)
        {
            string current = _t.text;

            if(spawnFont != null)
            {
                _t.text = current + "<font="+ spawnFont.name + ">" + text[i] + "</font>";

                float s = spawnDuration - Time.deltaTime;
                while (s > 0)
                {
                    s -= Time.deltaTime;
                    yield return null;
                }
            }
            
            _t.text = current + text[i];
            i++;

            float t = pause - Time.deltaTime;
            while (t > 0)
            {
                t -= Time.deltaTime;
                yield return null;
            }
        }

        doneReveal = true;
    }

    private void Update()
    {
        if(doneReveal && flickerFont != null)
        {
            int r = Random.Range(0, text.Length);

            string start = "";
            if(r > 0)
                start = text.Substring(0, r);

            string glitch = "<font=" + flickerFont.name + ">" + text[r] + "</font>";

            string end = "";
            if (r < text.Length)
                end = text.Substring(r+1, text.Length - r - 1);

            _t.text = start + glitch + end;
        }
    }
}
