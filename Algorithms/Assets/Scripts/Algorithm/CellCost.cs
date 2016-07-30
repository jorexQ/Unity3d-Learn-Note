using Assets.Scripts.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Algorithm
{
    public class CellCost<T> where T : GridCell
    {
        public T gridCell { get; set; }

        public float g { get; set; }

        public float h { get; set; }

        public float f
        {
            get
            {
                return g + h;
            }
        }

        public CellCost<T> parent { get; set; }

        public int CompareTo(CellCost<T> cellCost)
        {
            var compare = f.CompareTo(cellCost.f);
            if (compare == 0)
            {
                compare = h.CompareTo(cellCost.h);
            }
            return compare;
        }
    }
}
