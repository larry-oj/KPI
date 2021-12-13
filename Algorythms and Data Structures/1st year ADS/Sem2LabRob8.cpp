#include <stdio.h>
#include <limits.h>
#include <iostream>
#define V 6
  

using namespace std;


int MinimalDistance(int dist[], bool sptSet[]);
void PrintPath(int parent[], int j);
void Print(int dist[], int n, int parent[]);
void Dijkstra(int graph[V][V], int src);


int main()
{
    int graph[V][V] = {
        {0, 10, 5, 0, 0, 0},
        {0, 0, 0, 0, 2, 5},
        {0, 0, 0, 5, 8, 0},
        {0, 0, 0, 0, 5, 0},
        {0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 5, 0}
    };
  
    Dijkstra(graph, 0);
    return 0;
}


int MinimalDistance(int dist[], bool sptSet[])
{
    int min = INT_MAX, min_index;
  
    for (int v = 0; v < V; v++)
    {
        if (sptSet[v] == false && dist[v] <= min)
        {
            min = dist[v], min_index = v;
        }
    }
        
    return min_index;
}
  

void PrintPath(int parent[], int j)
{
    if (parent[j] == -1) return;
  
    PrintPath(parent, parent[j]);
    
    string tmp;
    switch(j)
    {
        case 0:
            tmp = "a";
            break;

        case 1:
            tmp = "b";
            break;

        case 2:
            tmp = "c";
            break;

        case 3:
            tmp = "d";
            break;

        case 4:
            tmp = "e";
            break;

        case 5:
            tmp = "f";
            break;
    }

    cout << " -> " << tmp;
}
  

void Print(int dist[], int n, int parent[])
{
    string src = "a";
    cout << "Vertex\t\t Distance \tPath";
    for (int i = 1; i < V; i++)
    {
        string tmp;
        switch(i)
        {
            case 0:
                tmp = "a";
                break;

            case 1:
                tmp = "b";
                break;

            case 2:
                tmp = "c";
                break;

            case 3:
                tmp = "d";
                break;

            case 4:
                tmp = "e";
                break;

            case 5:
                tmp = "f";
                break;
        }
        cout << endl << src << " -> " << tmp << " \t\t " << dist[i] << "\t\t" << src;
        PrintPath(parent, i);
    }
}
  

void Dijkstra(int graph[V][V], int src)
{
    int dist[V]; 

    bool sptSet[V];

    int parent[V];

    for (int i = 0; i < V; i++)
    {
        parent[0] = -1;
        dist[i] = INT_MAX;
        sptSet[i] = false;
    }
  
    dist[src] = 0;
  
    for (int count = 0; count < V - 1; count++)
    {
        int u = MinimalDistance(dist, sptSet);
  
        sptSet[u] = true;
  
        for (int v = 0; v < V; v++)
        {
            if (!sptSet[v] && graph[u][v] && dist[u] + graph[u][v] < dist[v])
            {
                parent[v] = u;
                dist[v] = dist[u] + graph[u][v];
            }           
        }
    }
  
    Print(dist, V, parent);
}
