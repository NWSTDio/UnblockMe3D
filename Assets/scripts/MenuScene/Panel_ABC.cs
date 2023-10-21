using UnityEngine;

public class Panel_ABC : BasePanel {

    [SerializeField] private BasePanel _settingsPanel;

    public void ToSettings() => Hide(_settingsPanel, Vector2.right);

    }