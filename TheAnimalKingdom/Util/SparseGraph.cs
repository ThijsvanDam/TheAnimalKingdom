using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;

namespace TheAnimalKingdom.Util
{
    public class SparseGraph<EdgeType, NodeType> 
        where EdgeType : GraphEdge 
        where NodeType : GraphNode
    {
        public List<NodeType> NodeList { get; set; }
        public List<EdgeType> EdgeList { get; set; }
        public bool IsDirected { get; set; }
        public int NextNodeIndex { get; }
        
        public SparseGraph(bool isDirected)
        {
            NodeList = new List<NodeType>();
            EdgeList = new List<EdgeType>();
            NextNodeIndex = 0;
            IsDirected = isDirected;
        }

        public NodeType GetNode(int index)
        {
            return NodeList[index];
        }

        public EdgeType GetEdge(int from, int to)
        {
            return EdgeList.FirstOrDefault(x => x.From == from && x.To == to);
        }

        public void AddNode(NodeType node)
        {
            NodeList[NextNodeIndex] = node;
        }

        public void RemoveNode(int nodeIndex)
        {
            // Remove a node by setting its index to an invalid node index
            NodeList[nodeIndex].Index = -1;
        }

        public void AddEdge(EdgeType edge)
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

        public static SparseGraph<EdgeType, NodeType> LoadFromFile(string path)
        {
            try
            {
                using (Stream stream = File.Open(path, FileMode.Create))
                {
                    var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    return (SparseGraph<EdgeType, NodeType>) binaryFormatter.Deserialize(stream);
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
    }
}