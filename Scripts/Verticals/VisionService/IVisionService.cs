using System.Collections.Generic;
using UnityEngine;
using Byjus.Gamepod.CarnivalCubes.Util;

namespace Byjus.Gamepod.CarnivalCubes.Verticals {
    /// <summary>
    /// This is the interface used by whichever class wants to read Vision Data
    /// Difference is it should mainly work with in-game models and shouldn't use anything platform dependent
    /// so, no vision related models or any other external platform related models
    /// </summary>
    public interface IVisionService {
        void Init();
        List<ExtInput> GetVisionObjects();
    }

    public enum TileType { RED_CUBE, BLUE_ROD }

    public class ExtInput {
        public TileType type;
        public int id;
        public Vector2 position;
        public Vector2 normalizedPosition;

        public ExtInput() { }

        public override string ToString() {
            return id + ", " + type + ", " + GenUtil.VecToString(position) + ", " + GenUtil.VecToString(normalizedPosition);
        }
    }

    public class SectionData {
        public int id;
        public Vector2 min;
        public Vector2 max;
        public List<ExtInput> items;

        public SectionData() {
            items = new List<ExtInput>();
        }

        public override string ToString() {
            return id + ", Min: " + min + ", Max: " + max + ", count: " + items.Count;
        }
    }
}