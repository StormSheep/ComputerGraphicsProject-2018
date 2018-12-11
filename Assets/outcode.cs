using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class outcode  {

    public bool up, 
                down, 
                left, 
                right;
    public Vector2 vector;

    public outcode ( Vector2 v)
    {
        up = v.y > 1;
        down = v.y < -1;
        left = v.x < -1;
        right = v.x > 1;

        vector = v;
    }

    public outcode()
    {
        up = false;
        down = false;
        left = false;
        right = false;
    }

    public outcode(bool up, bool down, bool left, bool right)
    {
        this.up = up;
        this.down = down;
        this.left = left;
        this.right = right;
    }

    public static bool operator ==(outcode out1, outcode out2)
    {
        return (out1.down == out2.down 
                && out1.left == out2.left 
                && out1.up == out2.up 
                && out1.right == out2.right);
    }

    public static bool operator !=(outcode out1, outcode out2)
    {
        return (out1.down != out2.down 
                || out1.left != out2.left 
                || out1.up != out2.up 
                || out1.right != out2.right);
    }

    public static outcode operator &(outcode out1, outcode out2)
    {
        return (
            new outcode(out1.up && out2.up, out1.down && out2.down, out1.left && out2.left, out1.right && out2.right)
        );
    }
    
    public Edge[] getEdges()
    {
        int i = 0;
        Edge[] edges = new Edge[4];
        if(up)
        {
            edges[i] = Edge.UP;
            i++;
        }
        if (down)
        {
            edges[i] = Edge.DOWN;
            i++;
        }
        if (left)
        {
            edges[i] = Edge.LEFT;
            i++;
        }
        if (right)
        {
            edges[i] = Edge.RIGHT;
            i++;
        }

        return edges;
    }
}

public enum Edge
{
    NONE,
    UP,
    DOWN,
    LEFT,
    RIGHT
}
