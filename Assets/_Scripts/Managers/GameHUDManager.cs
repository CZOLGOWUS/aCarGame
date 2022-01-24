using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

using CarGame.Managers;

namespace CarGame
{
    public class GameHUDManager : Singleton<GameHUDManager>
    {

        [SerializeField] private GameObject heartPrefab;
        [SerializeField] private TextMeshProUGUI distance;
        [SerializeField] private RectTransform HPPanel;

        private List<GameObject> heartsDisplaing = new List<GameObject>();

        public int UpdateDistance(int distanceToAdd)
        {

            int.TryParse(distance.text, out int res);
            distance.text = (distanceToAdd + res).ToString();

            return (distanceToAdd + res);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="hpModification">integer, hp can be a value from 0 to 10</param>
        public void UpdateHP(int hpModification)
        {
            if (hpModification == 0)
                return;

            if(hpModification > 0)
            {
                for (int i = 0; i < hpModification; i++)
                {
                    heartsDisplaing.Add(Instantiate(heartPrefab, HPPanel));
                }
            }
            else
            {
                for (int i = 0; i < -hpModification; i++)
                {
                    Destroy(heartsDisplaing[i]);
                    heartsDisplaing.Remove(heartsDisplaing[0]);
                }

            }

            Debug.LogWarning(heartsDisplaing.Count);
        }
    }
}
