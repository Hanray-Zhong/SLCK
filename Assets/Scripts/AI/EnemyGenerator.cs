using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public Vector2[] GeneratePoints; 
    public float GenerateCD;
    public GameObject[] Enemys;

    private float GenerateTimer;
    private float RunTime;

    void FixedUpdate() {
        RunTime = Time.time;
        GenerateCD =  240 / ((int)(RunTime / 60) + 1);
        Generate();
    }

    void Generate() {
        if (GenerateTimer >= GenerateCD) {
            int PointIndex = Random.Range(0, GeneratePoints.Length);
            int EnemyIndex;
            float i = Random.Range(0f, 1f);
            if (i >= 0 && i < 0.8f) 
                EnemyIndex = 0;
            else if (i >= 0.8f && i < 0.95f)
                EnemyIndex = 1;
            else if (i >= 0.95f && i <= 1) 
                EnemyIndex = 2;
            else
                EnemyIndex = 0;
            Instantiate(Enemys[EnemyIndex], GeneratePoints[PointIndex], Enemys[EnemyIndex].transform.rotation);
            GenerateTimer = 0;
        }
        GenerateTimer++;
    }

    private void OnDrawGizmosSelected() {
        for ( int i = 0; i < GeneratePoints.Length; i++)
			Gizmos.DrawSphere(GeneratePoints[i], 0.05f);
    }
}
