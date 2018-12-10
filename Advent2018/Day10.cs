using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent2018
{
    public class Day10 : Day
    {
        List<List<int>> Instructions;
        public Day10(string _input) : base(_input)
        {
            Instructions = this.parseListOfIntegerLists(_input);
        }
        public override Tuple<string, string> getResult()
        {
            int Sum = 0;
            int Sum2 = 0;
            List<VectorCoordinate> RescueLights = new List<VectorCoordinate>();
            foreach(List<int> l in Instructions)
            {
                RescueLights.Add(new VectorCoordinate(l[0],l[1],l[2],l[3]));
            }
            long MinArea = 1000000000000;
            long MinX = 1000000;
            long MinY = 1000000;
            long MaxX = -1000000;
            long MaxY = -1000000;
            bool Stop = false;
            while (!Stop)
            {
                MinX = 1000000;
                MinY = 1000000;
                MaxX = -1000000;
                MaxY = -1000000;
                foreach (VectorCoordinate v in RescueLights)
                {
                    v.Iterate();
                    if (v.x < MinX)
                        MinX = v.x;
                    if (v.x > MaxX)
                        MaxX = v.x;
                    if (v.y < MinY)
                        MinY = v.y;
                    if (v.y > MaxY)
                        MaxY = v.y;
                }
                long Area = (MaxX - MinX) * (MaxY - MinY);
                if (Area > MinArea)
                    Stop = true;
                MinArea = Area;
                Sum2++;
            }
            bool[,] ResultGrid = new bool[100, 100];
            // back it up one step
            foreach (VectorCoordinate v in RescueLights)
            {
                v.NegaIterate();
                if (v.IsInPositiveBounds(100+(int)MinX, 100+(int)MinY))
                {
                    ResultGrid[v.x-MinX, v.y-MinY] = true;
                }
            }
            StringBuilder builder = new StringBuilder();
            for(int y= 0; y < 100; y++)
            {
                for (int x = 0; x < 100; x++)
                {
                    if (ResultGrid[x, y])
                        builder.Append("#");
                    else
                        builder.Append(".");
                }
                builder.Append("/r/n");
            }
            return Tuple.Create(builder.ToString(), Sum2.ToString());
        }
        public override string getPartOne()
        {
            throw new NotImplementedException();
        }
        public override string getPartTwo()
        {
            throw new NotImplementedException();
        }
    }
    public class VectorCoordinate : Coordinate
    {
        public int vx;
        public int vy;        
        public VectorCoordinate(int x, int y, int vx, int vy) : base(x, y)
        {
            this.vx = vx;
            this.vy = vy;
        }
        public void Iterate()
        {
            x += vx;
            y += vy;
        }
        public void NegaIterate()
        {
            x -= vx;
            y -= vy;
        }
    }
}
