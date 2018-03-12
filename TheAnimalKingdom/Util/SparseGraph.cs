using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace TheAnimalKingdom.Util
{
    public class SparseGraph {
        public List<NavGraphNode> NodeList { get; set; }
        public List<GraphEdge> EdgeList { get; set; }
        public bool IsDirected { get; set; }
        public int NextNodeIndex { get; }
        
        public SparseGraph(bool isDirected)
        {
            NodeList = new List<NavGraphNode>();
            EdgeList = new List<GraphEdge>();
            NextNodeIndex = 0;
            IsDirected = isDirected;
        }

        public NavGraphNode GetNode(int index)
        {
            return NodeList[index];
        }

        public GraphEdge GetEdge(int from, int to)
        {
            return EdgeList.FirstOrDefault(x => x.From == from && x.To == to);
        }

        public IEnumerable<GraphEdge> GetConnectedEdges(int from)
        {
            return EdgeList.Where(x => x.From == from);
        }

        public void AddNode(NavGraphNode node)
        {
            NodeList[NextNodeIndex] = node;
        }

        public void RemoveNode(int nodeIndex)
        {
            // Remove a node by setting its index to an invalid node index
            NodeList[nodeIndex].Index = -1;
        }

        public void AddEdge(GraphEdge edge)
        {
            EdgeList.Add(edge);
        }

        public void RemoveEdge(int from, int to)
        {
            EdgeList.Remove(EdgeList.FirstOrDefault(x => x.From == from && x.To == to));
        }

        public int NumberOfNodes()
        {
            return NodeList.Count;
        }

        public int NumberOfActiveNodes()
        {
            return NodeList.Count(x => x.Index >= 0);
        }

        public int NumberOfEdges()
        {
            return EdgeList.Count;
        }

        public bool IsEmpty()
        {
            return NumberOfNodes() == 0;
        }

        public bool IsPresent(int nodeIndex)
        {
            return NodeList.Exists(x => x.Index == nodeIndex);
        }

        public void Clear()
        {
            EdgeList.Clear();
            NodeList.Clear();
        }

        #region File Handling

        public bool SaveToFile(string fileName)
        {
            string path = Directory.GetCurrentDirectory();

            try
            {
                using (Stream stream = File.Open(path + fileName, FileMode.Create))
                {
                    var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    binaryFormatter.Serialize(stream, this);
                }

                return true;
            }
            catch (SerializationException)
            {
                return false;
            }
        }

        public static SparseGraph LoadFromFile(string path)
        {
            try
            {
                using (Stream stream = File.Open(path, FileMode.Create))
                {
                    var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    return (SparseGraph) binaryFormatter.Deserialize(stream);
                }
            }
            catch (SerializationException)
            {
                return null;
            }
            catch (FileLoadException)
            {
                return null;
            }
        }

        #endregion

        #region Drawing

        public void Render(Graphics g)
        {
            //Do not start drawing when the graph is empty
            if (IsEmpty()) return;
            
            //Start the recursive draw method with the first node in the list
            Render(g, NodeList.First());
        }

        private void Render(Graphics g, NavGraphNode node)
        {
            //Draw the node itself
            var left = (int)node.Position.X - 2;
            var top = (int)node.Position.Y - 2;
            g.FillEllipse(new SolidBrush(Color.Black), left, top, 4, 4);
            
            //Draw edges to other nodes and continue with those nodes
            foreach (var edge in GetConnectedEdges(node.Index))
            {
                var nodeTo = GetNode(edge.To);
                g.DrawLine(new Pen(Color.Black), node.Position.ToPoint(), nodeTo.Position.ToPoint());
                //Continue down the graph and draw the connected node
                Render(g, nodeTo);
            }
        }

        #endregion
    }
}