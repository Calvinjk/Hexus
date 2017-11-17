using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wunderwunsch.HexGridSimplified
{    
    public class GameManagerDepreciated : MonoBehaviour
    {
        [SerializeField]
        private Camera primaryCamera = null;
        [SerializeField]
        private Camera wrapCamera = null;
        [SerializeField]
        private MapGenerator mapGenerator = null;
        public Map Map { get; private set; }
        

        // Use this for initialization
        void Start()
        {
            InitMap();
            Debug.Log("HexMapSize: " + HexDepreciated.MapSize);
            InitCameras();
            InitUnits();
        }

        private void InitMap()
        {
            Map m = mapGenerator.GenerateMap();
            m.InitVisualisation();
            Map = m;
        }

        private void InitCameras()
        {
            primaryCamera.GetComponent<MainCameraWrap>().Init();
            wrapCamera.GetComponent<WrapCamera>().Init();
        }

        private void InitUnits()
        {
            ////TODO mapGenerator should also generate spawnpositions
            //initialSpawner.SpawnUnit(0, new Vector3Int(2, 2, -4));
            ////debugSpawner.SpawnUnit(0, 0, new Vector3Int(2, 2, -4));
            //initialSpawner.SpawnUnit(0, new Vector3Int(7, 5, -12));
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
