using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Grid;
using System.Collections;
using Assets.Scripts.Algorithm;

public class Main : MonoBehaviour {

    public Transform startPoint;
    public Transform endPoint;
    public GridConfig gridConfig;

    private GridManager gridManager;
    private CostManager costManager;

    private List<GridCell> openList = new List<GridCell>();
    private List<GridCell> closeList = new List<GridCell>();

    void Awake()
    {
        gridManager = new GridManager(gridConfig);
        costManager = new CostManager(gridManager, startPoint.position, endPoint.position);
        //var processCells = costManager.SearchMinCostLinePath();
    }

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(findPath());


    }
	
	// Update is called once per frame
	void Update ()
    {
	    
	}   

    private IEnumerator findPath()
    {
        yield return StartCoroutine(costManager.SearchMinCostLinePath());
        //var processCells = costManager.GetProcessCells();
    }
}
