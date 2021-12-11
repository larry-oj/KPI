#include <iostream>
#include <windows.h>
using namespace std;

double factorial(double num) {
	double res = 1;
	for (int i = 1; i <= num; ++i)
	{
		 res *= i;
	}
	return res;
}

int main()
{
	SetConsoleCP(1251);
	SetConsoleOutputCP(1251);
	cout.precision(10);

	double x1 = 0.3, x2 = 3, m = 40, k = 1, precise, result = 0, addition, _error;

	double x_diff = x2 - x1;
	x_diff /= m;

	double x_loop = x1;

	for (int i = 0; i < m; i++) {
		precise = sin(x_loop);

		do {
			addition = pow(-1, k - 1) * (pow(x_loop, 2 * k - 1)) / (factorial(2 * k - 1));
			k++;
			result += addition;
		} 
		while (result != result + addition);

		_error = abs(precise - result);

		cout << "При х = " << x_loop << endl;
		cout << "Точне значення f(x) = " << precise << endl;
		cout << "Наближене значення = " << result << endl;
		cout << "Похибка наближеного значення = " << _error << endl << endl;

		x_loop += x_diff;
		result = 0;
		addition = 0;
		k = 1;
	};
}
