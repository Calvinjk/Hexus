using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Wunderwunsch.HexGridSimplified
{ 
    public abstract class MapGenerator : MonoBehaviour
    {
        [SerializeField]
        protected Vector2Int mapSize;
        [SerializeField]
        protected List<int> parameters;
        [SerializeField]
        protected bool wrapHorizontal;
        [SerializeField]
        protected MapVisualisation mapVisualisation;
        public abstract Map GenerateMap(); 

        public void HasHorizontalWrap(bool wrap)
        {
            wrapHorizontal = wrap;
        }

        public void SetParameters(List<int> parameters)
        {
            this.parameters = parameters;
        }

        public void SetMapSize(Vector2Int mapSize)
        {
            this.mapSize = mapSize;
        }
    }
}
