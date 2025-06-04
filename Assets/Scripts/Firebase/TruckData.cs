using System.Collections.Generic;

[System.Serializable]
public class TruckData
{
    public Identification identification;
    public Route route;
    public LoadStatus loadStatus;
    public Telemetry telemetry;
    public Operator operatorData;
    public string timestamp;
}

[System.Serializable]
public class Identification
{
    public string brand;
    public string model;
    public string truckNumber;
}

[System.Serializable]
public class Route
{
    public string origin;
    public string destination;
    public List<string> currentPath;
    public int currentNodeIndex;
}

[System.Serializable]
public class LoadStatus
{
    public bool isLoaded;
    public string material;
    public float tons;
}

[System.Serializable]
public class Telemetry
{
    public float speed_kph;
}

[System.Serializable]
public class Operator
{
    public string name;
    public string employeeId;
}