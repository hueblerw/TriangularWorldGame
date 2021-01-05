using UnityEngine;

public class ObjectClickedController : MonoBehaviour {

    private const string TILE_INFO_TITLE = "Tile Info";
    private const int SPACE_PER_LINE = 25;

    public WorldMapController worldMapController;

	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            Vector2 recalculatedOrigin = new Vector2(-ray.origin.z * (ray.direction.x / ray.direction.z) + ray.origin.x, -ray.origin.z * (ray.direction.y / ray.direction.z) + ray.origin.y);
            RaycastHit2D hit = Physics2D.Raycast(recalculatedOrigin, Vector2.zero, Mathf.Infinity);

            if (hit != null && hit.collider != null)
            {
                if (hit.collider != null)
                {
                    MainDataController mainDataController = GameObject.Find("MainDataPanel").GetComponent<MainDataController>();
                    string dataInfo = worldMapController.world.getTileInfo(getCoordinates(hit.collider.gameObject.name));
                    mainDataController.titleText = TILE_INFO_TITLE;
                    mainDataController.bodyText = dataInfo;
                    mainDataController.textSize = SPACE_PER_LINE * countNumberOfLines(dataInfo);
                    mainDataController.updateDisplay();
                }
            }
        }
	}

    private Vector2 getCoordinates(string gameObjectName)
    {
        string[] data = gameObjectName.Split('-');
        string[] coors = data[0].Split(',');
        return new Vector2(float.Parse(coors[0]), float.Parse(coors[1]));
    }

    private int countNumberOfLines(string text)
    {
        return text.Split('\n').Length + 1;
    }

}
