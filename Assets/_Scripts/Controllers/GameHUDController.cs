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
    public class GameHUDController
    {

        private GameObject heartPrefab;
        private TextMeshProUGUI distance;
        private RectTransform HPPanel;

        public GameHUDController(TextMeshProUGUI distanceText, RectTransform HPPanel, GameObject heartPrefab)
        {
            this.distance = distanceText;
            this.HPPanel = HPPanel;
            this.heartPrefab = heartPrefab;
        }

        public void UpdateDistance(int distanceToAdd)
        {

            int.TryParse(distance.text, out int res);
            distance.text = (distanceToAdd + res).ToString();

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="hpModification">integer, hp can be a value from 0 to 10</param>
        public void UpdateHP(int hpModification,PlayerManager player)
        {
            if(player.currentHP + hpModification > player.maxHP ||
               player.currentHP + hpModification < 0 )
            {
                return;
            }

            if (hpModification < 0)
            {
                for (int i = 0; i < hpModification; i++)
                {

                }
            }
            else
            {
                for (int i = 0; i < hpModification; i++)
                {

                }
            }
        }
    }
}
