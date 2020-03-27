using UnityEngine;
using Byjus.Gamepod.CarnivalCubes.Views;
using Byjus.Gamepod.CarnivalCubes.Verticals;
using System.Collections.Generic;

namespace Byjus.Gamepod.CarnivalCubes.Controllers {
    public class GameManagerCtrl : IGameManagerCtrl, IExtInputListener {
        public IGameManagerView view;
        Dictionary<int, SectionInfo> mapSecIdToView;

        public void Init() {
            mapSecIdToView = new Dictionary<int, SectionInfo>();
            var viewSections = view.GetSections();
            foreach (var sec in viewSections) {
                mapSecIdToView.Add(sec.id, sec);
            }
        }

        public void OnInput(List<SectionData> sections) {
            foreach (var sec in sections) {
                var viewSec = mapSecIdToView[sec.id];
                view.MarkOccupied(viewSec, sec.items.Count > 0, () => {

                });
            }
        }
    }

    public interface IGameManagerCtrl {
        void Init();
    }
}