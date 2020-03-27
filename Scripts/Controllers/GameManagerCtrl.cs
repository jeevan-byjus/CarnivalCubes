using UnityEngine;
using Byjus.Gamepod.CarnivalCubes.Views;
using Byjus.Gamepod.CarnivalCubes.Verticals;
using System.Collections.Generic;
using System.Linq;

namespace Byjus.Gamepod.CarnivalCubes.Controllers {
    public class GameManagerCtrl : IGameManagerCtrl, IExtInputListener {
        public IGameManagerView view;
        Dictionary<int, SectionInfo> mapSecIdToView;
        int totalReqd;

        public void Init() {
            mapSecIdToView = new Dictionary<int, SectionInfo>();
            var viewSections = view.GetSections();
            foreach (var sec in viewSections) {
                mapSecIdToView.Add(sec.id, sec);
            }

            GenerateNumbers();
            view.Setup(mapSecIdToView.Values.ToList(), totalReqd, () => { });
        }

        void GenerateNumbers() {
            totalReqd = 18;
            var nums = new List<int> { 4, 5, 8, 3, 2, 7 };

            for (int i = 1; i <= 6; i++) {
                mapSecIdToView[i].number = nums[i - 1];
                mapSecIdToView[i].occupied = false;
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