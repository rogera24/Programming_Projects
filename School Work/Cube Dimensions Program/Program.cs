using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*Today, we're going to make a program that runs off the command prompt and will calculate
 the surface area and volume of a parallelogram, given its length, width and height. These are inputted
 into the command prompt interface and the program will parse these values to calculate the SA and V.*/
namespace Cube_Dimensions_Program
{
    class Program
    {
        static void Main(string[] args)
        {
            Parallelogram outputParallelogram = new Parallelogram(5,3,2);
            outputParallelogram.printValues();
                            /*Define the class that will type a message after inputting values.*/
        }
    }
}
