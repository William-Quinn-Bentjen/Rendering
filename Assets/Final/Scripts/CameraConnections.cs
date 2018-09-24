using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConnections : MonoBehaviour
{
    public bool IsDestination = false;
    public bool debug = false;
    //public bool Is = false;
    public List<CameraConnections> directlyConnectedTo = new List<CameraConnections>();
    public Dictionary<CameraConnections, Cinemachine.CinemachineSmoothPath> paths = new Dictionary<CameraConnections, Cinemachine.CinemachineSmoothPath>();
    private static Cinemachine.CinemachineSmoothPath.Waypoint here = new Cinemachine.CinemachineSmoothPath.Waypoint { position = Vector3.zero };
    //used internally to hold path data while destinations are discovered 
    struct ConnectionPathData
    {
        public CameraConnections connection;
        public List<CameraConnections> pathToConnection;
        public ConnectionPathData(CameraConnections connectionInput, List<CameraConnections> pathToConnectionInput)
        {
            connection = connectionInput;
            pathToConnection = pathToConnectionInput;
        }
        //only check connection data not pathtoconnection (needed?)
    };
    public void Connect()
    {
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
                        if (debug)
                        {
                            debug = true;
                        }
                        //path to the connection
                        //CameraConnections[] pathToArray = new CameraConnections[50];
                        //unexplored[0].pathToConnection.CopyTo(pathToArray);
                        List<CameraConnections> pathTo = new List<CameraConnections>(unexplored[0].pathToConnection.ToArray()); //unexplored[0].pathToConnection;
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
                if (debug)
                {
                    Debug.Log(i + " " + finalConnections[destination][i].ToString() + " " + transform.InverseTransformPoint(finalConnections[destination][i].transform.position));
                }
                waypoints.Add(new Cinemachine.CinemachineSmoothPath.Waypoint() { position = transform.InverseTransformPoint(finalConnections[destination][i].transform.position) });
            }
            //create path component 
            Cinemachine.CinemachineSmoothPath path = gameObject.AddComponent<Cinemachine.CinemachineSmoothPath>();
            //set waypoints
            path.m_Waypoints = waypoints.ToArray();
            //add path to dictionary of valid paths
            paths.Add(destination, path);
        }



        //OLD
        //create new paths for each camera connected
        //for (int i = 0; i < directlyConnectedTo.Count; i++)
        //{
        //    if (directlyConnectedTo != null)
        //    {
        //        //create new path
        //        Cinemachine.CinemachineSmoothPath path = gameObject.AddComponent<Cinemachine.CinemachineSmoothPath>();
        //        //add waypoint from here to the connected camera
        //        path.m_Waypoints = new Cinemachine.CinemachineSmoothPath.Waypoint[2] { here, new Cinemachine.CinemachineSmoothPath.Waypoint { position = transform.InverseTransformPoint(directlyConnectedTo[i].transform.position) } };
        //        //add path to the dictionary
        //        paths.Add(directlyConnectedTo[i], path);
        //    }
        //    else
        //    {
        //        Debug.LogError("Can not have a camera connection with a null Connected To value", gameObject);
        //    }

        //}
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
        paths.Clear();
    }
}
