using ShortestRouteOptimizer.Service;

namespace ShortestRouteOptimize.Service.Tests;

[TestFixture]
[TestOf(typeof(Graph<string>))]
public class Tests
{ 
    private Graph<string> _graph = default!;

    [SetUp]
    public void Init()
    {
        _graph = new(["A", "B", "C", "D", "E", "F", "G", "H", "I"]);
        _graph.AddBidirectionalEdge("A", "B", 4);
        _graph.AddBidirectionalEdge("A", "C", 6);
        _graph.AddBidirectionalEdge("B", "F", 2);
        _graph.AddBidirectionalEdge("C", "D", 8);
        _graph.AddBidirectionalEdge("D", "E", 4);
        _graph.AddBidirectionalEdge("D", "G", 1);
        _graph.AddEdge("E", "B", 2);
        _graph.AddBidirectionalEdge("E", "F", 3);
        _graph.AddBidirectionalEdge("E", "I", 8);
        _graph.AddBidirectionalEdge("F", "G", 4);
        _graph.AddBidirectionalEdge("F", "H", 6);
        _graph.AddBidirectionalEdge("G", "H", 5);
        _graph.AddBidirectionalEdge("G", "I", 5);
    }
    
    [Test]
    public void Test_Source_A()
    {
        var dijkstra = _graph.Dijkstra("A");
        Assert.That(dijkstra["A"].Distance, Is.EqualTo(0));
        Assert.That(dijkstra["A"].Path, Is.EqualTo(new List<string>(["A"])));
        
        Assert.That(dijkstra["B"].Distance, Is.EqualTo(4));
        Assert.That(dijkstra["B"].Path, Is.EqualTo(new List<string>(["A", "B"])));
        
        Assert.That(dijkstra["C"].Distance, Is.EqualTo(6));
        Assert.That(dijkstra["C"].Path, Is.EqualTo(new List<string>(["A", "C"])));

        Assert.That(dijkstra["D"].Distance, Is.EqualTo(11));
        Assert.That(dijkstra["D"].Path, Is.EqualTo(new List<string>(["A", "B", "F", "G", "D"])));
        
        Assert.That(dijkstra["E"].Distance, Is.EqualTo(9));
        Assert.That(dijkstra["E"].Path, Is.EqualTo(new List<string>(["A", "B", "F", "E"])));
        
        Assert.That(dijkstra["F"].Distance, Is.EqualTo(6));
        Assert.That(dijkstra["F"].Path, Is.EqualTo(new List<string>(["A", "B", "F"])));

        Assert.That(dijkstra["G"].Distance, Is.EqualTo(10));
        Assert.That(dijkstra["G"].Path, Is.EqualTo(new List<string>(["A", "B", "F", "G"])));

        Assert.That(dijkstra["H"].Distance, Is.EqualTo(12));
        Assert.That(dijkstra["H"].Path, Is.EqualTo(new List<string>(["A", "B", "F", "H"])));

        Assert.That(dijkstra["I"].Distance, Is.EqualTo(15));
        Assert.That(dijkstra["I"].Path, Is.EqualTo(new List<string>(["A", "B", "F", "G", "I"])));
    }
    
    [Test]
    public void Test_Source_B()
    {
        var dijkstra = _graph.Dijkstra("B");
        Assert.That(dijkstra["B"].Distance, Is.EqualTo(0));
        Assert.That(dijkstra["B"].Path, Is.EqualTo(new List<string>(["B"])));

        Assert.That(dijkstra["A"].Distance, Is.EqualTo(4));
        Assert.That(dijkstra["A"].Path, Is.EqualTo(new List<string>(["B", "A"])));

        Assert.That(dijkstra["C"].Distance, Is.EqualTo(10));
        Assert.That(dijkstra["C"].Path, Is.EqualTo(new List<string>(["B", "A", "C"])));

        Assert.That(dijkstra["D"].Distance, Is.EqualTo(7));
        Assert.That(dijkstra["D"].Path, Is.EqualTo(new List<string>(["B", "F", "G", "D"])));

        Assert.That(dijkstra["E"].Distance, Is.EqualTo(5));
        Assert.That(dijkstra["E"].Path, Is.EqualTo(new List<string>(["B", "F", "E"])));

        Assert.That(dijkstra["F"].Distance, Is.EqualTo(2));
        Assert.That(dijkstra["F"].Path, Is.EqualTo(new List<string>(["B", "F"])));

        Assert.That(dijkstra["G"].Distance, Is.EqualTo(6));
        Assert.That(dijkstra["G"].Path, Is.EqualTo(new List<string>(["B", "F", "G"])));

        Assert.That(dijkstra["H"].Distance, Is.EqualTo(8));
        Assert.That(dijkstra["H"].Path, Is.EqualTo(new List<string>(["B", "F", "H"])));

        Assert.That(dijkstra["I"].Distance, Is.EqualTo(11));
        Assert.That(dijkstra["I"].Path, Is.EqualTo(new List<string>(["B", "F", "G", "I"])));
    }
    
    [Test]
    public void Test_Source_C()
    {
        var dijkstra = _graph.Dijkstra("C");
        Assert.That(dijkstra["C"].Distance, Is.EqualTo(0));
        Assert.That(dijkstra["C"].Path, Is.EqualTo(new List<string>(["C"])));

        Assert.That(dijkstra["A"].Distance, Is.EqualTo(6));
        Assert.That(dijkstra["A"].Path, Is.EqualTo(new List<string>(["C", "A"])));

        Assert.That(dijkstra["B"].Distance, Is.EqualTo(10));
        Assert.That(dijkstra["B"].Path, Is.EqualTo(new List<string>(["C", "A", "B"])));

        Assert.That(dijkstra["D"].Distance, Is.EqualTo(8));
        Assert.That(dijkstra["D"].Path, Is.EqualTo(new List<string>(["C", "D"])));

        Assert.That(dijkstra["E"].Distance, Is.EqualTo(12));
        Assert.That(dijkstra["E"].Path, Is.EqualTo(new List<string>(["C", "D", "E"])));

        Assert.That(dijkstra["F"].Distance, Is.EqualTo(12));
        Assert.That(dijkstra["F"].Path, Is.EqualTo(new List<string>(["C", "A", "B", "F"])));

        Assert.That(dijkstra["G"].Distance, Is.EqualTo(9));
        Assert.That(dijkstra["G"].Path, Is.EqualTo(new List<string>(["C", "D", "G"])));

        Assert.That(dijkstra["H"].Distance, Is.EqualTo(14));
        Assert.That(dijkstra["H"].Path, Is.EqualTo(new List<string>(["C", "D", "G", "H"])));

        Assert.That(dijkstra["I"].Distance, Is.EqualTo(14));
        Assert.That(dijkstra["I"].Path, Is.EqualTo(new List<string>(["C", "D", "G", "I"])));
    }
    
    [Test]
    public void Test_Source_D()
    {
        var dijkstra = _graph.Dijkstra("D");
        Assert.That(dijkstra["D"].Distance, Is.EqualTo(0));
        Assert.That(dijkstra["D"].Path, Is.EqualTo(new List<string>(["D"])));

        Assert.That(dijkstra["A"].Distance, Is.EqualTo(10));
        Assert.That(dijkstra["A"].Path, Is.EqualTo(new List<string>(["D", "E", "B", "A"])));

        Assert.That(dijkstra["B"].Distance, Is.EqualTo(6));
        Assert.That(dijkstra["B"].Path, Is.EqualTo(new List<string>(["D", "E", "B"])));

        Assert.That(dijkstra["C"].Distance, Is.EqualTo(8));
        Assert.That(dijkstra["C"].Path, Is.EqualTo(new List<string>(["D", "C"])));

        Assert.That(dijkstra["E"].Distance, Is.EqualTo(4));
        Assert.That(dijkstra["E"].Path, Is.EqualTo(new List<string>(["D", "E"])));

        Assert.That(dijkstra["F"].Distance, Is.EqualTo(5));
        Assert.That(dijkstra["F"].Path, Is.EqualTo(new List<string>(["D", "G", "F"])));

        Assert.That(dijkstra["G"].Distance, Is.EqualTo(1));
        Assert.That(dijkstra["G"].Path, Is.EqualTo(new List<string>(["D", "G"])));

        Assert.That(dijkstra["H"].Distance, Is.EqualTo(6));
        Assert.That(dijkstra["H"].Path, Is.EqualTo(new List<string>(["D", "G", "H"])));

        Assert.That(dijkstra["I"].Distance, Is.EqualTo(6));
        Assert.That(dijkstra["I"].Path, Is.EqualTo(new List<string>(["D", "G", "I"])));
    }
    
    [Test]
    public void Test_Source_E()
    {
        var dijkstra = _graph.Dijkstra("E");
        Assert.That(dijkstra["E"].Distance, Is.EqualTo(0));
        Assert.That(dijkstra["E"].Path, Is.EqualTo(new List<string>(["E"])));

        Assert.That(dijkstra["A"].Distance, Is.EqualTo(6));
        Assert.That(dijkstra["A"].Path, Is.EqualTo(new List<string>(["E", "B", "A"])));

        Assert.That(dijkstra["B"].Distance, Is.EqualTo(2));
        Assert.That(dijkstra["B"].Path, Is.EqualTo(new List<string>(["E", "B"])));

        Assert.That(dijkstra["C"].Distance, Is.EqualTo(12));
        Assert.That(dijkstra["C"].Path, Is.EqualTo(new List<string>(["E", "D", "C"])));

        Assert.That(dijkstra["D"].Distance, Is.EqualTo(4));
        Assert.That(dijkstra["D"].Path, Is.EqualTo(new List<string>(["E", "D"])));

        Assert.That(dijkstra["F"].Distance, Is.EqualTo(3));
        Assert.That(dijkstra["F"].Path, Is.EqualTo(new List<string>(["E", "F"])));

        Assert.That(dijkstra["G"].Distance, Is.EqualTo(5));
        Assert.That(dijkstra["G"].Path, Is.EqualTo(new List<string>(["E", "D", "G"])));

        Assert.That(dijkstra["H"].Distance, Is.EqualTo(9));
        Assert.That(dijkstra["H"].Path, Is.EqualTo(new List<string>(["E", "F", "H"])));

        Assert.That(dijkstra["I"].Distance, Is.EqualTo(8));
        Assert.That(dijkstra["I"].Path, Is.EqualTo(new List<string>(["E", "I"])));
    }

    [Test]
    public void Test_Source_F()
    {
        var dijkstra = _graph.Dijkstra("F");
        Assert.That(dijkstra["F"].Distance, Is.EqualTo(0));
        Assert.That(dijkstra["F"].Path, Is.EqualTo(new List<string>(["F"])));

        Assert.That(dijkstra["A"].Distance, Is.EqualTo(6));
        Assert.That(dijkstra["A"].Path, Is.EqualTo(new List<string>(["F", "B", "A"])));

        Assert.That(dijkstra["B"].Distance, Is.EqualTo(2));
        Assert.That(dijkstra["B"].Path, Is.EqualTo(new List<string>(["F", "B"])));

        Assert.That(dijkstra["C"].Distance, Is.EqualTo(12));
        Assert.That(dijkstra["C"].Path, Is.EqualTo(new List<string>(["F", "B", "A", "C"])));

        Assert.That(dijkstra["D"].Distance, Is.EqualTo(5));
        Assert.That(dijkstra["D"].Path, Is.EqualTo(new List<string>(["F", "G", "D"])));

        Assert.That(dijkstra["E"].Distance, Is.EqualTo(3));
        Assert.That(dijkstra["E"].Path, Is.EqualTo(new List<string>(["F", "E"])));

        Assert.That(dijkstra["G"].Distance, Is.EqualTo(4));
        Assert.That(dijkstra["G"].Path, Is.EqualTo(new List<string>(["F", "G"])));

        Assert.That(dijkstra["H"].Distance, Is.EqualTo(6));
        Assert.That(dijkstra["H"].Path, Is.EqualTo(new List<string>(["F", "H"])));

        Assert.That(dijkstra["I"].Distance, Is.EqualTo(9));
        Assert.That(dijkstra["I"].Path, Is.EqualTo(new List<string>(["F", "G", "I"])));
    }

    [Test]
    public void Test_Source_G()
    {
        var dijkstra = _graph.Dijkstra("G");
        Assert.That(dijkstra["G"].Distance, Is.EqualTo(0));
        Assert.That(dijkstra["G"].Path, Is.EqualTo(new List<string>(["G"])));

        Assert.That(dijkstra["A"].Distance, Is.EqualTo(10));
        Assert.That(dijkstra["A"].Path, Is.EqualTo(new List<string>(["G", "F", "B", "A"])));

        Assert.That(dijkstra["B"].Distance, Is.EqualTo(6));
        Assert.That(dijkstra["B"].Path, Is.EqualTo(new List<string>(["G", "F", "B"])));

        Assert.That(dijkstra["C"].Distance, Is.EqualTo(9));
        Assert.That(dijkstra["C"].Path, Is.EqualTo(new List<string>(["G", "D", "C"])));

        Assert.That(dijkstra["D"].Distance, Is.EqualTo(1));
        Assert.That(dijkstra["D"].Path, Is.EqualTo(new List<string>(["G", "D"])));

        Assert.That(dijkstra["E"].Distance, Is.EqualTo(5));
        Assert.That(dijkstra["E"].Path, Is.EqualTo(new List<string>(["G", "D", "E"])));

        Assert.That(dijkstra["F"].Distance, Is.EqualTo(4));
        Assert.That(dijkstra["F"].Path, Is.EqualTo(new List<string>(["G", "F"])));

        Assert.That(dijkstra["H"].Distance, Is.EqualTo(5));
        Assert.That(dijkstra["H"].Path, Is.EqualTo(new List<string>(["G", "H"])));

        Assert.That(dijkstra["I"].Distance, Is.EqualTo(5));
        Assert.That(dijkstra["I"].Path, Is.EqualTo(new List<string>(["G", "I"])));
    }

    [Test]
    public void Test_Source_H()
    {
        var dijkstra = _graph.Dijkstra("H");
        Assert.That(dijkstra["H"].Distance, Is.EqualTo(0));
        Assert.That(dijkstra["H"].Path, Is.EqualTo(new List<string>(["H"])));

        Assert.That(dijkstra["A"].Distance, Is.EqualTo(12));
        Assert.That(dijkstra["A"].Path, Is.EqualTo(new List<string>(["H", "F", "B", "A"])));

        Assert.That(dijkstra["B"].Distance, Is.EqualTo(8));
        Assert.That(dijkstra["B"].Path, Is.EqualTo(new List<string>(["H", "F", "B"])));

        Assert.That(dijkstra["C"].Distance, Is.EqualTo(14));
        Assert.That(dijkstra["C"].Path, Is.EqualTo(new List<string>(["H", "G", "D", "C"])));

        Assert.That(dijkstra["D"].Distance, Is.EqualTo(6));
        Assert.That(dijkstra["D"].Path, Is.EqualTo(new List<string>(["H", "G", "D"])));

        Assert.That(dijkstra["E"].Distance, Is.EqualTo(9));
        Assert.That(dijkstra["E"].Path, Is.EqualTo(new List<string>(["H", "F", "E"])));

        Assert.That(dijkstra["F"].Distance, Is.EqualTo(6));
        Assert.That(dijkstra["F"].Path, Is.EqualTo(new List<string>(["H", "F"])));

        Assert.That(dijkstra["G"].Distance, Is.EqualTo(5));
        Assert.That(dijkstra["G"].Path, Is.EqualTo(new List<string>(["H", "G"])));

        Assert.That(dijkstra["I"].Distance, Is.EqualTo(10));
        Assert.That(dijkstra["I"].Path, Is.EqualTo(new List<string>(["H", "G", "I"])));
    }

    [Test]
    public void Test_Source_I()
    {
        var dijkstra = _graph.Dijkstra("I");
        Assert.That(dijkstra["I"].Distance, Is.EqualTo(0));
        Assert.That(dijkstra["I"].Path, Is.EqualTo(new List<string>(["I"])));

        Assert.That(dijkstra["A"].Distance, Is.EqualTo(14));
        Assert.That(dijkstra["A"].Path, Is.EqualTo(new List<string>(["I", "E", "B", "A"])));

        Assert.That(dijkstra["B"].Distance, Is.EqualTo(10));
        Assert.That(dijkstra["B"].Path, Is.EqualTo(new List<string>(["I", "E", "B"])));

        Assert.That(dijkstra["C"].Distance, Is.EqualTo(14));
        Assert.That(dijkstra["C"].Path, Is.EqualTo(new List<string>(["I", "G", "D", "C"])));

        Assert.That(dijkstra["D"].Distance, Is.EqualTo(6));
        Assert.That(dijkstra["D"].Path, Is.EqualTo(new List<string>(["I", "G", "D"])));

        Assert.That(dijkstra["E"].Distance, Is.EqualTo(8));
        Assert.That(dijkstra["E"].Path, Is.EqualTo(new List<string>(["I", "E"])));

        Assert.That(dijkstra["F"].Distance, Is.EqualTo(9));
        Assert.That(dijkstra["F"].Path, Is.EqualTo(new List<string>(["I", "G", "F"])));

        Assert.That(dijkstra["G"].Distance, Is.EqualTo(5));
        Assert.That(dijkstra["G"].Path, Is.EqualTo(new List<string>(["I", "G"])));

        Assert.That(dijkstra["H"].Distance, Is.EqualTo(10));
        Assert.That(dijkstra["H"].Path, Is.EqualTo(new List<string>(["I", "G", "H"])));
    }
}