using UnityEngine;
using System.Collections.Generic;

public class ObjectGenerator : MonoBehaviour
{
    public List<GameObject> availableObjects;
    public List<GameObject> currentObjects;


    public List<GameObject> PowerupAvailableObjects;
    public List<GameObject> PowerupcurrentObjects;

    private float minY;
    private float maxY;

    private float screenWidth;
    private float minDistanceBetweenObjs;
    private int counter=0;
    float maxDistX = 0.0f;
    public void Start()
    {
        var height = Camera.main.orthographicSize;
        this.minY = -height / 2;
        this.maxY = height / 2;

        this.screenWidth = height * 2 * Camera.main.aspect;
        this.minDistanceBetweenObjs = this.screenWidth;

        Invoke("SpawnPopupup", Random.Range(20, 25));
        //Invoke("SpawnPopupup", Random.Range(5, 10));

    }

    public void Update()
    {
        var position = this.transform.position;

        var maxOffsetX = position.x + this.screenWidth;
        var minOffsetX = position.x - this.screenWidth;

        var farthestDistanceX = 0.0f;

        var objectsToRemove = new List<GameObject>();
        var PowerupobjectsToRemove = new List<GameObject>();

        foreach (var obj in this.currentObjects)
        {
            var objCenterX = obj.transform.position.x;
            farthestDistanceX = Mathf.Max(farthestDistanceX, objCenterX);

            if (objCenterX < minOffsetX)
            {
                objectsToRemove.Add(obj);
            }
        }

        foreach (var obj in this.PowerupcurrentObjects)
        {
            var objCenterX = obj.transform.position.x;
            //farthestDistanceX = Mathf.Max(farthestDistanceX, objCenterX);

            if (objCenterX < minOffsetX)
            {
                PowerupobjectsToRemove.Add(obj);
            }
        }

        if (minDistanceBetweenObjs >= this.screenWidth / 3)
        {
            this.minDistanceBetweenObjs -= 0.001f;
        }

        foreach (var obj in objectsToRemove)
        {
            this.currentObjects.Remove(obj);
                Destroy(obj);
        }
        if (PowerupobjectsToRemove.Count > 1)
        {
            foreach (var obj in PowerupobjectsToRemove)
            {
                this.PowerupcurrentObjects.Remove(obj);
                Destroy(obj);
            }
        }

        if (farthestDistanceX < maxOffsetX)
        {
            maxDistX = farthestDistanceX + this.minDistanceBetweenObjs;
            this.AddObject(farthestDistanceX + this.minDistanceBetweenObjs);
        }
    }
    public void SpawnPopupup()
    {
        if (!PlayerController.gameOver)
        {
            var randomObjIndex = 1;
            randomObjIndex = Random.Range(0, this.PowerupAvailableObjects.Count);
            //randomObjIndex = 2;
            var obj = Instantiate(this.PowerupAvailableObjects[randomObjIndex]);
            this.PowerupcurrentObjects.Add(obj);
            var position = obj.transform.position;
            position.x = maxDistX + 5.0f;
            position.y = Random.Range(this.minY, this.maxY);
            obj.transform.position = position;

            Invoke("SpawnPopupup", Random.Range(20, 25));
            //Invoke("SpawnPopupup", Random.Range(5, 10));

        }
        else
        {
            CancelInvoke();

        }
    }
    public void AddObject(float maxDistanceX)
    {
        //if (!MouseController.IsObstacleOn)
        {
                var randomObjIndex = 1;
                if (!PlayerController.isBubbleOn && !PlayerController.isMagnetOn && !PlayerController.isSpeedDashOn)
                    randomObjIndex = Random.Range(0, this.availableObjects.Count);

                var  obj = Instantiate(this.availableObjects[randomObjIndex]);    
                this.currentObjects.Add(obj);

                var position = obj.transform.position;
                position.x = maxDistanceX;
                position.y = Random.Range(this.minY, this.maxY);
                obj.transform.position = position;

        }        
    }
}
