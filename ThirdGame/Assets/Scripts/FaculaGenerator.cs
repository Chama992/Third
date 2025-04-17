using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class FaculaGenerator : MonoBehaviour
{
    public GameObject[] faculas;
    public List<GameObject> curFaculas;
    public float freshTime;
    public Transform freshMaxPoint;
    public Transform freshMinPoint;
    public Transform delPoint;
    public float speed;
    private float freshTimer;
    private void Start()
    {
        freshTimer = freshTime;
    }

    private void OnDisable()
    {
        for (int i = curFaculas.Count - 1; i >= 0; i--)
        {
            Destroy(curFaculas[i]);
        }
    }
    
    private void Update()
    {
        freshTimer -= Time.deltaTime;
        if (freshTimer < 0)
        {
            freshTimer = freshTime;
            GenerateFacula();
        }
    }
    private void GenerateFacula()
    {
        int index = Random.Range(0, faculas.Length - 1);
        float x = freshMinPoint.position.x;
        float y = Random.Range(freshMinPoint.position.y, freshMaxPoint.position.y);
        Vector3 rota = new Vector3(0,0, Random.Range(0f,360f));
        Vector3 sca = new Vector3(Random.Range(1f,1.2f),Random.Range(1f,1.2f), Random.Range(1f,1.2f));
        GameObject newFacula = Instantiate(faculas[index],transform);
        newFacula.transform.position = new Vector3(x,y,0);
        newFacula.transform.eulerAngles = rota;
        newFacula.transform.localScale = sca;
        newFacula.GetComponent<Facula>().Init(speed,delPoint);
        newFacula.SetActive(true);
        curFaculas.Add(newFacula);
    }
}
