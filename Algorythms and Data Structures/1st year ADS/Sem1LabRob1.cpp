#include <stdio.h>
#include <math.h>
#include <windows.h>
#include <iostream>

using namespace std;

int main()
{
	int n;
	int a;
	int b;
	int c;

	SetConsoleOutputCP(1251);
	SetConsoleCP(1251);
	while (true) {
		cout << "Номер варіанту = ";
		cin >> n;

		switch (n) {
		case 1:
			a = 3;
			b = 5;
			c = 4;
		case 2:
			a = 8;
			b = 13;
			c = 11;
		case 3:
			a = 5;
			b = 10;
			c = 12;
		}

		double p = (a + b + c) / 2;
		double S = sqrt(p * (p - a) * (p - b) * (p - c));

		if (S < a * a) {
			cout << "\nКвадрат\n\n";
		}
		else { cout << "\nТрикутник\n\n"; }
	}
}