// See https://aka.ms/new-console-template for more information
using CompositePattern;

CompositeNode root = new("root", null);

CompositeNode node1 = new("node1", root);
LeafNode leaft1 = new LeafNode("leaf1", node1);
LeafNode leaft2 = new LeafNode("leaf2", node1);
node1.AddNode(leaft1);
node1.AddNode(leaft2);

root.AddNode(node1);

root.PrintParent();
root.PrintChild();

node1.PrintParent();
node1.PrintChild();

leaft1.PrintParent();
leaft1.PrintChild();

Console.ReadLine();