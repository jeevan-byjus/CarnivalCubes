using UnityEngine;
using Byjus.Gamepod.CarnivalCubes.Controllers;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Byjus.Gamepod.CarnivalCubes.Views {

    public class GameManagerView : MonoBehaviour, IGameManagerView {
        [SerializeField] List<SectionInfo> oneTimeSections;
        [SerializeField] Text totalReqdText;

        public IGameManagerCtrl ctrl;

        public void Setup(List<SectionInfo> sections, int totalReqd, Action onDone) {
            foreach (var sec in sections) {
                sec.letterText.text = sec.number + "";
                sec.doneImage.gameObject.SetActive(sec.occupied);
            }

            totalReqdText.text = totalReqd + "";

            onDone();
        }

        public void MarkOccupied(SectionInfo section, bool occupied, Action onDone) {
            section.doneImage.gameObject.SetActive(occupied);
            onDone();
        }

        public List<SectionInfo> GetSections() {
            var ret = oneTimeSections;
            // removing this reference because ctrl will always provide the reqd section
            // keeping it in both areas can cause shared data updating problems
            oneTimeSections = null; 
            return ret;
        }
    }

    public interface IGameManagerView {
        void Setup(List<SectionInfo> sections, int totalReqd, Action onDone);
        void MarkOccupied(SectionInfo section, bool occupied, Action onDone);
        List<SectionInfo> GetSections();
    }
}