using UnityEngine;

public class WorldGenerator : MonoBehaviour
    {
    [SerializeField] private GameObject _horizontalTilePrefab, _verticalTilePrefab;
    [SerializeField] private Material _playerMaterial, _tileMaterial;
    [SerializeField] private Transform _tileContainer;
    
    private void Start()
        {
        TextAsset text = Resources.Load("level_" + Config.Level) as TextAsset;
        Init(text.text);
        }
    private void Init(string data)
        {
        bool start = false;
        string word = "", key = "", type = "", axis = "";
        float scale = 0, x = 0, z = 0;
        foreach (char ch in data)
            {
            if (ch.Equals('{'))
                {
                start = true;
                continue;
                }
            if (start && ch.Equals('}'))
                {
                AddTile(type, axis, scale, x, z);
                start = false;
                continue;
                }
            if (ch.Equals(':'))
                {
                key = word;
                word = "";
                continue;
                }
            if (ch.Equals('|'))
                {
                switch (key)
                    {
                    case "type": type = word; break;
                    case "axis": axis = word; break;
                    case "scale": scale = int.Parse(word); break;
                    case "x": x = (float)System.Convert.ToDouble(word); break;
                    case "z": z = (float)System.Convert.ToDouble(word); break;
                    }
                word = "";
                continue;
                }
            if (start)
                word += ch;
            }
        }
    private void AddTile(string type, string axis, float scale, float x, float z)
        {
        GameObject tile = Instantiate((axis.Equals("horizontal") ? _horizontalTilePrefab : _verticalTilePrefab), new Vector3(x, .25f, z), Quaternion.identity);
        tile.transform.localRotation = Quaternion.Euler(transform.localRotation.x, (axis.Equals("horizontal") ? 0 : 90), transform.localRotation.z);
        tile.GetComponent<MeshRenderer>().material = type.Equals("player") ? _playerMaterial : _tileMaterial;
        tile.GetComponent<Tile>().ChangeScale(scale);
        tile.transform.SetParent(_tileContainer);
        }
    }