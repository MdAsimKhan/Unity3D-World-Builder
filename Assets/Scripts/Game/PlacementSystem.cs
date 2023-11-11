using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject mouseIndicator, cellIndicator;

    [SerializeField]
    private InputManager inputManager;

    [SerializeField]
    private Grid grid;

    [SerializeField]
    private ObjectsDatabaseSO database;

    private int selectedObject = -1;

    private void Start()
    {
        StopPlacement();
    }

    private void Update()
    {
        // if no object is selected (from the UI), do nothing
        if (selectedObject < 0)
        {
            return;
        }

        // get the current mouse position and move the indicator to it
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        mouseIndicator.transform.position = mousePosition;
    }

    public void StartPlacement(int ID)
    {
        StopPlacement();
        selectedObject = database.objectsData.FindIndex(data => data.ID == ID);
        if (selectedObject < 0)
        {
            Debug.LogError("Not found");
            return;
        }
        inputManager.OnClicked += PlaceStructure;
        inputManager.OnExit += StopPlacement;
    }

    public void PlaceStructure()
    {
        if (inputManager.IsPointerOverUI())
        {
            return;
        }

        Vector3 mousePosition = inputManager.GetSelectedMapPosition();

        // get the cell position of the mouse position
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        // instantiate the object at the cell position from the SO DB
        GameObject newObject = Instantiate(database.objectsData[selectedObject].Prefab);

        // set the object's position to the cell position
        newObject.transform.position = grid.CellToWorld(gridPosition);
    }

    public void StopPlacement()
    {
        selectedObject = -1;
        inputManager.OnClicked -= PlaceStructure;
        inputManager.OnExit -= StopPlacement;
    }
}
