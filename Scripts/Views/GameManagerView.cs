using UnityEngine;
using Byjus.Gamepod.CarnivalCubes.Controllers;
using System;
using System.Collections.Generic;

namespace Byjus.Gamepod.CarnivalCubes.Views {

    public class GameManagerView : MonoBehaviour, IGameManagerView {
        [SerializeField] List<SectionInfo> sections;
        public IGameManagerCtrl ctrl;

        public void MarkOccupied(SectionInfo section, bool occupied, Action onDone) {
            section.doneImage.gameObject.SetActive(occupied);
            onDone();
        }

        public List<SectionInfo> GetSections() {
            return sections;
        }
    }

    public interface IGameManagerView {
        void MarkOccupied(SectionInfo section, bool occupied, Action onDone);
        List<SectionInfo> GetSections();
    }
}