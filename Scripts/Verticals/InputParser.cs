using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Byjus.Gamepod.CarnivalCubes.Util;

namespace Byjus.Gamepod.CarnivalCubes.Verticals {
    public class InputParser : MonoBehaviour {

        public IExtInputListener inputListener;

        IVisionService visionService;
        int inputCount;
        List<SectionData> sections;

        public void Init(List<SectionData> sections) {
            visionService = Factory.GetVisionService();
            inputCount = 0;
            this.sections = sections;

            StartCoroutine(ListenForInput());
        }

        IEnumerator ListenForInput() {
            yield return new WaitForSeconds(Constants.INPUT_DELAY);
            inputCount++;

            var objs = visionService.GetVisionObjects();
            Process(objs);

            StartCoroutine(ListenForInput());
        }

        void Process(List<ExtInput> objs) {
            objs.RemoveAll(x => x.type == TileType.BLUE_ROD);
            objs.RemoveAll(x => x.normalizedPosition.x < 0 || x.normalizedPosition.x > 1 || x.normalizedPosition.y < 0 || x.normalizedPosition.y > 1);

            foreach (var sec in sections) { sec.items.Clear(); }

            foreach (var obj in objs) {
                foreach (var sec in sections) {
                    if (obj.normalizedPosition.x >= sec.min.x &&
                        obj.normalizedPosition.x <= sec.max.x &&
                        obj.normalizedPosition.y >= sec.min.y &&
                        obj.normalizedPosition.y <= sec.max.y) {

                        Debug.LogError("Adding obj: " + obj + ", in section: " + sec);
                        sec.items.Add(obj);
                        break;
                    }
                }
            }
            
            inputListener.OnInput(sections);
        }
    }

    public interface IExtInputListener {
        void OnInput(List<SectionData> objs);
    }

}