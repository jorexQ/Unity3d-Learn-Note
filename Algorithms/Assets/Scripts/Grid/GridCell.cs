using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Grid
{
    public class GridCell
    {
        public int x { get; set; }

        public int y { get; set; }

        public bool walkable { get; set; }

        public Vector3 worldPosition { get; set; }



        public GameObject quad { get; set; }


        public float GetH(GridCell startCell)
        {
            return GridManager.GetDistance(this, startCell);
        }

        public float GetG(GridCell endCell)
        {
            return GridManager.GetDistance(this, endCell);
        }

    }
}
