using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public Sprite[] heartSprites;

    private RectTransform rect;
    private List<Image> renderers;

    public void Set(float current, float max)
    {
        if (rect == null)
            rect = GetComponent<RectTransform>();

        if (renderers == null)
            renderers = new List<Image>();

        int count = Mathf.CeilToInt(max / heartSprites.Length);
        int nearest = Mathf.CeilToInt(current / heartSprites.Length);
        var s = rect.sizeDelta;
        s.x = count * s.y;
        rect.sizeDelta = s;

        for (int i = 0; i < Mathf.Max(count, renderers.Count); i++)
        {
            int spriteIndex = 0;
            if (i == nearest)
                spriteIndex = Mathf.RoundToInt(current - ((i-1) * heartSprites.Length));
            if (i < nearest)
                spriteIndex = heartSprites.Length - 1;
            spriteIndex = Mathf.Clamp(spriteIndex, 0, heartSprites.Length-1);

            if (i >= count)
                renderers[i].gameObject.SetActive(false);
            else if (i >= renderers.Count)
            {
                GameObject go = new GameObject();
                go.transform.parent = transform;
                Image sde = go.AddComponent<Image>();
                sde.sprite = heartSprites[spriteIndex];
                renderers.Add(sde);
            }
            else
            {
                renderers[i].sprite = heartSprites[spriteIndex];
                renderers[i].gameObject.SetActive(true);
            }
        }
    }
}
