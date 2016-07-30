using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Grid
{
    [Serializable]
    public class GridManager
    {
        private const string GRID_WRAP = "GridWrap";

        private GridConfig _gridConfig;
        // 地面标注单位大小
        private Vector3 _planStandardSize;

        public Transform GridWrap;
        public GridCell[,] Cells;

        public GridManager(GridConfig gridConfig)
        {
            _gridConfig = gridConfig;

            // 通过碰撞体来获取标注单位大小
            var collider = gridConfig.planTranform.GetComponent<MeshCollider>();
            _planStandardSize = collider.bounds.size;

            Cells = new GridCell[(int)gridConfig.gridSize.x,(int)gridConfig.gridSize.y];
            GridWrap = gridConfig.planTranform.FindChild(GRID_WRAP);
            if(GridWrap == null)
            {
                GridWrap = new GameObject(GRID_WRAP).transform;
            }
            CreateCell();
        }

        public void CreateCell()
        {
            var planXStarPoint = _gridConfig.planTranform.position.x - _planStandardSize.x / 2;
            var planYStarPoint = _gridConfig.planTranform.position.z - _planStandardSize.z / 2;
            var planItemX = _planStandardSize.x / _gridConfig.gridSize.x;
            var planItemY = _planStandardSize.z / _gridConfig.gridSize.y;
            var currentXPoint = planXStarPoint + planItemX / 2;
            for (var x = 0; x < _gridConfig.gridSize.x; x++)
            {
                var currentYPoint = planYStarPoint + planItemY / 2;
                for (var y = 0; y < _gridConfig.gridSize.y; y++)
                {
                    var worldPosition = new Vector3(currentXPoint, 0.75f, currentYPoint);
                    var cell = new GridCell
                    {
                        x = x,
                        y = y,
                        worldPosition = worldPosition,
                        walkable = !Physics.CheckSphere(worldPosition, 0.4f, _gridConfig.unWalkableMask)
                    };
                    Cells[x, y] = cell;
                    DrawCell(cell);
                    currentYPoint += planItemY;
                }
                currentXPoint += planItemX;
            }
        }

        public void DrawCell(GridCell cell, bool isColor = false, bool isPathLine = false, Action<Material> customSet = null)
        {
            var quad = cell.quad ?? GameObject.CreatePrimitive(PrimitiveType.Quad);
            if (!cell.walkable || isColor)
            {
                var mesh = quad.GetComponent<MeshRenderer>();
                mesh.material.color = isPathLine ? Color.green : Color.red;
                if (customSet != null)
                {
                    customSet(mesh.material);
                }
            }
            quad.transform.rotation = Quaternion.Euler(90, 0, 0);
            quad.transform.position = new Vector3(cell.worldPosition.x, 0.25f, cell.worldPosition.z);
            quad.transform.localScale = new Vector3(_gridConfig.cellRadiusPercent, _gridConfig.cellRadiusPercent, _gridConfig.cellRadiusPercent);
            quad.transform.parent = GridWrap;
            cell.quad = quad;
        }

        public List<GridCell> GetNeighbours(GridCell cell)
        {
            var neighbourCells = new List<GridCell>();
            for (var x = -1; x <= 1; x++)
            {
                for(var y = -1; y <= 1; y++)
                {
                    var currentCellX = cell.x + x;
                    var currentCellY = cell.y + y;
                    if (currentCellX >= Cells.GetLength(0) 
                        || currentCellY >= Cells.GetLength(1)
                        || currentCellX < 0 
                        || currentCellY < 0) continue;

                    var neighbourCell = Cells[currentCellX, currentCellY];
                    neighbourCells.Add(neighbourCell);
                }
            }
            return neighbourCells;
        }

        public GridCell GetCellFromPosition(Vector3 worldPosition)
        {
            var middleCellX = _gridConfig.gridSize.x / 2;
            var middleCellY = _gridConfig.gridSize.y / 2;
            var middleWorldX = _planStandardSize.x / 2;
            var middleWorldZ = _planStandardSize.z / 2;

            var unitCellDistanceX = middleWorldX / middleCellX ;
            var unitCellDistanceY = middleWorldZ  / middleCellY;

            var gridCellX = middleCellX + worldPosition.x / unitCellDistanceX;
            var gridCellY = middleCellY + worldPosition.z / unitCellDistanceY;
            
            var cell = Cells[Mathf.FloorToInt(gridCellX), Mathf.FloorToInt(gridCellY)];
            DrawCell(cell, true);
            return cell;
        }

        public static float GetDistance(GridCell from, GridCell to)
        {
            var distanceX = Mathf.Abs(to.x - from.x);
            var distanceY = Mathf.Abs(to.y - from.y);

            if (distanceX > distanceY)
            {
                return distanceY * 14 + 10 * (distanceX - distanceY);
            }
            else
            {
                return distanceX * 14 + 10 * (distanceY - distanceX);
            }
        }
    }
}
