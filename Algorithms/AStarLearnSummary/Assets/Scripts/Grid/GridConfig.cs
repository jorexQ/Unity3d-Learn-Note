using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Grid
{
    [Serializable]
    public class GridConfig
    {
        public Vector2 gridSize;

        public LayerMask unWalkableMask;

        [Range(0,1)]
        public float cellRadiusPercent;

        public Transform planTranform;
    }
}
