using UnityEngine;
using Byjus.Gamepod.CarnivalCubes.Controllers;
using Byjus.Gamepod.CarnivalCubes.Views;
using Byjus.Gamepod.CarnivalCubes.Verticals;
using System.Collections.Generic;

namespace Byjus.Gamepod.CarnivalCubes.Externals {
    /// <summary>
    /// Since there are controllers (non-monobehaviors) involved, we can't just directly assign references
    /// So, this class is used which manages all reference assigning
    /// If a View needs some objects, it can be directly assigned
    /// This class is just to assign references between components
    /// Like, Controller-View connections, Parent-Child connections etc.
    /// </summary>
    public class HierarchyManager : MonoBehaviour {
        [SerializeField] InputParser inputParser;
        [SerializeField] GameManagerView gameManager;

        public void Setup() {
            GameManagerCtrl gameCtrl = new GameManagerCtrl();

            inputParser.inputListener = gameCtrl;
            gameManager.ctrl = gameCtrl;
            gameCtrl.view = gameManager;

            var visionSections = new List<SectionData>();
            visionSections.Add(new SectionData { id = 1, min = new Vector2(0, 0), max = new Vector2(0.33f, 0.2f) });
            visionSections.Add(new SectionData { id = 2, min = new Vector2(0.34f, 0), max = new Vector2(0.66f, 0.2f) });
            visionSections.Add(new SectionData { id = 2, min = new Vector2(0.67f, 0), max = new Vector2(1f, 0.2f) });
            visionSections.Add(new SectionData { id = 2, min = new Vector2(0, 0.21f), max = new Vector2(0.33f, 0.4f) });
            visionSections.Add(new SectionData { id = 2, min = new Vector2(0.34f, 0.21f), max = new Vector2(0.66f, 0.4f) });
            visionSections.Add(new SectionData { id = 2, min = new Vector2(0.67f, 0.21f), max = new Vector2(1f, 0.4f) });

            ((IGameManagerCtrl) gameCtrl).Init();
            inputParser.Init(visionSections);
        }
    }
}