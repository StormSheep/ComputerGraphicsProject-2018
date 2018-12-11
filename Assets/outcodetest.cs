using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class outcodetest {
	
    public List<Vector2> rasterise(Vector2 start, Vector2 end)
    {
        List<Vector2> listOfVector = new List<Vector2>();

        float   dy  = end.y - start.y,
                dx  = end.x - start.x,
                p   = 2 * dy - dx,
                x   = start.x, 
                y   = start.y;

        if (dx < 0)
        {
            return rasterise(end, start);
        }

        if (dy < 0)
        {
            return negateY(rasterise(negateY(start), negateY(end)));
        }

        if (dy > dx)
        {
            return swapXY(rasterise(swapXY(start), swapXY(end)));
        }

        listOfVector.Add(new Vector2(x, y));
        while (x < end.x && y < end.y)
        {
            // X
            x++;

            // Y
            if(p > 0)
            {
                y++;
            }

            // P
            if(p < 0)
            {
                p += 2 * dy;
            }
            else
            {
                p += 2 * (dy - dx);
            }

            listOfVector.Add(new Vector2(x, y));

        }

        return listOfVector;
    }

    private List<Vector2> swapXY(List<Vector2> list)
    {
        List<Vector2> listToReturn = new List<Vector2>();
        foreach (Vector2 v in list)
            listToReturn.Add(swapXY(v));

        return listToReturn;

    }

    private List<Vector2> negateY(List<Vector2> list)
    {
        List<Vector2> listToReturn = new List<Vector2>();
        foreach (Vector2 v in list)
            listToReturn.Add(negateY(v));

        return listToReturn;

    }

    private Vector2 negateY(Vector2 vector)
    {
        Vector2 vectorToReturn = new Vector2(vector.x, -vector.y);
        return vectorToReturn;
    }

    private Vector2 swapXY(Vector2 vector)
    {
        Vector2 vectorToReturn = new Vector2(vector.y, vector.x);
        return vectorToReturn;
    }

    public bool Line_Clip(ref Vector2 start, ref Vector2 end)
    {
        outcode startOutcode = new outcode(start);
        outcode endOutcode = new outcode(end);

        if (startOutcode == new outcode() && endOutcode == new outcode())
        {
            //print("Trivially Accept");
            return true;

        }
        if ((startOutcode & endOutcode) != new outcode())
        {
            //print("Trivially Reject");
            return false;
        }

        start = Line_Intercept(start, end, startOutcode.getEdges());
        end = Line_Intercept(end, start, endOutcode.getEdges());

        return true;

    }

    public Vector2 convertForScreen(Vector2 vector, int width, int height)
    {
        Vector2 vectorResult = new Vector2();
        vectorResult.x = (float)Math.Round(((vector.x + 1) / 2) * (width - 1));
        vectorResult.y = (float)Math.Round(((vector.y + 1) / 2) * (height - 1));
        return vectorResult;
    }


    public Vector2 Line_Intercept(Vector2 start, Vector2 end, Edge[] edgeToClip)
    {
        Vector2 vectorToReturn= start;
        for(int i = 0; i<4; i++)
        {
            Edge edge = edgeToClip[i];
            float m = (end.y - vectorToReturn.y) / (end.x - vectorToReturn.x);
            switch (edge)
            {
                case Edge.UP:
                    vectorToReturn = new Vector2(vectorToReturn.x + (1 - vectorToReturn.y) / m, 1);
                    break;
                case Edge.DOWN:
                    vectorToReturn = new Vector2(-1, vectorToReturn.x + m * (-1 - vectorToReturn.y));
                    break;
                case Edge.LEFT:
                    vectorToReturn = new Vector2(-1, vectorToReturn.y + m*(-1 - vectorToReturn.x));
                    break;
                case Edge.RIGHT:
                    vectorToReturn = new Vector2(vectorToReturn.y + (1 - vectorToReturn.x) / m, 1);
                    break;
                default:
                    break;

            }
        }
        return vectorToReturn;
    }
}
