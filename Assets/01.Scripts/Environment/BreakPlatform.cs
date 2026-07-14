using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class BreakPlatform : MonoBehaviour
{
    [SerializeField] private float breakDelay = 0.4f;
    [SerializeField] private float respawnTime = 2f;

    private Tilemap tilemap;

    private Dictionary<Vector3Int, TileBase> breakingTiles = new Dictionary<Vector3Int, TileBase>();

    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Bounds bounds = collision.collider.bounds;
            Vector3 footPos = new Vector3(bounds.center.x, bounds.min.y - 0.02f, 0f);
            Vector3Int cellPos = tilemap.WorldToCell(footPos);

            if (!tilemap.HasTile(cellPos))
                return;
            
            if (breakingTiles.ContainsKey(cellPos))
                return;

            StartCoroutine(BreakTile(cellPos));
        }
    }

    IEnumerator BreakTile(Vector3Int pos)
    {
        TileBase originalTile = tilemap.GetTile(pos);

        breakingTiles.Add(pos, originalTile);

        yield return new WaitForSeconds(breakDelay);

        tilemap.SetTile(pos, null);
        tilemap.RefreshTile(pos);

        yield return new WaitForSeconds(respawnTime);

        tilemap.SetTile(pos, originalTile);
        tilemap.RefreshTile(pos);

        breakingTiles.Remove(pos);
    }
}
