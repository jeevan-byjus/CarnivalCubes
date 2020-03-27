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

        bool gameWaiting;

        public void Init() {
            mapSecIdToView = new Dictionary<int, SectionInfo>();
            var viewSections = view.GetSections();
            foreach (var sec in viewSections) {
                mapSecIdToView.Add(sec.id, sec);
            }

            GenerateNumbers();

            gameWaiting = true;
            view.ShowIntro(() => {
                view.Setup(mapSecIdToView.Values.ToList(), totalReqd, () => { });
                gameWaiting = false;
                view.StartTimer(20, 1);
            });
        }

        public void OnTimerExpired() {
            gameWaiting = true;
            view.ShowGameText("DONE...", () => {
                view.Wait(1, () => {
                    view.ShowGameText("EVALUATING......", () => {
                        view.Wait(3, () => {
                            Validate();
                        });
                    });
                });
            });
        }

        void Validate() {
            int finalCount = 0;
            foreach (var sec in mapSecIdToView.Values) {
                finalCount += sec.number;
            }

            if (finalCount == totalReqd) {
                view.ShowGameText("CONGRATS... YOU SCORED " + finalCount + ". YOU WIN..!!!", () => { });
            } else {
                view.ShowGameText("SORRY.. YOU SCORED " + finalCount + ". NEXT TIME MAYBE..!!!", () => { });
            }
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
            if (gameWaiting) {
                return;
            }

            view.ClearAll(() => { });

            foreach (var sec in sections) {
                var viewSec = mapSecIdToView[sec.id];
                foreach (var item in sec.items) {
                    Debug.Log("Creating item: " + item);
                    view.CreateRed(item.position, () => { });
                }

                view.MarkOccupied(viewSec, sec.items.Count > 0, () => { });
            }
        }
    }

    public interface IGameManagerCtrl {
        void Init();
        void OnTimerExpired();
    }
}