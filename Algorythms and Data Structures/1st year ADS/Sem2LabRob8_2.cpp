#include <iostream>
#include <string>
#define V 6
#define INF 99999
 

using namespace std;
 

void Floyd(int graph[][V]);
void Print(int dist[][V]);



int main()
{
    int graph[V][V] = {
        {0, 10, 5, INF, INF, INF},
        {INF, 0, INF, INF, 2, 5},
        {INF, INF, 0, 5, 8, INF},
        {INF, INF, INF, 0, 5, INF},
        {INF, INF, INF, INF, 0, INF},
        {INF, INF, INF, INF, 5, 0}
    };
 
    // Print the solution
    Floyd(graph);
    return 0;
}


void Floyd(int graph[][V])
{
    int dist[V][V], i, j, k;
 
    for (i = 0; i < V; i++)
    {
        for (j = 0; j < V; j++)
        {
            dist[i][j] = graph[i][j];
        }
    }
        
    for (k = 0; k < V; k++)
    {
        for (i = 0; i < V; i++)
        {
            for (j = 0; j < V; j++)
            {
                if (dist[i][k] + dist[k][j] < dist[i][j])
                {
                    dist[i][j] = dist[i][k] + dist[k][j];       
                }
            }
        }
    }
 
    Print(dist);
}
 

void Print(int dist[][V])
{
    cout << "\ta\tb\tc\td\te\tf" << endl << endl;
    for (int i = 0; i < V; i++)
    {
        switch(i)
        {
            case 0:
                cout << "a\t";
                break;

            case 1:
                cout << "b\t";
                break;

            case 2:
                cout << "c\t";
                break;

            case 3:
                cout << "d\t";
                break;

            case 4:
                cout << "e\t";
                break;

            case 5:
                cout << "f\t";
                break;
        }

        for (int j = 0; j < V; j++)
        {
            if (dist[i][j] == INF)
            {
                cout << "inf" << "\t";
            }              
            else
            {
                cout << dist[i][j] << "\t";
            }
        }
        cout<<endl;
    }
}