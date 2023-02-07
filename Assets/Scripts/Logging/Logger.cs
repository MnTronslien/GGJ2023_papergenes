
using UnityEngine;
//Simple serializable class for logging
[System.Serializable]
public class Logger
{

    [Tooltip("The level of detail of the log message you are requesting")]
    [SerializeField] public LogLevel level;

    //Declare a generic member variable of type T
    public Object context;

/// <summary>
/// Constructor for the logger
/// </summary>

    public Logger(Object context = null)
    {
        level = LogLevel.Info;
        this.context = context;
    }

    /// <summary>
    /// Log a message to the console
    /// Good for messages that can get logged many times a frame or for messages that are only useful for debugging
    /// </summary>
    public void LogDebug(string message)
    {
        if (level >= LogLevel.Debug)
            //Log the message, if context is not null, then add the context object and preface the message with context object
            if (context != null)
            {
                Debug.Log(context != null ? context.ToString().ToUpper() + ": " + message : message);
            }
            else
            {
                Debug.Log(message);
            }

    }

/// <summary>
/// Log a message to the console
/// Use for information on where the program is in the execution cycle.
/// </summary>
    public void LogInfo(string message)
    {
        if (level >= LogLevel.Info)
            //Log the message, if context is not null, then add the context object and preface the message with context object
            if (context != null)
            {
                Debug.Log(context != null ? context.ToString().ToUpper() + ": " + message : message);
            }
            else
            {
                Debug.Log(message);
            }

    }
/// <summary>
/// Log a message to the console
/// Use for information that is not an error but may be useful to know.
/// </summary>
    public void LogWarning(string message)
    {
        if (level >= LogLevel.Warning)
            //Log the message, if context is not null, then add the context object and preface the message with context object
            if (context != null)
            {
                Debug.LogWarning(context != null ? context.ToString().ToUpper() + ": " + message : message);
            }
            else
            {
                Debug.LogWarning(message);
            }

    }
    /// <summary>
    /// Log a message to the console
    /// Use for information that is an error.
    /// </summary>
    public void LogError(string message)
    {
        if (level >= LogLevel.Error)
            //Log the message, if context is not null, then add the context object and preface the message with context object
            if (context != null)
            {
                Debug.LogError(context != null ? context.ToString().ToUpper() + ": " + message : message);
            }
            else
            {
                Debug.LogError(message);
            }

    }
[System.Serializable]
    public enum LogLevel
    {
        //The higher the level the more detailed log the log messages you are requesting
        Error,
        Warning,
        Info,
        Debug


    }
}