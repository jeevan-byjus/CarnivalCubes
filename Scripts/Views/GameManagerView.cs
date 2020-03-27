using UnityEngine;
using Byjus.Gamepod.CarnivalCubes.Controllers;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Byjus.Gamepod.CarnivalCubes.Views {

    public class GameManagerView : MonoBehaviour, IGameManagerView {
        [SerializeField] List<SectionInfo> oneTimeSections;
        [SerializeField] Text totalReqdText;
        [SerializeField] Text gameText;
        [SerializeField] GameObject redPrefab;
        [SerializeField] GameObject bluePrefab;
        [SerializeField] Text timerText;

        List<GameObject> gos;

        public IGameManagerCtrl ctrl;

        public void Setup(List<SectionInfo> sections, int totalReqd, Action onDone) {
            gos = new List<GameObject>();

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

        public void ClearAll(Action onDone) {
            foreach (var go in gos) {
                Destroy(go.gameObject);
            }
            gos.Clear();

            onDone();
        }

        public void CreateRed(Vector2 position, Action onDone) {
            var go = Instantiate(redPrefab, position, Quaternion.identity, transform);
            gos.Add(go);

            onDone();
        }

        public void Wait(float seconds, Action onDone) {
            StartCoroutine(WaitUntil(seconds, onDone));
        }

        System.Collections.IEnumerator WaitUntil(float seconds, Action onDone) {
            yield return new WaitForSeconds(seconds);

            onDone();
        }

        public void ShowGameText(string text, Action onDone) {
            gameText.text = text;
            onDone();
        }

        public void ShowIntro(Action onDone) {
            StartCoroutine(IntroRoutine(onDone));
        }

        float introDelay = 3;

        System.Collections.IEnumerator IntroRoutine(Action onDone) {
            gameText.text = "Are you ready for the challenge..!!!";

            yield return new WaitForSeconds(introDelay);

            gameText.text = "You want an item. Throw a red cube on it..";

            yield return new WaitForSeconds(introDelay);

            gameText.text = "Each item gives you points..";

            yield return new WaitForSeconds(introDelay);

            gameText.text = "Collect the required amount of points (nothing less, nothing more) to win the game..!!!";

            yield return new WaitForSeconds(introDelay);

            gameText.text = "And oh yes, there is a timer!!";

            yield return new WaitForSeconds(introDelay);

            gameText.text = "READY!!!";

            yield return new WaitForSeconds(introDelay);

            gameText.text = "GO...!!!";

            onDone();
        }

        public void StartTimer(float seconds, float step) {
            currTime = seconds;
            timerText.text = currTime + "";
            StartCoroutine(Timer(step));
        }

        float currTime;

        System.Collections.IEnumerator Timer(float step) {
            while (currTime > 0) {
                yield return new WaitForSeconds(step);

                currTime -= step;
                timerText.text = currTime + "";
            }

            ctrl.OnTimerExpired();
        }

        public void ShowOutro(Action onDone) {
            StartCoroutine(OutroRoutine(onDone));
        }

        System.Collections.IEnumerator OutroRoutine(Action onDone) {
            gameText.text = "DONE... HANDS OFF..!!";

            yield return new WaitForSeconds(1);

            gameText.text = "EVALUATING...";

            yield return new WaitForSeconds(2);

            onDone();
        }
    }

    public interface IGameManagerView {
        void ClearAll(Action onDone);
        void CreateRed(Vector2 position, Action onDone);
        void Setup(List<SectionInfo> sections, int totalReqd, Action onDone);
        void MarkOccupied(SectionInfo section, bool occupied, Action onDone);
        List<SectionInfo> GetSections();

        void ShowGameText(string text, Action onDone);
        void Wait(float seconds, Action onDone);
        void ShowIntro(Action onDone);
        void StartTimer(float seconds, float step);
        void ShowOutro(Action onDone);
    }
}