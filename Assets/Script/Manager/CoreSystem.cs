/// <summary>
/// CoreSystem is the central singleton of the game lifecycle, responsible for:
/// - Managing global services (called Managers) and registering them at runtime
/// - Providing typed access to registered services
/// - Triggering events when services become available
/// - Persisting across scenes (DontDestroyOnLoad)
///
/// Manager<T> is a base class for core services, enabling:
/// - Automatic registration with CoreSystem upon initialization
/// - Safe and generic access patterns
/// - Scene-independent logic injection and dependency handling
/// </summary>

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Com.DefaultCompany.LostSoul
{
    // Class of all Manager and auto set them after CoreSystem and before all other GameObject
    public abstract class Manager<T> : MonoBehaviour where T : class
    {
        protected CoreSystem core;
        private bool _initialized;

        protected virtual void OnEnable()
        {
            CoreSystem.OnInit += Init;
        }
        protected virtual void OnDisable()
        {
            CoreSystem.OnInit -= Init;
        }

        protected void Init()
        {
            if (_initialized) return;
            _initialized = true;

            core = CoreSystem.Instance;
            if (this is T derived)
            {
                if (!core.RegisterService(derived))
                {
                    Debug.LogError($"Duplicate manager {typeof(T)} detected in scene {gameObject.scene.name}. Destroying duplicate.");
                    Destroy(gameObject);
                    return;
                }
            }

            transform.SetParent(core.transform);
            OnRegistered();
        }

        protected virtual void OnRegistered()
        {
        }
    }

    public class CoreSystem : MonoBehaviour
    {
        public static CoreSystem Instance { get; private set; }

        private readonly Dictionary<Type, object> _services = new();

        // Event
        public static event Action<Type> OnServiceRegistered; // Generic event

        public static event Action OnInit;

        #region Unity Methods
        private void Awake()
        {
            DontDestroyOnLoad(this);

            // Auto instanciate
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void Start()
        {
            OnInit?.Invoke();
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        #endregion

        #region Methods
        // Auto set manager
        public bool RegisterService<T>(T service) where T : class
        {
            Type type = typeof(T);
            if (service == null || _services.ContainsKey(type))
            {
                Debug.LogError($"Attempt to register a null service or {type} is already registered.");
                return false;
            }

            _services.Add(type, service);

            OnServiceRegistered?.Invoke(typeof(T));

            Debug.Log($"service registered : {type}");
            return true;
        }

        public T GetService<T>() where T : class
        {
            if (_services.TryGetValue(typeof(T), out var service))
                return service as T;

            Debug.LogError($"Service not found : {typeof(T)}");
            return null;
        }
        #endregion

        #region Implementation
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Debug.Log($"OnSceneLoaded: {scene.name} Mode : {mode}", gameObject);
            OnInit?.Invoke();
        }
        #endregion
    }
}