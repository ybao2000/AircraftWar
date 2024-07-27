using System;
using System.Collections.Generic;
using UnityEngine;

public enum DIR
{
    FRONT,
    BACK,
    UP,
    DOWN,
    LEFT,
    RIGHT
}

public class RaySix
{
    public Vector3 offset;
    public float[] distances;

    public Ray front, back, up, down, left, right;

    public Vector3 frontDir, backDir, upDir, downDir, leftDir, rightDir;
    public Color color;
    public RaySix(Vector3 offset, float[] distances, Color color)
    {
        this.offset = offset;
        this.distances = distances;
        this.color = color;
    }
}

public class RaySixDirCollision
{
    private List<RaySix> raySixList = new List<RaySix>();
    private int layerMask = 0;
    public RaySixDirCollision(int layerMask)
    {
        this.layerMask = layerMask;
    }

    public void AddRayLayer(Vector3 offset, float dist, Color color)
    {
        float[] dists = { dist, dist, dist, dist, dist, dist };
        raySixList.Add(new RaySix(offset, dists, color));
    }

    public void SetDistance(int layer, DIR dir, float dist)
    {
        try
        {
            raySixList[layer].distances[(int)dir] = dist;
        }
        catch (Exception e)
        {
            Debug.Log(e);
            throw;
        }
    }

    public void RaySixDirCollisionUpdate(Transform transform, bool isDrawLine = true)
    {
        CreateSixDirRay(transform);
        UpdatePosition(transform);
        if (isDrawLine)
        {
            DrawRayLine(transform);
        }
    }

    private void UpdatePosition(Transform transform)
    {
        foreach (RaySix item in raySixList)
        {
            item.frontDir = transform.TransformDirection(new Vector3(0, 0, 1));
            item.backDir = transform.TransformDirection(new Vector3(0, 0, -1));
            item.upDir = transform.TransformDirection(new Vector3(0, 1, 0));
            item.downDir = transform.TransformDirection(new Vector3(0, -1, 0));
            item.leftDir = transform.TransformDirection(new Vector3(-1, 0, 0));
            item.rightDir = transform.TransformDirection(new Vector3(1, 0, 0));
        }
    }

    private Vector3 centerPos;

    private void CreateSixDirRay(Transform transform)
    {
        foreach (RaySix item in raySixList)
        {
            centerPos = transform.position + transform.TransformDirection(item.offset);
            item.front = new Ray(centerPos, item.frontDir);
            item.back = new Ray(centerPos, item.backDir);
            item.up = new Ray(centerPos, item.upDir);
            item.down = new Ray(centerPos, item.downDir);
            item.left = new Ray(centerPos, item.leftDir);
            item.right = new Ray(centerPos, item.rightDir);
        }
    }

    private void DrawRayLine(Transform transform)
    {
        foreach (RaySix item in raySixList)
        {
            centerPos = transform.position + transform.TransformDirection(item.offset);
            Debug.DrawLine(centerPos, centerPos + item.frontDir * item.distances[0], item.color);
            Debug.DrawLine(centerPos, centerPos + item.backDir * item.distances[1], item.color);
            Debug.DrawLine(centerPos, centerPos + item.upDir * item.distances[2], item.color);
            Debug.DrawLine(centerPos, centerPos + item.downDir * item.distances[3], item.color);
            Debug.DrawLine(centerPos, centerPos + item.leftDir * item.distances[4], item.color);
            Debug.DrawLine(centerPos, centerPos + item.rightDir * item.distances[5], item.color);
        }
    }

    public void SixRaycast(Action<DIR, RaycastHit> success, Action<DIR, RaycastHit> fail)
    {
        FrontRaycast(success, fail);
        BackRaycast(success, fail);
        UpRaycast(success, fail);
        DownRaycast(success, fail);
        LeftRaycast(success, fail);
        RightRaycast(success, fail);
    }

    public void FrontRaycast(Action<DIR, RaycastHit> success, Action<DIR, RaycastHit> fail)
    {
        foreach (RaySix item in raySixList)
        {
            Raycast(item.front, item.distances[0], DIR.FRONT, success, fail);
        }
    }

    public void FrontRaycastToLayer(int layer, Action<DIR, RaycastHit> success, Action<DIR, RaycastHit> fail)
    {
        try
        {
            Raycast(raySixList[layer].front, raySixList[layer].distances[0], DIR.FRONT, success, fail);
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
            throw;
        }
    }

    public void BackRaycast(Action<DIR, RaycastHit> success, Action<DIR, RaycastHit> fail)
    {
        foreach (RaySix item in raySixList)
        {
            Raycast(item.back, item.distances[1], DIR.BACK, success, fail);
        }
    }

    public void BackRaycastToLayer(int layer, Action<DIR, RaycastHit> success, Action<DIR, RaycastHit> fail)
    {
        try
        {
            Raycast(raySixList[layer].back, raySixList[layer].distances[1], DIR.BACK, success, fail);
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
            throw;
        }
    }
    public void UpRaycast(Action<DIR, RaycastHit> success, Action<DIR, RaycastHit> fail)
    {
        foreach (RaySix item in raySixList)
        {
            Raycast(item.up, item.distances[2], DIR.UP, success, fail);
        }
    }

    public void UpRaycastToLayer(int layer, Action<DIR, RaycastHit> success, Action<DIR, RaycastHit> fail)
    {
        try
        {
            Raycast(raySixList[layer].up, raySixList[layer].distances[2], DIR.UP, success, fail);
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
            throw;
        }
    }

    public void DownRaycast(Action<DIR, RaycastHit> success, Action<DIR, RaycastHit> fail)
    {
        foreach (RaySix item in raySixList)
        {
            Raycast(item.down, item.distances[3], DIR.DOWN, success, fail);
        }
    }

    public void DownRaycastToLayer(int layer, Action<DIR, RaycastHit> success, Action<DIR, RaycastHit> fail)
    {
        try
        {
            Raycast(raySixList[layer].down, raySixList[layer].distances[3], DIR.DOWN, success, fail);
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
            throw;
        }
    }

    public void LeftRaycast(Action<DIR, RaycastHit> success, Action<DIR, RaycastHit> fail)
    {
        foreach (RaySix item in raySixList)
        {
            Raycast(item.left, item.distances[4], DIR.LEFT, success, fail);
        }
    }

    public void LeftRaycastToLayer(int layer, Action<DIR, RaycastHit> success, Action<DIR, RaycastHit> fail)
    {
        try
        {
            Raycast(raySixList[layer].left, raySixList[layer].distances[4], DIR.LEFT, success, fail);
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
            throw;
        }
    }

    public void RightRaycast(Action<DIR, RaycastHit> success, Action<DIR, RaycastHit> fail)
    {
        foreach (RaySix item in raySixList)
        {
            Raycast(item.right, item.distances[5], DIR.RIGHT, success, fail);
        }
    }
    public void RightRaycastToLayer(int layer, Action<DIR, RaycastHit> success, Action<DIR, RaycastHit> fail)
    {
        try
        {
            Raycast(raySixList[layer].right, raySixList[layer].distances[5], DIR.RIGHT, success, fail);
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
            throw;
        }
    }

    public void Raycast(Ray ray, float distance, DIR dir, Action<DIR, RaycastHit> success, Action<DIR, RaycastHit> fail)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, distance, layerMask))
        {
            if (success != null) success(dir, hit);
        }
        else
        {
            if (fail != null) fail(dir, hit);
        }
    }
}