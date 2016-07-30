using Assets.Scripts.Grid;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Algorithm
{
    public class CostManager
    {
        public List<CellCost<GridCell>> openCellCosts { private set; get; }
        public object Spwatch { get; private set; }

        public List<GridCell> closeCells = new List<GridCell>();
        public GridCell startCell;
        public GridCell endCell;

        private CellCost<GridCell> endCellCost;
        private GridManager gridManager;

        public CostManager(GridManager gridManager, Vector3 startPoint, Vector3 endPoint)
        {
            openCellCosts = new List<CellCost<GridCell>>();
            startCell = gridManager.GetCellFromPosition(startPoint);
            endCell = gridManager.GetCellFromPosition(endPoint);
            this.gridManager = gridManager;
            openCellCosts.Add(new CellCost<GridCell>
            {
                gridCell = startCell,
                g = startCell.GetG(startCell),
                h = startCell.GetH(endCell)
            });
        }

        public IEnumerator SearchMinCostLinePath()
        {
            var spw = new System.Diagnostics.Stopwatch();
            spw.Start();
            while (true)
            {
                var assessCell = OpenListPopFirst();               
                if (assessCell.gridCell.worldPosition == endCell.worldPosition)
                {
                    spw.Stop();
                    Debug.Log("use time : " + spw.ElapsedMilliseconds);
                    endCellCost = assessCell;
                    yield break;
                };

                List<CellCost<GridCell>> minCostCells;
                if (TryGetMinCostCellFromNeighbours(assessCell, out minCostCells))
                {
                    openCellCosts.AddRange(minCostCells);
                    foreach (var item in minCostCells)
                    {
                        gridManager.DrawCell(item.gridCell, true, true);
                    }
                }
                else
                {
                    //Debug.Log("close cell count : " + closeCells.Count);
                    openCellCosts.Remove(assessCell);
                    closeCells.Add(assessCell.gridCell);
                    gridManager.DrawCell(assessCell.gridCell, true, true, (m) => { m.color = Color.yellow; });
                }
                OpenListSort();
                yield return new WaitForSeconds(0.05f);
            }
        }

        public List<GridCell> GetProcessCells()
        {
            var processCells = new List<GridCell>();
            processCells.Add(endCellCost.gridCell);
            var childCell = endCellCost;
            while (true)
            {
                gridManager.DrawCell(childCell.gridCell, true, true,(material) => { material.color = Color.black; });
                if (childCell.parent == null) break;
                var parentCell = childCell.parent;
                processCells.Add(parentCell.gridCell);
                childCell = parentCell;
            }
            return processCells;
        }

        private bool TryGetMinCostCellFromNeighbours(CellCost<GridCell> currentCellCost, out List<CellCost<GridCell>> minCostCells)
        {
            var isImpasse = true;
            minCostCells = new List<CellCost<GridCell>>();
            var minCostCell = new CellCost<GridCell>();
            var neighbours = gridManager.GetNeighbours(currentCellCost.gridCell);
            for (var i = 0; i<neighbours.Count; i++)
            {
                var item = neighbours[i];
                if (!item.walkable || closeCells.Contains(item) || OpenListContains(item)) continue;
                var cellCost = new CellCost<GridCell>
                {
                    gridCell = item,
                    g = currentCellCost.g + item.GetG(currentCellCost.gridCell),
                    h = item.GetH(endCell),
                    parent = currentCellCost
                };

                if (minCostCell.CompareTo(cellCost) > 0 || minCostCell.f == 0)
                {
                    minCostCell = cellCost;
                    if (minCostCells.Count > 0) minCostCells.Clear();
                    minCostCells.Add(cellCost);
                }
                else if (minCostCell.CompareTo(cellCost) == 0)
                {
                    minCostCells.Add(cellCost);
                }
                isImpasse = false;
            }
            return !isImpasse;           
        }

        private CellCost<GridCell> OpenListPopFirst()
        {
            var item = openCellCosts[0];
            //openCellCosts.Remove(item);
            //closeCells.Add(item.gridCell);
            //gridManager.DrawCell(item.gridCell, true, true, (m) => { m.color = Color.yellow; });
            return item;
        }

        private void OpenListSort()
        {
            openCellCosts.Sort((a,b)=> { return a.CompareTo(b); });
            //SortedList
            //openCellCosts = openCellCosts
            //    .OrderBy(o => o.f)
            //    .ToList();
        }

        private bool OpenListContains(GridCell gridCell)
        {
            return openCellCosts.Any(o => o.gridCell == gridCell);
        }      
    }
}
