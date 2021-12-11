#include <stdio.h>
#include <math.h>
#include <windows.h>
#include <iostream>
#include <vector>

int main()
{
	std::vector<int> A = { -2, -3 };
	std::vector<int> B = { -2, 2 };
	std::vector<int> C = { 4, 2 };
	std::vector<int> D = { 4, -3 };

	float height = B[1] - A[1];
	float h_med = height / 2;
	float width = C[0] - B[0];
	float w_med = width / 2;

	int R;
	int coord1;
	int coord2;

	std::cout << "R = ";
	std::cin >> R;

	std::cout << "X = ";
	std::cin >> coord1;

	std::cout << "Y = ";
	std::cin >> coord2;

	SetConsoleOutputCP(1251);
	SetConsoleCP(1251);

	if ((R * 2) <= height && (R * 2) <= width)
	{
		if ((coord1 + R) <= h_med || coord2 + R <= w_med)
		{
			std::cout << "\nÒàê" << std::endl;
		}
		else
		{
			std::cout << "\nÍ³" << std::endl;
		}
	}
	else
	{
		std::cout << "\nÍ³" << std::endl;
	}
}