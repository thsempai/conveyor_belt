using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorManager : MonoBehaviour
{
    [SerializeField] float spawnRate = 1f;
    float chrono = 0f;
    [SerializeField] int batch = 50;
    [SerializeField] GameObject prefab;
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

    private void Spawn(){
        if(queue.Count == 0){
            AddBatch();
        }

        ConveyorItem item = queue.Dequeue();
        item.Initialize(this, start.position, end.position, speed);
        item.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        chrono += Time.deltaTime;
        if(chrono >= spawnRate){
            chrono = 0f;
            Spawn();
        }
    }

    public void ItemArrived(ConveyorItem item){
        item.gameObject.SetActive(false);
        queue.Enqueue(item);
    }
}
