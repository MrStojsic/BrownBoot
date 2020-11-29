using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Npcs;
using UnityEngine.UI;

public class TownMapPoiManager : MonoBehaviour
{
    [SerializeField] private RectTransform mapImageRect = null;

    [SerializeField] List<Npc> npcs = new List<Npc>();
    [SerializeField] List<RectTransform> iconButtonsRects = new List<RectTransform>();
    // Start is called before the first frame update
    void Start()
    {
        SetNpcPositions();
    }

    // Update is called once per frame
    void SetNpcPositions()
    {
        print(mapImageRect.rect.width * npcs[0].PositionPercentage.x + " " + -mapImageRect.rect.height * npcs[0].PositionPercentage.y);
        for (int i = 0; i < npcs.Count; i++)
        {
            iconButtonsRects[i].anchoredPosition = new Vector2(mapImageRect.rect.width * npcs[i].PositionPercentage.x, -mapImageRect.rect.height * npcs[i].PositionPercentage.y);
           // iconButtons[i].transform.localPosition = mapImage.rectTransform.rect.size * npcs[i].PositionPercentage;
        }
    }
}
