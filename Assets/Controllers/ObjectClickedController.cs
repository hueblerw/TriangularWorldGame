using UnityEngine;

public class ObjectClickedController : MonoBehaviour {

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
                    Debug.Log(worldMapController.world.getTileInfo(getCoordinates(hit.collider.gameObject.name)));
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

}
