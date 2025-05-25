using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
class Edge
{
    public int From, To, Weight;
    public Edge(int from, int to, int weight)
    {
        From = from;
        To = to;
        Weight = weight;
    }
}
class DSU
{
    int[] parent, rank;
    public DSU(int size)
    {
        parent = new int[size];
        rank = new int[size];
        for (int i=0; i<size; i++) parent[i]=i;
    }
    public int Find(int x)
    {
        if (parent[x]!=x) parent[x]=Find(parent[x]);
        return parent[x];
    }
    public bool Union(int x, int y)
    {
        int rx=Find(x), ry=Find(y);
        if (rx==ry) return false;
        if (rank[rx]<rank[ry]) parent[rx]=ry;
        else
        {
            parent[ry]=rx;
            if (rank[rx]==rank[ry]) rank[rx]++;
        }
        return true;
    }
}
class Program
{
    static void Main()
    {
        var lines=File.ReadAllLines("input.txt");
        int n=int.Parse(lines[0]);
        int m=int.Parse(lines[1]);
        var edges=new List<Edge>();
        for (int i=2;i<2+m;i++)
        {
            var p=lines[i].Split();
            edges.Add(new Edge(int.Parse(p[0])-1,int.Parse(p[1])-1,int.Parse(p[2])));
        }

        var mst=new List<Edge>();
        int totalWeight=0;
        var dsu=new DSU(n);
        
        foreach(var e in edges.OrderBy(e=>e.Weight))
            if(dsu.Union(e.From,e.To))
            {
                totalWeight+=e.Weight;
                mst.Add(e);
            }
        bool connected=true;
        int root=dsu.Find(0);
        for(int i=1;i<n;i++)
            if(dsu.Find(i)!=root){connected=false;break;}
            using(var writer=new StreamWriter("output.txt"))
        {
            if(connected)
            {
                writer.WriteLine($"Минимальная утечка информации = {totalWeight}");
                foreach(var e in mst)
                    writer.WriteLine($"{e.From+1} {e.To+1} {e.Weight}");
            }
            else
                writer.WriteLine(-1);
        }
    }
}
