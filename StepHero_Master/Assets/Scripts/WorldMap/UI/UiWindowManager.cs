using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[HelpURL("https://www.youtube.com/watch?v=rdXC2om16lo")]

// NOTE: Remember we can call Show() passing in a type and check if we have that UI window type in our list without actually needing a direct refernce to it
//       (assuming we only have one of those window classes in the uiPanels)
//       EG Button.onClick.AddListener(() => UiManager.Show<MapInteraction_Window>())
//       And Show<T>() will look to see if it contains a womdow of that type and if so will show it.

public class UiWindowManager : MonoBehaviour
{
    private static UiWindowManager instance;

    [SerializeField] private UiWindow startingPanel;
    [SerializeField] private UiWindow[] uiPanels;

    private UiWindow currentUiPanel;

    private readonly Stack<UiWindow> _history = new Stack<UiWindow>();

    private void Awake() => instance = this;



    public static T GetUiPanel<T>() where T : UiWindow
    {
        for (int i = 0; i < instance.uiPanels.Length; i++)
        {
            if (instance.uiPanels[i] is T tUiPanel)
            {
                return tUiPanel;
            }
        }
        return null;
    }

    public static UiWindow Show<T>(bool trackInHistory = false) where T : UiWindow
    {
        for (int i = 0; i < instance.uiPanels.Length; i++)
        {
            if (instance.uiPanels[i] is T)
            {
                if (instance.currentUiPanel != null)
                {
                    if (trackInHistory)
                    {
                        instance._history.Push(instance.currentUiPanel);
                    }
                    instance.currentUiPanel.Hide();
                }

                instance.currentUiPanel = instance.uiPanels[i];
                instance.uiPanels[i].Show();

                return instance.uiPanels[i];
            }
        }
        return null;
    }
    public static void Show(UiWindow uiPanel, bool trackInHistory = false)
    {
        if (instance.currentUiPanel != null)
        {
            if (trackInHistory)
            {
                instance._history.Push(instance.currentUiPanel);
            }
            instance.currentUiPanel.Hide();
        }
        instance.currentUiPanel = uiPanel;

        uiPanel.Show();


    }

    public static void ShowLast()
    {
        if (instance._history.Count != 0)
        {
            Show(instance._history.Pop(), false);
        }
        // HACK Added in by me for when we need to close the last window.
        else
        {
            instance.currentUiPanel.Hide();
        }
    }
}
