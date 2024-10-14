using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorManager : MonoBehaviour
{
    [SerializeField] private ConveyorManager connectionOut;
    [HideInInspector] public ConveyorManager connectionIn;
    [SerializeField] private float spawnRate = 1f;
    private float chrono = 0f;
    [SerializeField] private int batch = 50;
    [SerializeField] private GameObject prefab;
    private Queue<ConveyorItem> queue = new();

    [SerializeField]private Transform start;
    [SerializeField]private Transform end;

    [SerializeField]private float speed = 1f;

    private void AddBatch(){
        for(int _=0; _ < batch;_++){
            GameObject newItem = Instantiate(prefab);
            newItem.SetActive(false);
            queue.Enqueue(newItem.GetComponent<ConveyorItem>());
        }
    }

    public void Spawn(){
        if(queue.Count == 0){
            AddBatch();
        }

        ConveyorItem item = queue.Dequeue();
        item.Initialize(this, start.position, end.position, speed);
        item.gameObject.SetActive(true);
    }

    void Start(){
        if(connectionOut != null)
            connectionOut.connectionIn = this;
    }
    void Update()
    {
        if(connectionIn == null){
            chrono += Time.deltaTime;
            if(chrono >= spawnRate){
                chrono = 0f;
                Spawn();
            }
        }
    }

    public void ItemArrived(ConveyorItem item){
        item.gameObject.SetActive(false);
        queue.Enqueue(item);
        if(connectionOut != null)
            connectionOut.Spawn();
    }
}
