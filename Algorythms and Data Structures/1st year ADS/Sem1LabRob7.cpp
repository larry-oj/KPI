#include <iostream>
#include <fstream>
#include <string>

using namespace std;

struct People
{
	string name;
	string surname;
	string gender;	// 0 - female, 1 - male
	string height;
	string weight;
} Humans[22];

int main()
{
	string path = "D:\\Code\\C++\\LabRob7\\info.txt";

	int ppl_count = 22;

	ifstream file;
	file.open(path);

	while (!file.eof())
	{
		for (int i = 0; i < ppl_count * 5; i++)
		{
			getline(file, Humans[i].name, '-');
			getline(file, Humans[i].surname, '-');
			getline(file, Humans[i].gender, '-');
			getline(file, Humans[i].height, '-');
			getline(file, Humans[i].weight);
		}
	}

	for (int i = 0; i < ppl_count; i++) {
		if (Humans[i].gender == "0" && stoi(Humans[i].weight) > 71) {
			cout << i + 1 << ") " 
				 << Humans[i].surname 
				 << " " << Humans[i].weight << endl;
		}
	}
}

