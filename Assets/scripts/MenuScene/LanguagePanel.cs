using UnityEngine;

public class LanguagePanel : BasePanel {

    [SerializeField] private BasePanel _menuPanel;

    public void Back() => Hide(_menuPanel, Vector2.right);

    }