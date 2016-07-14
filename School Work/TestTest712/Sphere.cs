using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTest712
{
    public class Sphere
    {
        double circumference;
        float radius;
        float diameter;
        double volume;
        double surfaceArea;

        public Sphere(float r)
        {
            this.radius = r;
            this.diameter = r * 2;
            this.volume = (4 * 3.14 * (Math.Pow(r, 3)))/3;
            this.circumference = 2 * 3.14 * r;
            this.surfaceArea = 4 * 3.14*(Math.Pow(r, 2));
        }
        public void printValues() 
        {
            Console.WriteLine(diameter);
            Console.WriteLine(circumference);
            Console.WriteLine(surfaceArea);
            Console.WriteLine(volume);
            Console.WriteLine(4 * 3.14 * Math.Pow(this.radius, 3));
/*---------------this stupid stuff does stupid stuff lol---------------------------------------*/
/*---------------------roger is an idiot <3-------------------------------------*/
        }
    }
}
