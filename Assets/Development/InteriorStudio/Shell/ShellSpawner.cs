using UnityEngine;
using System.Collections.Generic;

public class ShellSpawner : MonoBehaviour
{
    public Vector3 roomSize = new();
    [SerializeField] private List<GameObject> wallPlanes = new();
    [SerializeField] private LayerMask interiorLayer;
    [SerializeField] private InteriorStudioManager studioManager;
    [SerializeField] private const int wallCount = 6;
    [SerializeField] private int index;

    private void Start()
    {
        studioManager.roomSize = roomSize;
        SpawnWalls(roomSize);
    }

    public void SpawnWalls(Vector3 size)
    {
        for (int i = 0; i < wallPlanes.Count; i++)
        {
            if (wallPlanes[i] != null)
                Destroy(wallPlanes[i]);
        }
        wallPlanes.Clear();
        for (int i = 0; i < wallCount; i++)
        {
            index = i;
            switch (index)
            {
                case 0:
                    var frontWall = GameObject.CreatePrimitive(PrimitiveType.Plane);
                    frontWall.transform.SetParent(this.transform, true);
                    frontWall.name = "Front Wall";
                    frontWall.transform.rotation = Quaternion.Euler(new Vector3(90,90,0));
                    frontWall.transform.localScale = frontWall.transform.localScale + new Vector3(roomSize.x / 10, roomSize.y / 10, roomSize.z / 10);
                    frontWall.transform.position = transform.position + new Vector3(-roomSize.x * 1.5f, 0,0);
                    wallPlanes.Add(frontWall);
                    break;
                case 1:
                    var backWall = GameObject.CreatePrimitive(PrimitiveType.Plane);
                    backWall.transform.SetParent(this.transform, true);
                    backWall.name = "Back Wall";
                    backWall.transform.rotation = Quaternion.Euler(new Vector3(90, -90, 0));
                    backWall.transform.localScale = backWall.transform.localScale + new Vector3(roomSize.x / 10, roomSize.y / 10, roomSize.z / 10);
                    backWall.transform.position = transform.position + new Vector3(roomSize.x * 1.5f, 0, 0);
                    wallPlanes.Add(backWall);
                    break;
                case 2:
                    var leftWall = GameObject.CreatePrimitive(PrimitiveType.Plane);
                    leftWall.transform.SetParent(this.transform, true);
                    leftWall.name = "Left Wall";
                    leftWall.transform.rotation = Quaternion.Euler(new Vector3(90, 180, 0));
                    leftWall.transform.localScale =leftWall.transform.localScale+ new Vector3(roomSize.x / 10, roomSize.y / 10, roomSize.z / 10);
                    leftWall.transform.position = transform.position + new Vector3(0,0,roomSize.x * 1.5f);
                    wallPlanes.Add(leftWall);
                    break;
                case 3:
                    var rightWall = GameObject.CreatePrimitive(PrimitiveType.Plane);
                    rightWall.transform.SetParent(this.transform, true);
                    rightWall.name = "Right Wall";
                    rightWall.transform.rotation = Quaternion.Euler(new Vector3(-90, -180, 0));
                    rightWall.transform.localScale = rightWall.transform.localScale + new Vector3(roomSize.x/10, roomSize.y / 10, roomSize.z / 10);
                    rightWall.transform.position = transform.position + new Vector3(0,0, -roomSize.x*1.5f);
                    wallPlanes.Add(rightWall);
                    break;
                case 4:
                    var floorPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
                    floorPlane.transform.SetParent(this.transform, true);
                    floorPlane.name = "Floor";
                    floorPlane.transform.localScale = floorPlane.transform.localScale + new Vector3(roomSize.x / 10, roomSize.y / 10, roomSize.z / 10);
                    floorPlane.transform.position = transform.position + new Vector3(0, -roomSize.y * 1.5f, 0);
                    wallPlanes.Add(floorPlane);
                    break;
                case 5:
                    var cielPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
                    cielPlane.transform.SetParent(this.transform, true);
                    cielPlane.name = "Cieling";
                    cielPlane.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
                    cielPlane.transform.localScale = cielPlane.transform.localScale + new Vector3(roomSize.x / 10, roomSize.y / 10, roomSize.z / 10);
                    cielPlane.transform.position = transform.position + new Vector3(0, roomSize.y * 1.5f, 0);
                    wallPlanes.Add(cielPlane);
                    AddCollisionToWalls();
                    break;

            }
        }
       
    }

    void AddCollisionToWalls()
    {
        foreach(var wall in wallPlanes)
        {
            var mesh = wall.GetComponent<MeshCollider>();
            DestroyImmediate(mesh);
           var comp =  wall.AddComponent<BoxCollider>();
            comp.includeLayers = LayerMask.NameToLayer("Player");
            
        }
    }
}
