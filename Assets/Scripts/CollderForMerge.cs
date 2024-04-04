using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderForMerge : MonoBehaviour
{
    [SerializeField] private TileState[] tileStates;
    [SerializeField] private Tile basketTile;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BallCollider"))
        {
            basketTile.SetState(tileStates[1]);
        }
    }
}
