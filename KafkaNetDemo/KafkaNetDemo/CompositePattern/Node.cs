namespace CompositePattern;

internal abstract class Node
{
    public abstract string Name { get; }
    public virtual void AddNode(Node node) => throw new NotImplementedException();
    public virtual void RemoveNode(Node node) => throw new NotImplementedException();
    public virtual void PrintChild() => throw new NotImplementedException();
    public virtual void PrintParent() => throw new NotImplementedException();
}

internal class CompositeNode : Node
{
    private string name;
    private Node? parent;
    private List<Node> leaf;

    public override string Name => name;

    public CompositeNode(string name, Node? parent)
    {
        this.name = name;
        this.parent = parent;
        leaf = new List<Node>();
    }

    public override void AddNode(Node node)
    {
        leaf.Add(node);
    }

    public override void RemoveNode(Node node)
    {
        base.RemoveNode(node);
    }

    public override void PrintParent()
    {
        if (parent != null)
            Console.WriteLine($"Parent: {parent.Name}");
        else
            Console.WriteLine($"Root Node");
    }

    public override void PrintChild()
    {
        Console.WriteLine($"Parent: {string.Join(", ", leaf.Select(x => x.Name))}");
    }
}

internal class LeafNode : Node
{
    private string name;
    private Node parent;
    
    public override string Name => name;

    public LeafNode(string name, Node parent)
    {
        this.name = name;
        this.parent = parent;
    }

    public override void PrintParent()
    {
        Console.WriteLine($"Parent: {parent.Name}");
    }

}