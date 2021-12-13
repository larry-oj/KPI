#include <iostream>
#include <math.h>
#include <windows.h>


using namespace std;


class Matrix
{
    public:
        int matrix[4][4];

        void Fill();
        void Print();
        void Max();

        Matrix operator +(Matrix);
        Matrix operator -(Matrix);
        Matrix operator *(Matrix);
        void operator /=(int);
        void operator +=(int);
        void operator -=(int);
        bool operator !=(Matrix);

        Matrix();
        Matrix(int[4][4]);
        ~Matrix();
};


void Matrix::Fill()
{
    for(int row = 0; row < 4; row++)
    {
        for(int column = 0; column < 4; column++)
        {
            cout << "Enter element a" << row + 1 << column + 1 << ": ";
            cin >> matrix[row][column];
        }
    }
}

void Matrix::Print()
{
    for(int row = 0; row < 4; row++)
    {
        for(int column = 0; column < 4; column++)
        {
            cout << matrix[row][column] << "\t";
        }
        cout << endl;
    }
}

void Matrix::Max()
{
    int tmp = INT_MIN;
    int r;
    int c;
    for(int row = 1; row <= 4; row++)
    {
        for(int column = 1; column <= 4; column++)
        {
            if (matrix[row][column] > tmp)
            {
                tmp = matrix[row][column];
                r = row;
                c = column;
            } 
        }
    }
    cout << "Max element is: a" << r << c << " = " << tmp << endl;
}


Matrix Matrix::operator +(Matrix m2)
{
    Matrix result;
    for(int row = 0; row < 4; row++)
    {
        for(int column = 0; column < 4; column++)
        {
            result.matrix[row][column] = matrix[row][column] + m2.matrix[row][column];
        }
    }
    return result;
}

Matrix Matrix::operator -(Matrix m2)
{
    Matrix result;
    for(int row = 0; row < 4; row++)
    {
        for(int column = 0; column < 4; column++)
        {
            result.matrix[row][column] = matrix[row][column] - m2.matrix[row][column];
        }
    }
    return result;
}

Matrix Matrix::operator *(Matrix m2)
{
    Matrix result;
    for(int row = 0; row < 4; row++)
    {
        for(int column = 0; column < 4; column++)
        {
            result.matrix[row][column] = 0;
        }
    }
        
    for(int row = 0; row < 4; row++)
    {
        for(int column = 0; column < 4; column++)
        {
            for(int i = 0; i < 4; i++)
            {
                result.matrix[row][column] += matrix[row][i] * m2.matrix[i][column];
            }
        }
    }

    return result;
}

void Matrix::operator /=(int num)
{
    for(int row = 0; row < 4; row++)
    {
        for(int column = 0; column < 4; column++)
        {
            matrix[row][column] /= num;
        }
    }
}

void Matrix::operator +=(int num)
{
    for(int row = 0; row < 4; row++)
    {
        for(int column = 0; column < 4; column++)
        {
            matrix[row][column] += num;
        }
    }
}

void Matrix::operator -=(int num)
{
    for(int row = 0; row < 4; row++)
    {
        for(int column = 0; column < 4; column++)
        {
            matrix[row][column] -= num;
        }
    }
}

bool Matrix::operator !=(Matrix m2)
{
    for(int row = 0; row < 4; row++)
    {
        for(int column = 0; column < 4; column++)
        {
            if(matrix[row][column] != m2.matrix[row][column]) return true;
        }
    }
    return false;
}

Matrix::Matrix(int m[4][4])
{
    for(int row = 0; row < 4; row++)
    {
        for(int column = 0; column < 4; column++)
        {
            matrix[row][column] = m[row][column];
        }
    }
}

Matrix::Matrix() { };

Matrix::~Matrix() { };


int main()
{
    cout << "Fill the first matrix:" << endl;
    Matrix m1;
    m1.Fill();
    cout << endl;

    cout << "Fill the second matrix:" << endl;
    Matrix m2;
    m2.Fill();
    cout << endl << endl;
    
    cout << "Result matrixes are:" << endl;
    cout << "Matrix one:" << endl;
    m1.Print();
    m1.Max();
    cout << "Matrix two:" << endl;
    m2.Print();
    m2.Max();
    cout << endl;

    int filler[4][4] = {
        {0, 0, 0, 0},
        {0, 0, 0, 0},
        {0, 0, 0, 0},
        {0, 0, 0, 0}
    };
    Matrix m3(filler);
    int num;

    while(true)
    {
        {
            cout << "Actions:" << endl;
            cout << "1 - Add matrixes" << endl;
            cout << "2 - Subtract matrixes" << endl;
            cout << "3 - Multiply matrixes" << endl;
            cout << "4 - Devide matrix one by a number" << endl;
            cout << "5 - Increase matrix two by a number" << endl;
            cout << "6 - Decrease matrix one by a number" << endl;
            cout << "7 - Compare matrixes" << endl;
        }

        int choice;
        cout << "Coose action: ";
        cin >> choice;

        switch(choice)
        {
            case 1:
                {
                    m3 = m1 + m2;
                    cout << "Result:" << endl;
                    m3.Print();
                    cout << endl;
                }
                break;

            case 2:
                {
                    m3 = m1 - m2;
                    cout << "Result:" << endl;
                    m3.Print();
                    cout << endl;
                }
                break;

            case 3:
                {
                    m3 = m1 * m2;
                    cout << "Result:" << endl;
                    m3.Print();
                    cout << endl;
                }
                break;

            case 4:
                {
                    cout << "Enter the number: ";
                    cin >> num;
                    m1 /= num;
                    cout << "Result:" << endl;
                    m1.Print();
                    cout << endl;
                }
                break;

            case 5:
                {
                    cout << "Enter the number: ";
                    cin >> num;
                    m2 += num;
                    cout << "Result:" << endl;
                    m2.Print();
                    cout << endl;
                }
                break;

            case 6:
                {
                    cout << "Enter the number: ";
                    cin >> num;
                    m1 -= num;
                    cout << "Result:" << endl;
                    m1.Print();
                    cout << endl;
                }
                break;

            case 7:
                {
                    if (m1 != m2)
                    {
                        cout << "Matrixes are not equal!" << endl;
                    }
                    else
                    {
                        cout << "Matrixes are equal!" << endl;
                    }
                    cout << endl;
                }
                break;
        }
    }

    return 0;
}
