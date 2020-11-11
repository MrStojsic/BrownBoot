using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[HelpURL("https://www.youtube.com/watch?v=rdXC2om16lo")]

// NOTE: Remember we can call Show() passing in a type and check if we have that UI window type in our list without actually needing a direct refernce to it
//       (assuming we only have one of those window classes in the uiPanels)
//       EG Button.onClick.AddListener(() => UiManager.Show<MapInteraction_Window>())
//       And Show<T>() will look to see if it contains a womdow of that type and if so will show it.

public class UiManager : MonoBehaviour
{
    private static UiManager instance;

    [SerializeField] private UiPanel startingPanel;
    [SerializeField] private UiPanel[] uiPanels;

    private UiPanel currentUiPanel;

    private readonly Stack<UiPanel> _history = new Stack<UiPanel>();

    private void Awake() => instance = this;



    public static T GetUiPanel<T>() where T : UiPanel
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

    public static UiPanel Show<T>(bool trackInHistory = true) where T : UiPanel
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

                instance.uiPanels[i].Show();
                instance.currentUiPanel = instance.uiPanels[i];
                return instance.uiPanels[i];
            }
        }
        return null;
    }
    public static void Show(UiPanel uiPanel, bool trackInHistory = true)
    {
        if (instance.currentUiPanel != null)
        {
            if (trackInHistory)
            {
                instance._history.Push(instance.currentUiPanel);
            }
            instance.currentUiPanel.Hide();
        }
        uiPanel.Show();

        instance.currentUiPanel = uiPanel;
    }

    public static void ShowLast()
    {
        if (instance._history.Count != 0)
        {
            Show(instance._history.Pop(), false);
        }
    }
}
