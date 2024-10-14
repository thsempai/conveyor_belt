using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorItem : MonoBehaviour
{
    private Vector3 start;
    private Vector3 end;

    private float time;
    private float chrono = 0f;
    private ConveyorManager manager;

    

    public void Initialize(ConveyorManager manager, Vector3 start, Vector3 end, float speed){
        this.manager = manager;
        this.start = start;
        this.end = end;
        this.time = Vector3.Distance(start, end) / speed;
        chrono = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        chrono += Time.deltaTime;
        transform.position = Vector3.Lerp(start, end, chrono/time);
        if(chrono >= time){
            manager.ItemArrived(this);
        } 
    }

    void OnMouseDown(){
        Destroy(gameObject);
    }
}
