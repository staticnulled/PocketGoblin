using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private float size = 1f;

    public Vector2 GetNearestPointOnGrid (Vector2 position)
    {
        position -= (Vector2)transform.position;

        int xCount = Mathf.RoundToInt(position.x / size);
        int yCount = Mathf.RoundToInt(position.y / size);

        Vector2 result = new Vector2(
            (float)xCount * size,
            (float)yCount * size);

        result += (Vector2)transform.position;

        return result;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        for (float x = 0; x < 40; x+= size)
        {
            for (float y = 0; y < 40; y += size)
            {
                var point = GetNearestPointOnGrid(new Vector2(x, y));
                Gizmos.DrawSphere(point, 0.1f);
            }
        }
    }
}
