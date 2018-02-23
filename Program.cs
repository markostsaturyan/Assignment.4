using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatrixLibrary;

namespace MatrixOperations
{
    class Program
    {
        static void Main(string[] args)
        {
            Matrix matrix = new Matrix(3, 3);

            matrix[0, 0] = 2.0 / 3;
            matrix[0, 1] = 1.0 / 3;
            matrix[0, 2] = 2.0 / 3;
            matrix[1, 0] = -2.0 / 3;
            matrix[1, 1] = 2.0 / 3;
            matrix[1, 2] = 1.0 / 3;
            matrix[2, 0] = 1.0 / 3;
            matrix[2, 1] = 2.0 / 3;
            matrix[2, 2] = -2.0 / 3;

            matrix.Print();


            Console.WriteLine("Ortogonal or not?");
            Console.WriteLine(matrix.IsOrthogonale());

            Random random = new Random();

            Matrix otherMatrix = new Matrix(4, 3);

            for(int i = 0; i < otherMatrix.LenghtOfColumns; i++)
            {
                for(int j = 0; j < otherMatrix.LenghtOfRows; j++)
                {
                    otherMatrix[i, j] = random.NextDouble();
                }
            }

            Console.WriteLine("Random matrix");
            otherMatrix.Print();

            Console.WriteLine("Transposing random matrix");
            otherMatrix = otherMatrix.Transpose();
            otherMatrix.Print();

            Console.WriteLine("Multiplicate");
            matrix = matrix * otherMatrix;
            matrix.Print();

            Console.WriteLine("Addition");
            matrix = matrix + otherMatrix;
            matrix.Print();

            Console.WriteLine("Scalar Multiplicate");
            matrix = 5*matrix;
            matrix.Print();

            Console.WriteLine("Translation");
            matrix = matrix.Translation(1, 2, 3);
            matrix.Print();

            Console.WriteLine("Smallest element");
            Console.WriteLine(matrix.SmallestElem());

            Console.WriteLine("Largest element");
            Console.WriteLine(matrix.LargestElem());

            Console.WriteLine("Rotation");
            matrix = matrix.Rotation3D(90, 30, 60);
            matrix.Print();

            Console.Read();
        }
    }
}
