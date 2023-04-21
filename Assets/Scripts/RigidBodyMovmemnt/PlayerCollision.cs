using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public float FloorCheckRadius;
    public float bottomOffset;
    public float WallCheckRadius;
    public float frontOffset;
    public float RoofCheckRadius;
    public float upOffset;

    public float LedgeGrabForwardPos;
    public float LedgeGrabUpwardsPos;
    public float LedgeGrabDistance;

    public LayerMask FloorLayers;
    public LayerMask WallLayers;
    public LayerMask RoofLayers;
    public LayerMask LedgeGrabLayerws;

    public bool CheckFloor(Vector3 Dir)
    {
        Vector3 pos = transform.position + (Dir * bottomOffset);
        Collider[] ColHit = Physics.OverlapSphere(pos, FloorCheckRadius, FloorLayers);
        if (ColHit.Length > 0)
            return true;
        return false;
    }

    public bool CheckWalls(Vector3 Dir)
    {
        Vector3 pos = transform.position + (Dir * bottomOffset);
        Collider[] ColHit = Physics.OverlapSphere(pos, WallCheckRadius, WallLayers);
        if (ColHit.Length > 0)
            return true;
        return false;
    }
}
