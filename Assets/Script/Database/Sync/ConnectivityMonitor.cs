using UnityEngine;
using System;
using System.Collections;

public class ConnectivityMonitor : MonoBehaviour
{
    private static ConnectivityMonitor _instance;
    public static ConnectivityMonitor Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("ConnectivityMonitor");
                _instance = go.AddComponent<ConnectivityMonitor>();
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }

    public event Action<bool> OnConnectivityChanged;
    
    private bool _isOnline = false;
    private bool _lastKnownState = false;
    private float _checkInterval = 5f;

    public bool IsOnline => _isOnline;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        CheckConnectivity();
        StartCoroutine(MonitorConnectivity());
    }

    private IEnumerator MonitorConnectivity()
    {
        while (true)
        {
            yield return new WaitForSeconds(_checkInterval);
            CheckConnectivity();
        }
    }

    private void CheckConnectivity()
    {
        bool currentState = Application.internetReachability != NetworkReachability.NotReachable;

        if (currentState != _lastKnownState)
        {
            _lastKnownState = currentState;
            _isOnline = currentState;
            
            string status = _isOnline ? "ONLINE" : "OFFLINE";
            Debug.Log($"[ConnectivityMonitor] Status changed: {status}");
            
            OnConnectivityChanged?.Invoke(_isOnline);
        }
        else
        {
            _isOnline = currentState;
        }
    }

    public void ForceCheck()
    {
        CheckConnectivity();
    }
}