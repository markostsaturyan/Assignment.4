using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixLibrary
{
    /// <summary>
    /// The matrix class
    /// </summary>

    public class Matrix
    {
        /// <summary>
        /// The matrix field
        /// </summary>
        private readonly double[,] matrix;

        /// <summary>
        /// The lenght of columns
        /// </summary>
        public int LenghtOfColumns
        {
            get
            {
                return matrix.GetLength(0);
            }
        }

        /// <summary>
        /// The lenght of rows
        /// </summary>
        public int LenghtOfRows
        {
            get
            {
                return matrix.GetLength(1);
                //return matrix.Length / LenghtOfColumns;
                
            }
        }

        /// <summary>
        /// Parameter full property for matrix class  
        /// </summary>
        /// <param name="i">The index of row</param>
        /// <param name="j">The index of column</param>
        /// <returns></returns>
        public double this[int i, int j]
        {
            get
            {
                if (i < 0 || i >= LenghtOfColumns || j < 0 || j >= LenghtOfRows)
                {
                    throw new IndexOutOfRangeException();
                }
                return matrix[i, j];
            }
            set
            {
                if (i < 0 || i >= LenghtOfColumns || j < 0 || j >= LenghtOfRows)
                {
                    throw new IndexOutOfRangeException();
                }
                matrix[i, j] = value;


            }
        }

        /// <summary>
        /// Parameterless constructor.
        /// </summary>
        public Matrix()
        {
            matrix = null;
        }

        /// <summary>
        /// Parameterized constructor.
        /// </summary>
        /// <param name="row">The count of row</param>
        /// <param name="column">The count of column</param>
        public Matrix(int row, int column)
        {
            // Create an empty matrix
            matrix = new double[row, column];
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="mat">The matrix</param>
        public Matrix(Matrix mat)
        {
            // Create a new empty matrix
            matrix = new double[mat.LenghtOfColumns, mat.LenghtOfRows];

            // Copying elements
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for(int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = mat[i, j];
                }
            }
        }

        /// <summary>
        /// Matrix addition.
        /// </summary>
        /// <param name="left">The left matrix</param>
        /// <param name="right">The right matrix</param>
        /// <returns></returns>
        public static Matrix operator +(Matrix left, Matrix right)
        {
            // Left matrix is not initialized.
            if (left.matrix == null)
            {
                throw new Exception("Left matrix is not initialized");
            }

            // Right matrix is not initialized.
            if (right.matrix == null)
            {
                throw new Exception("Right matrix is not initialized");
            }

            // Matrix have a other dimensinons.
            if (left.LenghtOfColumns != right.LenghtOfColumns || left.LenghtOfRows != right.LenghtOfRows)
            {
                throw new Exception("Matrixs have a other dimensions");
            }
            
            Matrix addition = new Matrix(left.LenghtOfColumns, left.LenghtOfRows);

            // Addition
            for (int i = 0; i < addition.LenghtOfColumns; i++)
            {
                for (int j = 0; j < addition.LenghtOfRows; j++)
                {
                    addition[i, j] = left[i, j] + right[i, j];
                }
            }
            return addition;
        }

        /// <summary>
        /// Matrix multiplication
        /// </summary>
        /// <param name="left">The left matrix</param>
        /// <param name="right">The right matrix</param>
        /// <returns></returns>
        public static Matrix operator *(Matrix left, Matrix right)
        {
            // Left matrix is not initialized
            if (left.matrix == null)
            {
                throw new Exception("Left matrix is not initialized");
            }

            // Right matrix is not initialized
            if (right.matrix == null)
            {
                throw new Exception("Right matrix is not initialized");
            }

            // Matrix have a other dimensions
            if (left.LenghtOfRows != right.LenghtOfColumns)
            {
                throw new Exception("Matrixs have a other dimensions");
            }

            Matrix MultiMatrix = new Matrix(left.LenghtOfColumns, right.LenghtOfRows);

            // Multiplication
            for (int i = 0; i < left.LenghtOfColumns; i++)
            {
                for (int j = 0; j < right.LenghtOfRows; j++)
                {
                    for (int k = 0; k < right.LenghtOfColumns; k++)
                    {
                        MultiMatrix[i, j] += left[i, k] * right[k, j];
                    }
                }
            }
            return MultiMatrix;
        }

        /// <summary>
        /// Scalar multiplication
        /// </summary>
        /// <param name="number">The number</param>
        /// <param name="mat">The matrix</param>
        /// <returns></returns>
        public static Matrix operator *(double number ,Matrix mat)
        {
            // Matrix in not initialized
            if (mat.matrix == null)
            {
                throw new Exception("Matrix is not initialized");
            }

            Matrix ScalarMult = new Matrix(mat.LenghtOfColumns, mat.LenghtOfRows);

            // Scalar multiplication
            for(int i = 0; i < mat.LenghtOfColumns; i++)
            {
                for (int j = 0; j < mat.LenghtOfRows; j++)
                {
                    ScalarMult[i, j] = mat[i, j] * number;
                }
            }
            return ScalarMult;
        }

        /// <summary>
        /// Inverse matrix
        /// </summary>
        /// <returns></returns>
        public Matrix Inverse()
        {
            // Matrix in not initialized
            if (this.matrix == null)
            {
                throw new Exception("Matrix is not initialized");
            }

            // Matrix is not square
            if (this.LenghtOfColumns != this.LenghtOfRows)
            {
                throw new Exception("Matrix is not square");
            }

            Matrix identity = new Matrix(this.LenghtOfColumns, this.LenghtOfColumns);

            // Set 1 in diagonal
            for (int i = 0; i < identity.LenghtOfColumns; i++)
            {
                identity[i, i] = 1;
            }
            
            // Copying current matrix
            Matrix current = new Matrix(this);


            for (int i = 0; i < current.LenghtOfColumns; i++)
            {
                double curr = current[i, i];

                for (int k = 0; k < current.LenghtOfColumns; k++)
                {
                    current[i, k] /= curr;
                    identity[i, k] /= curr;
                }

                for (int j = 0; j < current.LenghtOfColumns; j++)
                {
                    if (j == i) continue;

                    double currentCoefficient = current[j, i];

                    for (int k = 0; k < current.LenghtOfRows; k++)
                    {
                        current[j, k] -= current[i, k] * currentCoefficient;
                        identity[j, k] -= identity[i, k] * currentCoefficient;
                    }
                }
            }
            return identity;
        }

        /// <summary>
        /// Transpose matrix
        /// </summary>
        /// <returns></returns>
        public Matrix Transpose()
        {
            Matrix transpose = new Matrix(this.LenghtOfRows,this.LenghtOfColumns);

            // Transposing.
            for(int i = 0; i < transpose.LenghtOfColumns; i++)
            {
                for(int j = 0; j < transpose.LenghtOfRows; j++)
                {
                    transpose[i, j] = this[j, i];
                }
            }
            return transpose;
        }

        /// <summary>
        /// Find the smallest element in matrix
        /// </summary>
        /// <returns></returns>
        public double SmallestElem()
        {
            // Matrix is not initialized
            if (this.matrix == null)
            {
                throw new Exception("Matrix is not initialized");
            }

            double smallest = this[0, 0];

            // Find smallest element
            for(int i = 0; i < this.LenghtOfColumns; i++)
            {
                for(int j = 0; j < this.LenghtOfRows; j++)
                {
                    if (smallest > this[i, j])
                    {
                        smallest = this[i, j];
                    }
                }
            }
            return smallest;
        }

        /// <summary>
        /// Find the largest element in matrix
        /// </summary>
        /// <returns></returns>
        public double LargestElem()
        {
            // Matrix is not initialized
            if (this.matrix == null)
            {
                throw new Exception("Matrix is not initialized");
            }

            double largest = this[0, 0];

            // Find largest element
            for (int i = 0; i < this.LenghtOfColumns; i++)
            {

                for(int j = 0; j < this.LenghtOfRows; j++)
                {
                    if (largest < this[i, j])
                    {
                        largest = this[i, j];
                    }
                }
            }
            return largest;
        }

        /// <summary>
        /// Matrix is ortogonale or not.
        /// </summary>
        /// <returns></returns>
        public bool IsOrthogonale()
        {
            // Matrix is not initialized
            if (this.matrix == null)
            {
                throw new Exception("Matrix is not initialized");
            }

            // Multiplicating current matrix with it's transpose matrix
            Matrix mat = this * this.Transpose();

            // Create a new empty matrix for identity
            Matrix identityMatrix = new Matrix(mat.LenghtOfColumns,mat.LenghtOfRows);

            // Set 1 in diagonal
            for(int i=0;i<identityMatrix.LenghtOfColumns;i++)
            {
                identityMatrix[i, i] = 1;
            }

            // Checking matrix is identity or not
            for(int i = 0; i < mat.LenghtOfColumns; i++)
            {
                for(int j = 0; j < mat.LenghtOfRows; j++)
                {
                    if (mat[i, j] != identityMatrix[i, j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Translation
        /// </summary>
        /// <param name="translationVector">Translation size vector</param>
        /// <returns></returns>
        public Matrix Translation(params double[] translationVector)
        {
            // Matrix is not initialized.
            if (this.matrix == null)
            {
                throw new Exception("Matrix is not initialized");
            }

            if (translationVector.Length > this.LenghtOfColumns)
            {
                throw new Exception("Translation vector is greater than row's length of matrix.");
            }

            // Copying current matrix.
            Matrix mat = new Matrix(this);

            // Translation
            for(int i = 0; i < translationVector.Length;i++)
            {
                for(int j = 0; j < mat.LenghtOfRows; j++)
                {
                    mat[i, j] += translationVector[i];
                }
            }
            return mat;
        }

        /// <summary>
        /// 2D rotation
        /// </summary>
        /// <param name="angle">The rotation angle</param>
        /// <returns></returns>
        public Matrix Rotation2D(double angle)
        {
            // Matrix is not initialized.
            if (this.matrix == null)
            {
                throw new Exception("Matrix is not initialized");
            }

            // Checking current matrix 2D or not
            if (this.LenghtOfColumns != 2)
            {
                throw new Exception("Current matrix is not 2D");
            }

            // Special matrix for 2D rotation
            Matrix rotationMatrix = new Matrix(2,2);

            // Initialized the special matrix
            rotationMatrix[0, 0] = Math.Cos(angle);
            rotationMatrix[0, 1] = -Math.Sin(angle);
            rotationMatrix[1, 0] = -rotationMatrix[0, 1];
            rotationMatrix[1, 1] = rotationMatrix[0, 0];
            
            // Rotating and return a new matrix
            return rotationMatrix * this;
        }

        /// <summary>
        /// 3D rotation
        /// </summary>
        /// <param name="angleX">The rotation angle around the X axis</param>
        /// <param name="angleY">The rotation angle around the Y axis</param>
        /// <param name="angleZ">The rotation angle around the Z axis</param>
        /// <returns></returns>
        public Matrix Rotation3D(double angleX = 0, double angleY = 0, double angleZ = 0)
        {
            // Matrix is not initialized.
            if (this.matrix == null)
            {
                throw new Exception("Matrix is not initialized");
            }

            // Current matrix is not 3D.
            if (this.LenghtOfColumns != 3)
            {
                throw new Exception("Current matrix is not 3D");
            }

            Matrix current = new Matrix(this);
            
            // Rotating 
            if (angleX != 0)
            {
                // Creating a special rotation matrix for rotating around X axiz
                Matrix rotationX = new Matrix(3, 3);

                // Initialized the special matrix
                rotationX[0, 0] = 1;
                rotationX[1, 1] = Math.Cos(angleX);
                rotationX[1, 2] = -Math.Sin(angleX);
                rotationX[2, 1] = -rotationX[1, 2];
                rotationX[2, 2] = rotationX[1, 1];

                // Rotating around X axis
                current = rotationX * current;
            }

            if (angleY != 0)
            {
                // Creating a special rotation matrix for rotating around Y axiz
                Matrix rotationY = new Matrix(3, 3);

                // Initialized the special matrix
                rotationY[0, 0] = Math.Cos(angleY);
                rotationY[0, 2] = Math.Sin(angleY);
                rotationY[1, 1] = 1;
                rotationY[2, 0] = -rotationY[0, 2];
                rotationY[2, 2] = rotationY[0, 0];

                // Rotating around Y axis
                current = rotationY * current;
            }

            if(angleZ!=0)
            {
                // Creating a special rotation matrix for rotating around Z axiz
                Matrix rotationZ = new Matrix(3, 3);

                // Initialized the special matrix
                rotationZ[0, 0] = Math.Cos(angleZ);
                rotationZ[0, 1] = -Math.Sin(angleZ);
                rotationZ[1, 0] = -rotationZ[0, 1];
                rotationZ[1, 1] = rotationZ[0, 0];
                rotationZ[2, 2] = 1;

                // Rotating around Z axis
                current = rotationZ * current;

            }
            return current;
        }

        /// <summary>
        /// Scaling
        /// </summary>
        /// <param name="factorsVector">The factors vector</param>
        /// <returns></returns>
        public Matrix  Scaling(params double[] factorsVector)
        {
            // Matrix is not initialized.
            if (this.matrix == null)
            {
                throw new Exception("Matrix is not initialized");
            }

            if (factorsVector.Length > this.LenghtOfColumns)
            {
                throw new Exception("Factor vector is greater than row's length of matrix.");
            }

            Matrix scalMat = new Matrix(this.LenghtOfColumns, this.LenghtOfRows);

            int i;

            for (i = 0; i < factorsVector.Length; i++)
            {
                scalMat[i, i] = factorsVector[i];
            }

            while (i < LenghtOfColumns)
            {
                scalMat[i, i] = 1;
            }

            return scalMat * this;
        }

        public void Print()
        {
            // Matrix is not initialized.
            if (this.matrix == null)
            {
                throw new Exception("Matrix is not initialized");
            }

            for (int i = 0; i < this.LenghtOfColumns; i++)
            {
                for(int j = 0; j < this.LenghtOfRows; j++)
                {
                    Console.Write(this[i, j]);
                    Console.Write("  ");
                }
                Console.Write("\n");
            }
        }
    }
}
