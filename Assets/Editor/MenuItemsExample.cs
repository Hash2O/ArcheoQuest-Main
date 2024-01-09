using UnityEngine;
using UnityEditor;

public class MenuItemsExample
{
    // Add a new menu item under an existing menu

    [MenuItem("Window/Unity Learn - New Option")]
    private static void NewMenuOption()
    {
    }

    // Add a new menu item with hotkey CTRL-SHIFT-A

    [MenuItem("Tools/Unity Learn - New Option %#a")]
    private static void NewMenuOption2()
    {
    }

    // Add a menu item with multiple levels of nesting

    [MenuItem("Tools/Unity Learn - SubMenu/Option")]
    private static void NewNestedOption()
    {
    }

    // Add a new menu item with hotkey CTRL-G

    [MenuItem("Tools/Unity Learn - Item %g")]
    private static void NewNestedOption2()
    {
    }

    // Add a new menu item with hotkey G
    [MenuItem("Tools/Unity Learn - Item2 _g")]
    private static void NewOptionWithHotkey()
    {
    }

    // Add a new menu item that is accessed by right-clicking on an asset in the project view

    [MenuItem("Assets/Unity Learn - Load Additive Scene")]
    private static void LoadAdditiveScene()
    {
        var selected = Selection.activeObject;
        EditorApplication.OpenSceneAdditive(AssetDatabase.GetAssetPath(selected));
    }

    [MenuItem("Assets/Unity Learn - ProcessTexture")]
    private static void DoSomethingWithTexture()
    {
    }

    // Note that we pass the same path, and also pass "true" to the second argument.
    [MenuItem("Assets/Unity Learn - ProcessTexture", true)]
    private static bool NewMenuOptionValidation()
    {
        // This returns true when the selected object is a Texture2D (the menu item will be disabled otherwise).
        return Selection.activeObject.GetType() == typeof(Texture2D);
    }

    // Adding a new menu item under Assets/Create

    [MenuItem("Assets/Create/Unity Learn - Add Configuration")]
    private static void AddConfig()
    {
        // Create and add a new ScriptableObject for storing configuration
    }

    // Add a new menu item that is accessed by right-clicking inside the RigidBody component

    [MenuItem("CONTEXT/Rigidbody/Unity Learn - New Option")]
    private static void NewOpenForRigidBody()
    {
    }

    [MenuItem("CONTEXT/Rigidbody/Unity Learn - New Option With Context")]
    private static void NewMenuOption(MenuCommand menuCommand)
    {
        // The RigidBody component can be extracted from the menu command using the context field.
        var rigid = menuCommand.context as Rigidbody;
    }

    [MenuItem("Unity Learn - NewMenu/Option1", false, 1)]
    private static void UnityLearnNewMenuOption()
    {
    }

    [MenuItem("Unity Learn - NewMenu/Option2", false, 2)]
    private static void UnityLearnNewMenuOption2()
    {
    }

    [MenuItem("Unity Learn - NewMenu/Option3", false, 3)]
    private static void UnityLearnNewMenuOption3()
    {
    }

    [MenuItem("Unity Learn - NewMenu/Option4", false, 51)]
    private static void UnityLearnNewMenuOption4()
    {
    }

    [MenuItem("Unity Learn - NewMenu/Option5", false, 52)]
    private static void UnityLearnNewMenuOption5()
    {
    }
}
