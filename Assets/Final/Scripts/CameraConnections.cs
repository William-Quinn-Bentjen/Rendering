using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConnections : MonoBehaviour
{
    public bool IsDestination = false;
    public List<CameraConnections> directlyConnectedTo = new List<CameraConnections>();
    [Header("Used to poplate dictonary, DON'T SET BY HAND")]
    public List<ConnectionPathData> availableDestinations = new List<ConnectionPathData>();
    [Header("Jenk solution to priority not working correctly")]
    public Cinemachine.CinemachineVirtualCamera Camera;
    /// <summary>
    /// Holds all destinations that can be reached and the path to get there.
    /// </summary>
    [SerializeField]
    public Dictionary<CameraConnections, Cinemachine.CinemachineSmoothPath> paths;// = new Dictionary<CameraConnections, Cinemachine.CinemachineSmoothPath>();
    private static Cinemachine.CinemachineSmoothPath.Waypoint here = new Cinemachine.CinemachineSmoothPath.Waypoint { position = Vector3.zero };
    //used internally to hold path data while destinations are discovered as well as populate the dictonary on awake
    [System.Serializable]
    public struct ConnectionPathData
    {
        public CameraConnections connection;
        public List<CameraConnections> pathToConnection;
        public Cinemachine.CinemachineSmoothPath path;
        /// <summary>
        /// Creates path data for internal use while creating a path
        /// </summary>
        /// <param name="connectionInput">connection node</param>
        /// <param name="pathToConnectionInput">path to the connection node as list of connections</param>
        public ConnectionPathData(CameraConnections connectionInput, List<CameraConnections> pathToConnectionInput)
        {
            connection = connectionInput;
            pathToConnection = pathToConnectionInput;
            path = null;
        }
        /// <summary>
        /// Creates path data for creating a dictionary after path values have been calculated
        /// </summary>
        /// <param name="connectionInput">destination</param>
        /// <param name="pathInput">smoothpath to the connection</param>
        public ConnectionPathData(CameraConnections connectionInput, Cinemachine.CinemachineSmoothPath pathInput)
        {
            connection = connectionInput;
            pathToConnection = null;
            path = pathInput;
        }

        //only check connection data not pathtoconnection (needed?)
    };
    private void Reset()
    {
        Disconnect();
        Camera = GetComponent<Cinemachine.CinemachineVirtualCamera>();
    }
    private void Awake()
    {
        //recreate the dictonary because unity clears the dictonary on play anyway
        paths = new Dictionary<CameraConnections, Cinemachine.CinemachineSmoothPath>();
        foreach(ConnectionPathData data in availableDestinations)
        {
            paths.Add(data.connection, data.path);
        }
    }
    public void Connect()
    {
        paths = new Dictionary<CameraConnections, Cinemachine.CinemachineSmoothPath>();
        //remove connections
        Disconnect();
        //prep for connections
        //used to hold all the destinations that will need to be added to the path dictonary at the end of the connect proccess
        Dictionary<CameraConnections, List<CameraConnections>> finalConnections = new Dictionary<CameraConnections, List<CameraConnections>>();
        //list of all connections discovered
        List<CameraConnections> discovered = new List<CameraConnections>();// directlyConnectedTo.ToArray());
        foreach (CameraConnections connection in directlyConnectedTo)
        {
            if (discovered.Contains(connection) == false)
            {
                discovered.Add(connection);
            }
        }
        //list of unexplored connections
        List<ConnectionPathData> unexplored = new List<ConnectionPathData>();
        //set up inital unexplored list
        foreach (CameraConnections connection in discovered)
        {
            if (connection.IsDestination)
            {
                finalConnections.Add(connection, new List<CameraConnections>() { this , connection });
            }
            unexplored.Add(new ConnectionPathData(connection, new List<CameraConnections>() { this , connection}));
        }
        discovered.Add(this);
        //list of explored connections
        List<ConnectionPathData> explored = new List<ConnectionPathData>() { new ConnectionPathData(this, new List<CameraConnections>()) };
        //while there are unexplored connections
        while(unexplored.Count != 0)
        {
            //look at each of the direct connections for the connection
            foreach (CameraConnections connection in unexplored[0].connection.directlyConnectedTo)
            {
                if (connection != null)
                {
                    //if it's direct connection is not discovered yet add it to the discovered list and the unexplored list
                    if (discovered.Contains(connection) == false)
                    {
                        //path to the connection
                        List<CameraConnections> pathTo = new List<CameraConnections>(unexplored[0].pathToConnection.ToArray());
                        pathTo.Add(connection);
                        //add to unexplored list
                        unexplored.Add(new ConnectionPathData(connection, pathTo));
                        //add to the discovered list so it's not found again
                        discovered.Add(connection);
                        //if its a destination
                        if (connection.IsDestination && finalConnections.ContainsKey(connection) == false)
                        {
                            //add to the final destinations dictonary
                            finalConnections.Add(connection, pathTo);
                        }
                    }
                }
                else
                {
                    Debug.LogError("Can not have a camera connection with a null Connected To value", gameObject);
                }
            }
            //add connection to the explored list after we have discovered all direct connections from it
            explored.Add(unexplored[0]);
            //remove the node that was just explored
            unexplored.RemoveAt(0);
        }
        //iterate through dictionary of final destinations and create a path using the data from it key and value
        foreach (CameraConnections destination in finalConnections.Keys)
        {
            //add waypoints starting from here to the destination
            List<Cinemachine.CinemachineSmoothPath.Waypoint> waypoints = new List<Cinemachine.CinemachineSmoothPath.Waypoint>() /*{ here }*/;
            for (int i = 0; i < finalConnections[destination].Count; i++)
            {
                waypoints.Add(new Cinemachine.CinemachineSmoothPath.Waypoint() { position = transform.InverseTransformPoint(finalConnections[destination][i].transform.position) });
            }
            //create path component 
            Cinemachine.CinemachineSmoothPath path = gameObject.AddComponent<Cinemachine.CinemachineSmoothPath>();
            //set waypoints
            path.m_Waypoints = waypoints.ToArray();
            //add path to dictionary of valid paths
            paths.Add(destination, path);
            //list so the dictonary can be repopulated on play
            availableDestinations.Add(new ConnectionPathData(destination, path));
        }
    }
    public void Disconnect()
    {
        //destroy all paths on this gameObject
        Cinemachine.CinemachineSmoothPath[] pathComponents = GetComponents<Cinemachine.CinemachineSmoothPath>();
        for (int i = 0; i < pathComponents.Length; i++)
        {
            //destory path component 
            DestroyImmediate(pathComponents[i]);
        }
        //clear dictonary of paths
        //paths.Clear();
        availableDestinations.Clear();
    }
}
