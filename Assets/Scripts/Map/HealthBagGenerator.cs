using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBagGenerator : MonoBehaviour
{
    public Vector2[] GeneratePoints; 
    public float GenerateCD;
    public GameObject HealthBag;

    private float GenerateTimer;
    private LayerMask HealthBagLayer;
    private int[] illegalPoint = new int[4];// 0 表示空，1 表示有

    private void Awake() {
        HealthBagLayer = LayerMask.NameToLayer("HealthBag");
    }

    void FixedUpdate() {
        Generate();
    }

    void Generate() {
        if (GenerateTimer >= GenerateCD) {
            foreach (var item in GeneratePoints) {
                Collider2D col = Physics2D.OverlapCircle(item, 1, 1 << HealthBagLayer);
                if (col == null) {
                    Instantiate(HealthBag, item, HealthBag.transform.rotation);
                    break;
                }
            }
            GenerateTimer = 0;
        }
        GenerateTimer++;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = new Color(1, 0, 0, 1);
        for ( int i = 0; i < GeneratePoints.Length; i++)
			Gizmos.DrawSphere(GeneratePoints[i], 0.05f);
    }
}
