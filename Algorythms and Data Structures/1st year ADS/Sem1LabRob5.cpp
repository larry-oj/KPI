#include <iostream>
#include <cstdlib>
#include <windows.h>
using namespace std;
int main()
{
	cout.precision(4);
	SetConsoleOutputCP(1251);
	SetConsoleCP(1251);
	double a = -1, b = 1, c, ac, cb, rnd1, rnd2, res1, res2, lenL, lenR, result1, result2, count1 = 0, count2 = 0;
	do
	{
		count1++;
		c = (a + b) / 2;
		rnd1 = (a + c) / 2;
		rnd2 = (c + b) / 2;
		res1 = exp(-rnd1) + (0.25 * rnd1) - 0.98;
		res2 = exp(-rnd2) + (0.25 * rnd2) - 0.98;
		if (abs(res1) < abs(res2))
		{
			b = c;
		}
		else
		{
			a = c;
		}
	} 
	while (abs(rnd1 - rnd2) >= 0.0001);
	result1 = (a + b) / 2;
	a = -1;
	b = 1;
	c = 0;			
	res1 = 0;
	res2 = 0;
	do
	{
		count2++;
		c = (a + b) / 2;
		ac = (a + c) / 2;
		cb = (c + b) / 2;
		res1 = -4 * exp(ac) + 4 * 0.98;
		res2 = -4 * exp(cb) + 4 * 0.98;
		lenL = abs(abs(ac) - res1);
		lenR = abs(abs(cb) - res2);
		if (lenL > lenR)
		{
			a = c;
		}
		else
		{
			b = c;
		}
	} 
	while (abs(res1 - res2) >= 0.0001);
	result2 = -(a + b) / 2;
	cout << "На відрізку [-1; 1]:" << endl;
	cout << "Методом половинного ділення = " << result1 << ". Кіл-ть ітерацій: " << count1 << endl;
	cout << "Ітераційним методом = " << result2 << ". Кіл-ть ітерацій: " << count2 << endl;
	cout << "Різниця = " << abs(result1 - result2) << endl;
}
