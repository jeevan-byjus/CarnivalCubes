﻿using Byjus.Gamepod.CarnivalCubes.Verticals;
using UnityEngine;

#if CC_STANDALONE
namespace Byjus.Gamepod.CarnivalCubes.Externals {

    /// <summary>
    /// The top most parent in view hierarchy in case we are running standalone
    /// </summary>
    public class StandaloneExternalParent : MonoBehaviour {
        public HierarchyManager hierarchyManager;

        void AssignRefs() {
            hierarchyManager = FindObjectOfType<HierarchyManager>();
        }

        private void Start() {
            AssignRefs();
            Factory.SetVisionService(new StandaloneVisionService());
            hierarchyManager.Setup();
        }
    }
}
#endif