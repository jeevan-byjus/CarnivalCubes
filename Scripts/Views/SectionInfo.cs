using System;
using UnityEngine;
using UnityEngine.UI;

namespace Byjus.Gamepod.CarnivalCubes.Views {
    public class SectionInfo : MonoBehaviour {
        public int id;
        public GameObject sectionPanel;
        public Text letterText;
        public Image doneImage;

        public int number;
        public bool occupied;
    }
}