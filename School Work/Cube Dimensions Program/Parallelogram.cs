using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cube_Dimensions_Program
{
    public class Parallelogram
    {
        /*Define variables length, width, height, surface area, and volume.*/
        double length;
        double width;
        double height;
        double surfaceArea;
        double volume;

        public Parallelogram(float l, float w, float h) 
        {
            /*Define a function that will manipulate arguments l, w, and h for surface area and volume.*/
            this.length = l;
            this.width = w;
            this.height = h;
            /*Below is the surface area and volume of a cuboid. A cuboid is a 3D quadrilateral like a
            parallelipiped.*/
            this.surfaceArea = 2*(l*w)+2*(l*h)+2*(w*h);
            this.volume = l * w * h;
        }

        public void printValues() 
        {
            Console.WriteLine("Your length is " + length);
            Console.WriteLine("Your width is " + width);
            Console.WriteLine("Your height is " + height);
            Console.WriteLine("The surface area is " + surfaceArea);
            Console.WriteLine("The volume is " + volume);
        }
    }
}
